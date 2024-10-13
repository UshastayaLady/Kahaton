using UnityEngine;

public class TriggerCheckItems : Trigger
{
    [SerializeField] private string nameObject;
    [SerializeField] private GameObject Open;
    public override void Make()
    {
        if (InventoryManager.instance.FindItem("nameObject"))
        {
            InventoryManager.instance.DeleteItem("nameObject" , 1);
            Open.SetActive(true);
        }
        
    }
}
