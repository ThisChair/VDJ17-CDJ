using UnityEngine.UI;

public class Dialogs : Message
{

    Text dialog;
    bool firtsMessage;
    InputControl input;

    public override void Start()
    {
        base.Start();
        firtsMessage = true;
        dialog = GetComponent<Text>();
        input = FindObjectOfType<InputControl>();
    }

    private void Update()
    {

        if (anyMessage && firtsMessage)
        {
            firtsMessage = false;
            dialog.text = ConsumeMessage();
        }

        if (anyMessage && input.action1WasPress)
        {

            dialog.text = ConsumeMessage();

            if (messages.Count == 0)
            {
                Invoke("FadeOutTransition", 2);
            }
        }
    }

    private void FadeOutTransition() {
        if (img != null && text != null) {

            onTrigger = false;
            firtsMessage = true;

            StartCoroutine(fadeMessages.FadeOut(img, text));

        }
    }
}
