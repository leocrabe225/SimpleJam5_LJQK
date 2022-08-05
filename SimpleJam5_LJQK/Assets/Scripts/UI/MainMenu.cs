using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeBlackOutSquare(false));
    }

    // Update is called once per frame
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
        StartCoroutine(ChangeSceneFade(2));
    }

    public void AttackMode()
    {

        AttackLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = ActivatedAttack;
        DefenseLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = DeactivatedDefense;
        DefenseLogoParent.transform.GetChild(2).GetComponent<RawImage>().texture = DeactivatedDrone;

        DefenseLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.5f, 0.5f);
        AttackLogoParent.transform.GetComponent<Image>().rectTransform.localScale = new Vector2(0.75f, 0.75f);
    }

    public void DefenseMode()
    {
        AttackLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = DeactivatedAttack;
        DefenseLogoParent.transform.GetChild(0).GetComponent<RawImage>().texture = ActivatedDefense;
        DefenseLogoParent.transform.GetChild(2).GetComponent<RawImage>().texture = ActivatedDrone;

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
