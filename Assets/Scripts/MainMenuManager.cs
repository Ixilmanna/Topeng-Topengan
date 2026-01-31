using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public Image fadeImage;
    public float fadeDuration = 1f;
    public float delayBeforeLoad = 0.5f;

    public void NewGame()
    {
        StartCoroutine(FadeAndLoad());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator FadeAndLoad()
    {
        // Fade out
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        // Optional delay
        yield return new WaitForSeconds(delayBeforeLoad);

        SceneManager.LoadScene("movement test");
    }
}
