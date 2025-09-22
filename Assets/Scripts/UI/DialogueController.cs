using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private GameObject ResponseButtonPrefab;
    [SerializeField] private Transform ResponseButtonContainer;
    [SerializeField] private float typeSpeed;
    DialogueNode currentNode;
    private Queue<string> dialogueQueue = new Queue<string>();
    private string dialogueLine;
    private bool startOfConversation = true;
    private bool conversationEnded;
    private bool isTyping;
    private bool responsesPresent = false;
    private Coroutine typeDialogueCoroutine;
    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    public void ConversationController(Dialogue dialogue)
    {
        if (startOfConversation)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            NPCNameText.text = dialogue.SpeakerName;
            currentNode = dialogue.RootNode;
        }

        startOfConversation = false;

        ConversationIterator(currentNode);

    }

    public void ConversationIterator(DialogueNode currentNode)
    {
        if (!responsesPresent)
        {
            if (dialogueQueue.Count == 0)
            {
                if (conversationEnded == false)
                {
                    foreach (string line in currentNode.dialogueText)
                    {
                        dialogueQueue.Enqueue(line);
                    }
                }
                else if (conversationEnded && !isTyping)
                {
                    EndConversation();
                    return;
                }
            }

            if (!isTyping)
            {
                dialogueLine = dialogueQueue.Dequeue();
                typeDialogueCoroutine = StartCoroutine(TypeDialogueText(dialogueLine));
            }
            else if (isTyping)
            {
                FinishParagraphEarly();
            }
        }

        if (!responsesPresent && dialogueQueue.Count == 0)
        {
            DisplayResponses(currentNode);
        }
    }

    private void DisplayResponses(DialogueNode currentNode)
    {
        foreach (DialogueResponse response in currentNode.responses)
        {
            GameObject button = Instantiate(ResponseButtonPrefab, ResponseButtonContainer);
            responsesPresent = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = response.responseText;
            button.GetComponent<Button>().onClick.AddListener(() => DestroyResponses());
            button.GetComponent<Button>().onClick.AddListener(() => ResponseHandler(response));
        }
    }

    public void ResponseHandler(DialogueResponse response)
    {
        if (response.nextNode != null)
        {
            ConversationIterator(response.nextNode);
        }
        else
        {
            EndConversation();
        }
    }

    public void DestroyResponses()
    {
        foreach (Transform child in ResponseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        responsesPresent = false;
    }

    private void EndConversation()
    {
        conversationEnded = true;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);

        NPCDialogueText.text = dialogueLine;

        isTyping = false;
    }
    
    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }
}
