using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hints : Message
{
    Text hint;
    private bool showingMessage;

    private void Start()
    {
        hint = GetComponent<Text>();
    }

    void FixedUpdate () {
        if (anyMessage && !showingMessage) {
            showingMessage = true;
            StartCoroutine(DisplayMessages());
        }
    }


    private IEnumerator DisplayMessages() {

        while (anyMessage) {
            hint.text = ConsumeMessage();
            yield return new WaitForSeconds(2);

        }

        showingMessage = false;
        yield break;
    }
}
