using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeIn : MonoBehaviour {

    private float alphaValue;
    private Image fadeImage;
    
	void Start ()
    {
        fadeImage = GetComponent<Image>();
        alphaValue = fadeImage.color.a;
        StartCoroutine(fadeIn());
	}

    private IEnumerator fadeIn()
    {
        while (alphaValue > 0)
        {
            alphaValue -= 1 * Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alphaValue);
            yield return null;
        }

        if (alphaValue <= 0)
        {
            fadeImage.raycastTarget = false;
        }
    }
}
