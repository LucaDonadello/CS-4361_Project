using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    // The image used for fading.
    public Image fadeImage;
    public float fadeDuration = 7.0f;

    // start called at the start of the game
    public void StartFade()
    {
        StartCoroutine(FadeOut());
        fadeImage.gameObject.SetActive(true);
    }

    // coroutine to fade the image out.
    private IEnumerator FadeOut()
    {
        float timer = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(0f, 0f, 0f, 1f); // black

        while (timer < fadeDuration)
        {
            // interpolate the color of the image.
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // ensure the fade is complete.
        fadeImage.color = endColor;
    }
}
