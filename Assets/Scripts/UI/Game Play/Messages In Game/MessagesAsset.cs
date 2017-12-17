using UnityEngine;

[CreateAssetMenu(menuName = "Create Message List")]
public class MessagesAsset : ScriptableObject {

    public string[] messages;

    public bool isDecision = true;
}
