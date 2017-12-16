using UnityEngine.UI;

public class Dialogs : Message
{
    Text dialog;
    InputControl input;

    private void Start()
    {
        input = FindObjectOfType<InputControl>();
        dialog = GetComponent<Text>();
    }

    private void Update()
    {
        if (anyMessage && input.action1WasPress)
        {
            dialog.text = ConsumeMessage();
        }
    }



}
