using UnityEngine;

public class TriggerOpen : Trigger
{
    [SerializeField] private GameObject Open;
    public override void Make()
    {
        Open.SetActive(true);
    }
}
