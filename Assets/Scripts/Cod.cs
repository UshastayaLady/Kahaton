using UnityEngine.UI;
using UnityEngine;

public class PinCodeInput : MonoBehaviour
{
    [SerializeField] private GameObject open;
    [SerializeField] private GameObject close;
    [SerializeField] private GameObject close1;
    private string enteredPin = "";
    public string correctPinCode = "1234"; // Правильный PIN-код
    public Text displayText; // Текстовое поле для отображения введенного PIN-кода

    void Update()
    {
        // Проверяем нажатие клавиш от 0 до 9
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                EnterDigit(i.ToString());
            }
        }

        // Проверяем нажатие клавиши Enter для подтверждения ввода
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckPin();
        }

        // Проверяем нажатие клавиши Backspace для удаления последнего символа
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveLastDigit();
        }
    }

    void EnterDigit(string digit)
    {
        enteredPin += digit;
        UpdateDisplayText();
    }

    void CheckPin()
    {
        if (enteredPin == correctPinCode)
        {
            Debug.Log("PIN-код правильный!");
            TaskBoardManager.instance.TaskEndAndDelete("Ввести пароль в терминал");
            TaskBoardManager.instance.TaskEndAndDelete("Восстановить подачу сероводорода в капсулу");
            open.SetActive(true);
            close.SetActive(false);
            if(close1!=null)
                close1.SetActive(false);
            GameObject.Destroy(gameObject);
        }
        else
        {
            Debug.Log("Неверный PIN-код.");  
        }

        // Очищаем введенный PIN-код после проверки
        enteredPin = "";
        UpdateDisplayText();
    }

    void RemoveLastDigit()
    {
        if (enteredPin.Length > 0)
        {
            enteredPin = enteredPin.Substring(0, enteredPin.Length - 1);
            UpdateDisplayText();
        }
    }

    void UpdateDisplayText()
    {
        // Обновляем текстовое поле, отображая только символы '*'
        displayText.text = new string('*', enteredPin.Length);
    }
}