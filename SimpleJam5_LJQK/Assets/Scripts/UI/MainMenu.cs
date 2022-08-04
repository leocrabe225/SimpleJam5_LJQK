using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{


    public GameObject blackOutSquare;


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
        StartCoroutine(ChangeSceneFade());
        
    }

    public void QuitGame()
    {
        StartCoroutine(QuitFade());
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

    public IEnumerator ChangeSceneFade()
    {
        StartCoroutine(FadeBlackOutSquare());
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(1);
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
