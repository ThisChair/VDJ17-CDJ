using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {

    public enum MessageType {
        HINT,
        DIALOG,
        ACTION_BAR
    }

    public bool visited;

    public MessageType messageType;

    private Message hint;
    private Message dialog;
    private ActionBarUI actionBar;

    public MessagesAsset info;

    private void Start()
    {
        hint = GameObject.Find("HintText").GetComponent<Message>();
        dialog = GameObject.Find("DialogText").GetComponent<Message>();
        actionBar = GameObject.Find("Action Bar").GetComponent<ActionBarUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !visited) {

            visited = true;

            if (messageType == MessageType.HINT)
            {
                hint.SetMessages(info.messages);
            }

            else if (messageType == MessageType.DIALOG) {
                dialog.SetMessages(info.messages, info.isDecision);

            }

            else if (messageType == MessageType.ACTION_BAR)
            {
                actionBar.SetTurn();
            }
        }
    }
}
