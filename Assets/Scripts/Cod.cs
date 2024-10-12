using UnityEngine.UI;
using UnityEngine;

public class PinCodeInput : MonoBehaviour
{
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
            // Здесь можно добавить дополнительную логику для успешного ввода
        }
        else
        {
            Debug.Log("Неверный PIN-код.");
            // Здесь можно добавить дополнительную логику для неверного ввода
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