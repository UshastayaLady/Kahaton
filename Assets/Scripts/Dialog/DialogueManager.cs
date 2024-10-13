using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private FirstPersonController FPS;
    [SerializeField] private Animator dialogueAnim;
    [SerializeField] public bool dialogueClosed = true;
    public static DialogueManager instance = null;

    public void Start()
    {
        if (instance == null)
        { instance = this; }

        FPS = FindObjectOfType<FirstPersonController>();
    }        

    public void StartDialogue()
    {
        dialogueAnim.SetBool("dialogueOpen", true);
        CursorManager.instance.cursorWork = true;
        dialogueClosed = false;
        FPS.setFreeze(true);
    }

    public void EndDialogue()
    {
        InstantiateDialogue.instance.CloseDialogue();
        dialogueAnim.SetBool("dialogueOpen", false);
        CursorManager.instance.cursorWork = false;
        dialogueClosed = true;
        FPS.setFreeze(false);
    }
}
