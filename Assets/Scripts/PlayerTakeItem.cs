using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class PlayerTakeItem : MonoBehaviour
{
    private Camera playerCamera;
    private float interactRange = 1f; // ������ ��� ������ ��������
    private GameObject nearestObject; // ��������� ������ ��� ��������������
    
    void Start()
    {
        playerCamera = Camera.main; // �������� ������ �� ������ ������
    }

    void Update()
    {
        // ���������, ���� �� ������ ������ ��������������
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ������� ��� ���������� � ������� ��������������
            Collider[] colliders = Physics.OverlapSphere(playerCamera.transform.position, interactRange);

            float closestDistance = Mathf.Infinity;
            nearestObject = null;

            // ���������� ��������� ����������
            foreach (Collider collider in colliders)
            {
                // ��������� ��� �������
                if (collider.CompareTag("Take"))
                {
                    // ��������� ���������� �� ������
                    float distance = Vector3.Distance(playerCamera.transform.position, collider.transform.position);

                    // ���� ������ ������ �����, ��� ������� ���������, ��������� ��������� ������
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestObject = collider.gameObject;
                    }
                }
            }

            // ���� ���� ��������� ������ � ���� ������ ������ ��������������, ���������� ���
            if (nearestObject != null)
            {
                InventoryManager.instance.AddItem(nearestObject, nearestObject.name);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCamera.transform.position, interactRange);
    }
}
