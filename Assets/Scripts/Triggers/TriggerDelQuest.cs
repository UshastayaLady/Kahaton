using UnityEngine;

public class TriggerDelQuest : Trigger
{
    [SerializeField] private string textOldQuest;
    public override void Make()
    {
        if (TaskBoardManager.instance.FindTaskFromBoard(textOldQuest))
        {
            TaskBoardManager.instance.TaskEndAndDelete(textOldQuest);
        }

    }
}
