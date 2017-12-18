using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeMethods
{
    float fadeSpeedIn = 2.4f;
    float fadeSpeedOut = 2f;

    void hideFade(Image img, Text txt, Text txt2)
    {
        // Lerp the colour of the image between itself and transparent.
        img.color = Color.Lerp(img.color, Color.clear, fadeSpeedOut * Time.deltaTime);

        if (txt != null) {
            txt.color = Color.Lerp(txt.color, Color.clear, fadeSpeedOut * Time.deltaTime);
        }

        if (txt2 != null)
        {
            txt2.color = Color.Lerp(txt2.color, Color.clear, fadeSpeedOut * Time.deltaTime);
        }
    }

    void showFade(Image img, Text txt, Color colorSprite, Color colorText, Text txt2)
    {
        // Lerp the colour of the image between itself and black.
        img.color = Color.Lerp(img.color, colorSprite, fadeSpeedIn * Time.deltaTime);

        if (txt != null) {
            txt.color = Color.Lerp(txt.color, colorText, fadeSpeedIn * Time.deltaTime);
        }

        if (txt2 != null)
        {
            txt2.color = Color.Lerp(txt2.color, colorText, fadeSpeedIn * Time.deltaTime);
        }
    }

    public IEnumerator FadeIn(Image img, Text txt, Color spriteColor, Color textColor, Text controlTxt = null)
    {
        img.enabled = true;
        txt.enabled = true;

        do
        {
            if (img.color.a < 0.95f)
            {
                showFade(img, txt, spriteColor, textColor, controlTxt);
                yield return null;
            }

            else
            {
                yield break;
            }

        } while (true);
    }

    public IEnumerator FadeOut(Image img, Text txt, Text controlTxt = null)
    {

        do
        {
            if (img.color.a >= 0.05f)
            {
                hideFade(img, txt, controlTxt);
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


    public IEnumerator ImageFadeIn(Image img, Color spriteColor)
    {
        img.enabled = true;

        do
        {
            if (img.color.a < 0.95f)
            {
                showFade(img, null, spriteColor, Color.blue, null);
                yield return null;
            }

            else
            {
                yield break;
            }

        } while (true);
    }

    public IEnumerator ImageFadeOut(Image img)
    {

        do
        {
            if (img.color.a >= 0.05f)
            {
                hideFade(img, null, null);
                yield return null;
            }

            else
            {
                img.enabled = false;
                yield break;
            }

        } while (true);
    }


}