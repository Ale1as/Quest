using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour
{
    public RectTransform pointA;
    public RectTransform pointB;
    public Image lineImage;

    void Update()
    {
        if (pointA == null || pointB == null) return;

        RectTransform canvasRect = lineImage.rectTransform.parent as RectTransform;

        // Convert world positions to local UI space
        Vector2 localA, localB;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointA.position, null, out localA);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, pointB.position, null, out localB);

        // Compute direction and distance
        Vector2 dir = localB - localA;
        float distance = dir.magnitude;

        // Apply correct size and positioning
        lineImage.rectTransform.sizeDelta = new Vector2(distance, 5f); // Set correct line length
        lineImage.rectTransform.anchoredPosition = (localA + localB) / 2; // ✅ Center the line properly
        lineImage.rectTransform.pivot = new Vector2(0.5f, 0.5f); // ✅ Center the pivot to avoid misalignment
        lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg); // ✅ Correct rotation
    }



}
