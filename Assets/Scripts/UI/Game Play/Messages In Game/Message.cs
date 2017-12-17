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

    public Color spriteColor;
    public Color textColor;

    public AudioClip clip;
    public FadeMethods fadeMessages = new FadeMethods();

    public bool isDecision;

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

    public void SetMessages(string[] msgs, bool decisionMsg = false)
    {
        if (decisionMsg)
        {
            isDecision = true;
        }
        else {
            isDecision = false;
        }

        anyMessage = true;
        messages = new Queue<string>(msgs);

        if (clip != null)
            AudioController.SetAndPlayAudioClip(clip, 0.3f);

        StartCoroutine(fadeMessages.FadeIn(img, text, spriteColor, textColor));
    }
}
