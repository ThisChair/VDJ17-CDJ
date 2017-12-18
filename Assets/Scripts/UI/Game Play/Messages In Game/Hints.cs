using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hints : Message
{
    Text hint;
    float timeLimit = 1.2f;
    private bool showingMessage;

    public override void Start()
    {
        base.Start();
        hint = GetComponent<Text>();
    }

    private void Update () {
        if (anyMessage && !showingMessage) {
            showingMessage = true;
            StartCoroutine(DisplayMessages());
        }
    }

    private IEnumerator DisplayMessages() {

        while (anyMessage) {
            hint.text = ConsumeMessage();
            yield return new WaitForSeconds(timeLimit);
        }

        yield return new WaitForSeconds(timeLimit);
        onTrigger = false;
        showingMessage = false;

        StartCoroutine(fadeMessages.FadeOut(img, text));

        yield break;
    }
}
