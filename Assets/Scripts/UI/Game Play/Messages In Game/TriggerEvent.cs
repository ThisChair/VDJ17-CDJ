using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{

    public enum EventNum {
        ONE,
        TWO,
        THREE,
        FOUR
    }

    public enum MessageType
    {
        HINT,
        DIALOG,
        ACTION_BAR
    }

    [HideInInspector] public bool visited;

    public EventNum enventNum;

    public MessageType messageType;

    private Message hint;
    private Message dialog;
    private ActionBarUI actionBar;

    public MessagesAsset info;
    public MessagesAsset mesajeAux;

    public GameObject desapearGO;

    private void Start()
    {
        hint = GameObject.Find("HintText").GetComponent<Message>();
        dialog = GameObject.Find("DialogText").GetComponent<Message>();
        actionBar = GameObject.Find("Action Bar").GetComponent<ActionBarUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !visited)
        {

            visited = true;

            if (messageType == MessageType.HINT)
            {
                hint.SetMessages(info.messages);
            }

            else if (messageType == MessageType.DIALOG)
            {
                dialog.SetMessages(info.messages, info.isDecision, this);

            }

            else if (messageType == MessageType.ACTION_BAR)
            {
                actionBar.SetTurn();
            }
        }
    }


    public void TakeAction(bool decision)
    {

        // Good decision
        if (decision)
        {
            switch (enventNum) {
                case EventNum.ONE:
                    actionBar.SetTurn();
                    break;

                case EventNum.TWO:
                    actionBar.SetTurn();
                    break;

                case EventNum.THREE:
                    actionBar.SetTurn();
                    break;

                case EventNum.FOUR:
                    break;
            }
        }

        // Bad Decision
        else
        {
            switch (enventNum)
            {
                case EventNum.ONE:
                    Destroy(desapearGO);
                    dialog.SetMessages(mesajeAux.messages);

                    break;

                case EventNum.TWO:
                    dialog.SetMessages(mesajeAux.messages);

                    break;

                case EventNum.THREE:
                    break;

                case EventNum.FOUR:
                    break;
            }
        }
    }
}
