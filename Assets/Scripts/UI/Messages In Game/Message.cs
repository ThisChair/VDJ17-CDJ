using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

    public bool anyMessage;

    public string message;
    public Queue<string> mesagges;

    public string ConsumeMessage() {
        anyMessage = mesagges.Count > 1 ? true : false;
        return message = mesagges.Dequeue();
    }

    public void SetMessages(string[] msgs) {
        anyMessage = true;
        mesagges = new Queue<string>(msgs);
    }

}
