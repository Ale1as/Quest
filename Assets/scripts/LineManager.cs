using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LineManager : MonoBehaviour
{
    public GameObject linePrefab; // Prefab for the line
    public Transform canvasTransform; // Assign the Canvas in Inspector
    private DrawLine currentLine;
    private bool isDrawing = false;
    public MatchManager matchManager;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    private Transform nearestAns; // Store nearest "ans" object

    private GameObject selectedQues; // Store the actual "ques" object
    private GameObject selectedAns; // Store the actual "ans" object

    void Update()
    {
        if (isDrawing && currentLine != null)
        {
            // Make the line follow the mouse
            Vector2 mousePos = Input.mousePosition;
            currentLine.pointB.position = mousePos;

            // Find the nearest "ans" object
            nearestAns = FindNearestAnswer(mousePos);
        }

        // Start dragging on "ques"
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerData, results);

            if (results.Count > 0)
            {
                GameObject clickedObject = results[0].gameObject;

                if (clickedObject.CompareTag("ques") && !isDrawing)
                {
                    StartNewLine(clickedObject.transform);
                    selectedQues = clickedObject; // Store the actual "ques" object
                }
            }
        }

        // Release mouse → Snap to nearest "ans"
        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            if (nearestAns != null)
            {
                selectedAns = nearestAns.gameObject; // Store actual "ans" object
                CompleteLine(nearestAns);
            }
            else
            {
                CancelLine(); // Cancel if no "ans" is near
            }
        }
    }

    void StartNewLine(Transform startPoint)
    {
        if (matchManager.IsQuesMatched(startPoint.gameObject)) // ✅ Check if already matched
        {
            Debug.LogWarning($"{startPoint.name} is already matched. Cannot create another line.");
            return; // 🚫 Prevent multiple lines
        }

        // Instantiate a new line object
        GameObject newLine = Instantiate(linePrefab, canvasTransform);
        currentLine = newLine.GetComponent<DrawLine>();

        // Create UI elements for pointA and pointB
        GameObject pointAObj = new GameObject("PointA", typeof(RectTransform));
        GameObject pointBObj = new GameObject("PointB", typeof(RectTransform));

        // Set them as children of the canvas
        pointAObj.transform.SetParent(canvasTransform);
        pointBObj.transform.SetParent(canvasTransform);

        // Get RectTransform components
        RectTransform pointA = pointAObj.GetComponent<RectTransform>();
        RectTransform pointB = pointBObj.GetComponent<RectTransform>();

        // Assign to DrawLine script
        currentLine.pointA = pointA;
        currentLine.pointB = pointB;

        // Set initial positions
        pointA.position = startPoint.position;
        pointB.position = startPoint.position; // Initially same as start

        selectedQues = startPoint.gameObject; // Store original ques
        isDrawing = true;
    }


    void CompleteLine(Transform endPoint)
    {
        if (currentLine != null)
        {
            currentLine.pointB.position = endPoint.position;

            // **Fix: Pass the original "ques" and "ans" objects, not UI points**
            if (selectedQues != null && selectedAns != null)
            {
                matchManager.AddMatch(selectedQues, selectedAns);
                Debug.Log($"Matched: {selectedQues.name} → {selectedAns.name}"); // Debugging
            }

            isDrawing = false;
            currentLine = null; // Reset for next line
            selectedQues = null;
            selectedAns = null;
        }
    }

    void CancelLine()
    {
        if (currentLine != null)
        {
            Destroy(currentLine.gameObject); // Remove the line if no answer is nearby
            isDrawing = false;
        }
    }

    Transform FindNearestAnswer(Vector2 mousePos)
    {
        float minDistance = float.MaxValue;
        Transform nearest = null;
        GameObject[] answers = GameObject.FindGameObjectsWithTag("ans");

        foreach (GameObject ans in answers)
        {
            float distance = Vector2.Distance(mousePos, ans.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = ans.transform;
            }
        }

        return minDistance < 100f ? nearest : null; // Only snap if within 100 pixels
    }
}
