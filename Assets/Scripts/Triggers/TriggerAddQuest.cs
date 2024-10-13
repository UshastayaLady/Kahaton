using UnityEngine;

public class TriggerAddQuest : Trigger
{    
    [SerializeField] private string textNewQuest;
    public override void Make()
    {
        TaskBoardManager.instance.AddTask(textNewQuest);
    }
}
