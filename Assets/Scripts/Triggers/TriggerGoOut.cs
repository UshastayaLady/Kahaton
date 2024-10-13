using UnityEngine;

abstract public class TriggerGoOut : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        OnTrigg();
    }
    private void OnTriggerExit(Collider other)
    {
        ExitTrigg();
    }

    public virtual void OnTrigg()
    {
        
    }
    public virtual void ExitTrigg()
    {

    }
}
