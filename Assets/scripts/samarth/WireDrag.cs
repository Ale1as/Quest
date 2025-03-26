using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WireDrag : MonoBehaviour
{
    public GameObject wirePrefab; // Assign Wire prefab in Inspector
    public Transform[] rightWirePoints; // Assign RightWire_1 to RightWire_4 in Inspector

    private GameObject currentWire;
    private RectTransform wireRect;
    private bool isDragging = false;
    private Transform closestRightPoint;

    void Update()
    {
        if (isDragging && currentWire != null)
        {
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                Input.mousePosition,
                null,
                out mousePos
            );

            UpdateWire(mousePos);
        }
    }

    public void StartDrag()
    {
        if (currentWire == null)
        {
            currentWire = Instantiate(wirePrefab, transform.parent); // Create wire in UI
            wireRect = currentWire.GetComponent<RectTransform>();

            // Ensure the wire starts at the correct position
            wireRect.anchoredPosition = transform.localPosition;
        }
        isDragging = true;
    }

    public void StopDrag()
    {
        isDragging = false;

        if (currentWire == null)
        {
            Debug.LogError("currentWire is NULL!");
            return;
        }

        if (wireRect == null)
        {
            Debug.LogError("wireRect is NULL!");
            return;
        }

        closestRightPoint = FindClosestRightWirePoint();

        if (closestRightPoint != null && Vector2.Distance(closestRightPoint.position, wireRect.anchoredPosition) < 50f)
        {
            UpdateWire(closestRightPoint.localPosition);
        }
        else
        {
            Destroy(currentWire);
        }
    }

    void UpdateWire(Vector2 endPos)
    {
        if (wireRect == null) return; // Prevent errors

        Vector2 startPos = transform.localPosition; // Left Wire Start Position
        Vector2 direction = endPos - startPos;
        float distance = direction.magnitude;

        wireRect.sizeDelta = new Vector2(distance, 5); // Adjust width dynamically
        wireRect.pivot = new Vector2(0, 0.5f);
        wireRect.anchoredPosition = startPos; // Ensure it starts from Left Wire
        wireRect.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    Transform FindClosestRightWirePoint()
    {
        Transform closestPoint = null;
        float minDistance = Mathf.Infinity;

        Vector2 wirePos = wireRect.anchoredPosition; // Get wire UI position

        foreach (Transform rightPoint in rightWirePoints)
        {
            Vector2 rightPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                rightPoint.position,
                null,
                out rightPos
            );

            float distance = Vector2.Distance(wirePos, rightPos);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = rightPoint;
            }
        }
        return closestPoint;
    }
}
