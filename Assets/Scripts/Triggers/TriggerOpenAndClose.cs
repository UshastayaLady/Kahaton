using UnityEngine;

public class TriggerOpenAndClose : TriggerGoOut
{
    [SerializeField] private GameObject openAndClose;

    public override void OnTrigg()
    {
        openAndClose.SetActive(true);
    }
    public override void ExitTrigg()
    {
        openAndClose.SetActive(false);
    }
}
