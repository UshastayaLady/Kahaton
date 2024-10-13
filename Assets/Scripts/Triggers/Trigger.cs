using UnityEngine;

abstract public class Trigger : MonoBehaviour
{
    private bool enter = false;
    private void Update()
    {
        if (enter && Input.GetKeyDown(KeyCode.E))
        {
            Make();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enter = false;
        }
    }

    public virtual void Make()
    {

    }
}
