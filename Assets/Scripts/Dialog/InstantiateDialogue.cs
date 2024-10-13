using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateDialogue : MonoBehaviour
{
    #region variables
    public static InstantiateDialogue instance = null;
    public Dialogue dialogue;    
    public TaskBoardManager questsManager;
    public InventoryManager itemsManager;

    [SerializeField] private GameObject Window;
        
    [SerializeField] private Text nodeText;
    [SerializeField] private Text firstAnswer;
    [SerializeField] private Text secondAnswer;
    [SerializeField] private Text thirdAnswer;
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private Button thirdButton;

    [HideInInspector] public bool dialogueEnded = false; //�������� �� ������?
    private bool firstNodeShown = false; //1 ��� ��� �������?
    private bool questFalse = false; //����� �� ��������?

    [HideInInspector]public TextAsset ta;

    private int currentNode = 0;    
    [HideInInspector] public int butClicked;
    [HideInInspector] public bool objectSetActiv = false;
    #endregion

    #region 
    void Start()
    {
        if (instance == null)
        { instance = this; }        

        secondButton.enabled = false;
        thirdButton.enabled = false;
       
        firstButton.onClick.AddListener(but1);
        secondButton.onClick.AddListener(but2);
        thirdButton.onClick.AddListener(but3);
        
    }

    private void Update()
    {
        if (ta!=null)
        {
            if (dialogueEnded == false)
            {
                if (!firstNodeShown)
                {
                    firstStart();
                }

            }
        }
                   
    }

    #region // Buttons
    private void but1()
    {
        butClicked = 0;
        AnswerClicked(0);
    }
    private void but2()
    {
        butClicked = 1;
        AnswerClicked(1);
    }
    private void but3()
    {
        butClicked = 2;
        AnswerClicked(2);
    }
    #endregion
        
    private void firstStart()
    {
        objectSetActiv = false;
        dialogue = null;       
        dialogue = Dialogue.Load(ta);
        currentNode = 0;
        firstNodeShown = true;
        WriteText();
    }    
   
    private void WriteText()
    {
        deleteDialogue(); //��������� ����� ������ ������ ��� ����� ������� ������ ������ ������ ���
        nodeText.text = dialogue.nodes[currentNode].Npctext;
      
        firstAnswer.text = dialogue.nodes[currentNode].answers[0].text;     //������ ����� ����� ������            
        if (dialogue.nodes[currentNode].answers.Length >= 2)                //���� ������� ���
        {
            secondButton.enabled = true;
            secondAnswer.text = dialogue.nodes[currentNode].answers[1].text;    //���������� 
        }
        else
        {
            secondButton.enabled = false;                                       //����� ��������
            secondAnswer.text = "";
        }

        if (dialogue.nodes[currentNode].answers.Length == 3)
        {
            thirdButton.enabled = true;
            thirdAnswer.text = dialogue.nodes[currentNode].answers[2].text;
        }
        else
        {
            thirdButton.enabled = false;
            thirdAnswer.text = "";
        }
    }

    private void AnswerClicked(int numberOfButton)
    {
        if (!DialogueManager.instance.dialogueClosed)
        {
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests != null)
            {
                WorkWithQuests(numberOfButton);
            }

            if (dialogue.nodes[currentNode].answers[numberOfButton].after == "true")
            {
                DialogueManager.instance.EndDialogue();
                StartCoroutine(waitFor(2f));
            }
            else if (dialogue.nodes[currentNode].answers[numberOfButton].end == "true")
            {
                dialogueEnded = true;
                DialogueManager.instance.EndDialogue();
            }
            else
            {
                currentNode = dialogue.nodes[currentNode].answers[numberOfButton].nextNode;
                if (questFalse)
                    currentNode = currentNode - 1;
                questFalse = false;
                WriteText();
            }
        }
    }
    #endregion

    private void WorkWithQuests(int numberOfButton)
    {
        for (int questNumber = 0; questNumber < dialogue.nodes[currentNode].answers[numberOfButton].quests.Length; questNumber++)
        {
            // �������� ������
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].textQuest != null)
            {
                if (!questsManager.FindTaskFromBoard(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].textQuest))
                    questsManager.AddTask(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].textQuest);
            }
            // ���� ���� ������ ���������� � ��� � �� ���� ����� ��������
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questEndAndDelete != null)
            {
                if (questsManager.FindTaskFromBoard(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questEndAndDelete))
                    questsManager.TaskEndAndDelete(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questEndAndDelete);

            }
            // ���� ���������� ����� ����������� ����� ��� �� �������� "���������"
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questDone != null)
            {
                if (questsManager.FindTaskFromBoard(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questDone))
                    if (questsManager.FindStatusTaskFromBoard(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questDone, "��������"))
                    {
                        questsManager.TaskEndAndDelete(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questDone);
                    }
                    else
                    {
                        questFalse = true;
                    }

            }
            // ���� ����� �������� ������ ������ ����� �������
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].textNewStatus != null)
            {
                if (questsManager.FindTaskFromBoard(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questChangeStatus))
                    questsManager.UpdateTaskStatus(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].questChangeStatus,
                        dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].textNewStatus);
            }
            // �������� ������
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].gameObjectSetActiv == "true")
            {
                objectSetActiv = true;
            }
            // ���� ����� �������� ������ ������ ����� �������
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items != null)
            {
                WorkWithItems(numberOfButton, questNumber);
            }
        }
    }
    private void WorkWithItems(int numberOfButton, int questNumber)
    {
        Dictionary<string, int> items = new Dictionary<string, int>();
        for (int itemNumber = 0; itemNumber < dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items.Length; itemNumber++)
        {
            if (dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items[itemNumber].gameObjectTake != null &
                dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items[itemNumber].gameObjectTakeCount > 0)
            {
                items.Add(dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items[itemNumber].gameObjectTake,
               dialogue.nodes[currentNode].answers[numberOfButton].quests[questNumber].items[itemNumber].gameObjectTakeCount);
            }
        }
        if (itemsManager.FindItems(items))
        {
            itemsManager.DeleteItems(items);
            currentNode = currentNode + 2;
            WriteText();

        }
        else
        {
            currentNode = currentNode + 1;
            WriteText();
        }
    }

    private IEnumerator waitFor(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void CloseDialogue()
    {

        if (dialogue != null)
        {
            dialogue = null;
        }
        deleteDialogue();
        currentNode = 0; // �������� � ������� ����
        firstNodeShown = false; // ���������� ���� ������ ������� ����
    }

    private void deleteDialogue()
    {
        secondButton.enabled = false;
        thirdButton.enabled = false;
        nodeText.text = "";
        firstAnswer.text = "";
        secondAnswer.text = "";
        thirdAnswer.text = "";
    }
}