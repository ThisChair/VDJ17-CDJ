using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour {

    public enum MessageType {
        HINT,
        DIALOG
    }

    public MessageType messageType;

    private Message hint;
    private Message dialog;

    public MessagesAsset info;

    private void Start()
    {
        hint = GameObject.Find("HintText").GetComponent<Message>();
        dialog = GameObject.Find("DialogText").GetComponent<Message>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            if (messageType == MessageType.HINT)
            {
                hint.SetMessages(info.messages);
            }

            else if (messageType == MessageType.DIALOG) {
                dialog.SetMessages(info.messages);

            }
        }
    }
}
