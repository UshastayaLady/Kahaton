using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class PlayerTakeItem : MonoBehaviour
{
    private Camera playerCamera;
    private float interactRange = 1f; // Радиус для поиска объектов
    private GameObject nearestObject; // Ближайший объект для взаимодействия
    
    void Start()
    {
        playerCamera = Camera.main; // Получаем ссылку на камеру игрока
    }

    void Update()
    {
        // Проверяем, была ли нажата кнопка взаимодействия
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Находим все коллайдеры в радиусе взаимодействия
            Collider[] colliders = Physics.OverlapSphere(playerCamera.transform.position, interactRange);

            float closestDistance = Mathf.Infinity;
            nearestObject = null;

            // Перебираем найденные коллайдеры
            foreach (Collider collider in colliders)
            {
                // Проверяем тег объекта
                if (collider.CompareTag("Take"))
                {
                    // Вычисляем расстояние до игрока
                    float distance = Vector3.Distance(playerCamera.transform.position, collider.transform.position);

                    // Если найден объект ближе, чем текущий ближайший, обновляем ближайший объект
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestObject = collider.gameObject;
                    }
                }
            }

            // Если есть ближайший объект и была нажата кнопка взаимодействия, уничтожаем его
            if (nearestObject != null)
            {
                InventoryManager.instance.AddItem(nearestObject, nearestObject.name);
            }
        }
    }    
}
