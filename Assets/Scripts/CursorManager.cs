using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D customCursor; // ѕеременна€ дл€ хранени€ текстуры курсора
    private Vector2 hotSpot = new Vector2(0, 0); // “очка прив€зки курсора (если хочетс€ настроить добавить SerializeField)

    public static CursorManager instance = null;
    public bool cursorWork = true;

    public void Start()
    {
        if (instance == null)
        { instance = this; }
        if(customCursor!=null)       
            Cursor.SetCursor(customCursor, hotSpot, CursorMode.Auto); // ”становка кастомного курсора
    }
   
    void Update()
    {
        if (cursorWork)
        { 
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ClikButtonFalse()
    {
        cursorWork = false;
    }
}
