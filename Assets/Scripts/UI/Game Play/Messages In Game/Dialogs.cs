using UnityEngine;
using UnityEngine.UI;

public class Dialogs : Message
{

    Text dialog;
    bool firtsMessage;
    InputControl input;
    public PlayerController player;

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
            player.canMove = false;
            firtsMessage = false;
            dialog.text = ConsumeMessage();
        }

        if (!isDecision)
        {
            if (anyMessage && input.action1WasPress)
            {

                dialog.text = ConsumeMessage();

                if (messages.Count == 0)
                {
                    Invoke("FadeOutTransition", 2);
                }
            }
        }

        else
        {
            if (anyMessage && input.action3WasPress)
            {
                dialog.text = ConsumeMessage();

                if (messages.Count == 0)
                {
                    Invoke("FadeOutTransition", 2);
                }

                action.TakeAction(true);
                print("Good decision");
            }

            else if (anyMessage && input.action4WasPress)
            {
                dialog.text = ConsumeMessage();

                if (messages.Count == 0)
                {
                    Invoke("FadeOutTransition", 0.4f);
                }

                action.TakeAction(false);
                print("Bad decision");
            }
        }

    }

    private void FadeOutTransition()
    {
        if (img != null && text != null)
        {
            onTrigger = false;
            firtsMessage = true;

            StartCoroutine(fadeMessages.FadeOut(img, text));
            player.canMove = true;
        }
    }
}
