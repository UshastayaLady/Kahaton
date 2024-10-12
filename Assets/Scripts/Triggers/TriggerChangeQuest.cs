using UnityEngine;

public class TriggerChangeQuest : Trigger
{
    [SerializeField] private string textChangeQuest;
    [SerializeField] private string textOldQuest;
    public override void Make()
    {
        if (TaskBoardManager.instance.FindTaskFromBoard(textOldQuest))
        {
            TaskBoardManager.instance.TaskEndAndDelete(textOldQuest);
            TaskBoardManager.instance.AddTask(textChangeQuest);
        }
        
    }
}
