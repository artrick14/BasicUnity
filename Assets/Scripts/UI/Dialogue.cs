using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/NewDialogueContainer")]
public class Dialogue : ScriptableObject
{
    public string SpeakerName;
    public DialogueNode RootNode;
}