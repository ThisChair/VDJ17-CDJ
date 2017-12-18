using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    [HideInInspector] public bool onTrigger;
    [HideInInspector] public bool anyMessage;

    [HideInInspector] public string message;
    public Queue<string> messages;

    [HideInInspector] public Image img;
    [HideInInspector] public Text text;
    public Text controlText;

    public Color spriteColor;
    public Color textColor;

    public AudioClip clip;
    public FadeMethods fadeMessages = new FadeMethods();

    [HideInInspector] public bool isDecision;
    [HideInInspector] public string text2;
    [HideInInspector] public TriggerEvent action;

    public virtual void Start()
    {
        img = transform.parent.GetComponent<Image>();
        text = GetComponent<Text>();
    }

    public string ConsumeMessage()
    {
        anyMessage = messages.Count > 1 ? true : false;
        return message = messages.Dequeue();
    }

    public void SetMessages(string[] msgs, bool decisionMsg = false, TriggerEvent eventGO = null, bool control = false)
    {
        if (decisionMsg)
        {
            isDecision = true;
            action = eventGO;
            text2 = "Press Y or N to answer";
        }
        else {
            isDecision = false;
            action = null;
            text2 = "Press Space key to continue";
        }

        anyMessage = true;
        messages = new Queue<string>(msgs);

        if (clip != null) {
            AudioController.SetAndPlayAudioClip(clip, 0.1f);
        }

        if (!control)
        {
            StartCoroutine(fadeMessages.FadeIn(img, text, spriteColor, textColor));
        }
        else {
            StartCoroutine(fadeMessages.FadeIn(img, text, spriteColor, textColor, controlText));
        }
    }
}
