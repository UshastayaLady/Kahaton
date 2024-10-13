using UnityEngine;

public class TriggerOpenDoor : TriggerGoOut
{
    [SerializeField] private Animator dialogueAnim;
   
    public override void OnTrigg()
    {
        dialogueAnim.SetBool("OpenDoor", true);
    }
    public override void ExitTrigg()
    {
        dialogueAnim.SetBool("OpenDoor", false);
    }
    
}
