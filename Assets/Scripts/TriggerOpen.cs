using UnityEngine;

public class TriggerOpen : Trigger
{
    private GameObject Open;
    public override void Make()
    {
        Open.SetActive(true);
    }
}
