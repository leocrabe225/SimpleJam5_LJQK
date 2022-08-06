using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Remember
{
    static public float win_timer = 0;
    static public int games_won = 0;
    static public float highscore_timer = 0;
    

    static public string get_timer_string(float timer) {
        TimeSpan time = TimeSpan.FromSeconds(timer);
        return(time.ToString(@"mm\:ss\:fff"));
    }
}

public class ScoreData
{
    public float win_timer = 0;
    public int games_won = 0;
    public float highscore_timer = 0;

    public ScoreData(float one, int two, float three) {
        this.win_timer = one;
        this.games_won = two;
        this.highscore_timer = three;
    }
}

public class MainMenu : MonoBehaviour
{
    public GameObject blackOutSquare;
    [SerializeField]
    GameObject AttackLogoParent;
    [SerializeField]
    Texture ActivatedAttack, DeactivatedAttack;
    [SerializeField]
    GameObject DefenseLogoParent;
    [SerializeField]
    Texture ActivatedDefense, DeactivatedDefense, ActivatedDrone, DeactivatedDrone;
    [SerializeField]
    Score_Manager score_manager;
    [SerializeField]
    private TextMeshProUGUI win_text;
    [SerializeField]
    private TextMeshProUGUI highscore_text;

    string get_timer_string(float timer) {
        TimeSpan time = TimeSpan.FromSeconds(timer);
        return(time.ToString(@"mm\:ss\:fff"));
    }

    string path = "";
    string persistentPath = "";
    ScoreData score_data;


    void JSONsave() {
        score_data= new ScoreData(Remember.win_timer, Remember.games_won, Remember.highscore_timer);

        string savePath = persistentPath;

        Debug.Log("Saving Data at " + savePath);
        string json = JsonUtility.ToJson(score_data);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    bool JSONread() {
        if (System.IO.File.Exists(persistentPath))
        {
            Debug.Log("File Exists");
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();

            score_data = JsonUtility.FromJson<ScoreData>(json);
            Debug.Log(score_data.ToString());
            return (true);
        }
        else {
            Debug.Log("File does not Exists");
            return (false);
        }
    }

    void SetPaths() {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    void Start()
    {
        SetPaths();
        Debug.Log("Before");
        Scene currentScene = SceneManager.GetActiveScene();
        int buildIndex = currentScene.buildIndex;
        if (buildIndex == 0) {
            if (JSONread()) {
                Remember.win_timer = score_data.win_timer;
                Remember.games_won = score_data.games_won;
                Remember.highscore_timer = score_data.highscore_timer;
            }
        }
        if (Remember.games_won > 1) {
            highscore_text.text = "Highscore : " + Remember.get_timer_string(Remember.highscore_timer);
        }
        else if (Remember.games_won == 1 && buildIndex == 0) {
            highscore_text.text = "Highscore : " + Remember.get_timer_string(Remember.highscore_timer);
        }
        else {
            highscore_text.text = "";
        }
        if (Remember.games_won == 1) {
            Remember.highscore_timer = Remember.win_timer;
        }
        if (buildIndex == 3) {
            if (Remember.games_won > 1 && Remember.win_timer < Remember.highscore_timer) {
                win_text.text = "New highscore : " + Remember.get_timer_string(Remember.win_timer) + " !\nCongratulations !";
                Remember.highscore_timer = Remember.win_timer;
            }
            else {
                win_text.text = "You won in " + Remember.get_timer_string(Remember.win_timer) + " !\nCongratulations !";
            }
            JSONsave();
        }
        Debug.Log("After");
        StartCoroutine(FadeBlackOutSquare(false));
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(ChangeSceneFade(1));
    }

    public void QuitGame()
    {
        StartCoroutine(QuitFade());
    }

    public void Death()
    {
        score_manager.StopStopWatch();
        StartCoroutine(ChangeSceneFade(2));
    }

    public void Win()
    {
        score_manager.StopStopWatch();
        Remember.win_timer = score_manager.currentTime;
        Remember.games_won++;
        StartCoroutine(ChangeSceneFade(3));
    }

    public void AttackMode()
    {

        AttackLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = ActivatedAttack;
        DefenseLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = DeactivatedDefense;
        DefenseLogoParent.transform.GetChild(1).GetComponent<RawImage>().texture = DeactivatedDrone;

        DefenseLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.5f, 0.5f);
        AttackLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.75f, 0.75f);
    }

    public void DefenseMode()
    {
        
        AttackLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = DeactivatedAttack;
        DefenseLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = ActivatedDefense;
        DefenseLogoParent.transform.GetChild(1).GetComponent<RawImage>().texture = ActivatedDrone;

        DefenseLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.75f, 0.75f);
        AttackLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.5f, 0.5f);

    }





    public IEnumerator FadeBlackOutSquare (bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackOutSquare.GetComponent<RawImage>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while(blackOutSquare.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<RawImage>().color = objectColor;

                yield return null;
            }
        }
        else
        {
            while(blackOutSquare.GetComponent<RawImage>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<RawImage>().color = objectColor;

                yield return null;
            }
        }
    }

    public IEnumerator ChangeSceneFade(int i)
    {
        StartCoroutine(FadeBlackOutSquare());
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(i);
        yield return null;
    }

    public IEnumerator QuitFade()
    {
        StartCoroutine(FadeBlackOutSquare());
        yield return new WaitForSeconds(0.8f);
        Application.Quit();
        yield return null;
    }



}
