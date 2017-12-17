using UnityEngine.UI;

public class Dialogs : Message
{

    Text dialog;
    bool firtsMessage;
    InputControl input;

    public override void Start()
    {
        base.Start();
        input = FindObjectOfType<InputControl>();
        dialog = GetComponent<Text>();
    }

    private void Update()
    {

        if (anyMessage && firtsMessage)
        {
            dialog.text = ConsumeMessage();
            firtsMessage = false;
        }

        if (anyMessage && input.action1WasPress)
        {
            dialog.text = ConsumeMessage();
        }

        if (!anyMessage) {
            onTrigger = false;
            firtsMessage = true;
            
            Invoke("FadeOutTransition", 1);
        }
    }

    private void FadeOutTransition() {
        if (img != null && text != null) {
            // StartCoroutine(fadeMessages.FadeOut(img, text));
        }
    }
}
