using UnityEngine;

public class TriggerAddQuest : Trigger
{
    [SerializeField] private GameObject Open;
    public override void Make()
    {
        Open.SetActive(true);
    }
}
