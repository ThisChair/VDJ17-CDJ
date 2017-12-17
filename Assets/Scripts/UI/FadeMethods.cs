using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeMethods
{
    float fadeSpeedIn = 2.4f;
    float fadeSpeedOut = 2f;

    void hideFade(Image img, Text txt)
    {
        // Lerp the colour of the image between itself and transparent.
        img.color = Color.Lerp(img.color, Color.clear, fadeSpeedOut * Time.deltaTime);
        txt.color = Color.Lerp(txt.color, Color.clear, fadeSpeedOut * Time.deltaTime);
    }

    void showFade(Image img, Text txt, Color colorSprite, Color colorText)
    {
        // Lerp the colour of the image between itself and black.
        img.color = Color.Lerp(img.color, colorSprite, fadeSpeedIn * Time.deltaTime);
        txt.color = Color.Lerp(txt.color, colorText, fadeSpeedIn * Time.deltaTime);
    }

    public IEnumerator FadeIn(Image img, Text txt, Color spriteColor, Color textColor)
    {
        img.enabled = true;
        txt.enabled = true;

        do
        {
            if (img.color.a < 0.95f)
            {
                showFade(img, txt, spriteColor, textColor);
                yield return null;
            }

            else
            {
                yield break;
            }

        } while (true);
    }

    public IEnumerator FadeOut(Image img, Text txt)
    {

        do
        {
            if (img.color.a >= 0.05f)
            {
                hideFade(img, txt);
                yield return null;
            }

            else
            {
                img.enabled = false;
                txt.enabled = false;
                yield break;
            }

        } while (true);
    }
}