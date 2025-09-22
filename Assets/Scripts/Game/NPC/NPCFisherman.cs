using UnityEngine;

public class NPCFisherman : NPC, ITalkable
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private DialogueController dialogueController;
    
    public override void Interact()
    {
        Talk(dialogue);
    }

    public void Talk(Dialogue dialogue)
    {
        dialogueController.ConversationController(dialogue); 
    }
}
