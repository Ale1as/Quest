using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSwipe : MonoBehaviour
{
    public RectTransform card;
    public Image cardImage;
    public RectTransform startZone, endZone;
    public float minSwipeTime = 0.7f;
    public float maxSwipeTime = 0.8f;
    public TMP_Text text;

    private bool isSwiping = false;
    private float swipeStartTime;
    private Vector2 originalPosition;

    void Start()
    {
        originalPosition = card.position;
        cardImage.color = Color.white;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(card, Input.mousePosition))
            {
                isSwiping = true;
                swipeStartTime = Time.time;
            }
        }

        if (Input.GetMouseButton(0) && isSwiping)
        {
            Vector2 newPos = Input.mousePosition;
            card.position = new Vector2(newPos.x, originalPosition.y); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            float swipeDuration = Time.time - swipeStartTime;

            if (RectTransformUtility.RectangleContainsScreenPoint(endZone, Input.mousePosition))
            {
                if (swipeDuration >= minSwipeTime && swipeDuration <= maxSwipeTime)
                {
                    Debug.Log("Swipe Successful");
                    text.text = "Swipe Successful";
                    cardImage.color = new Color(0f, 1f ,0f) ;
                }
                else if (swipeDuration > maxSwipeTime)
                {
                    Debug.Log("Swipe Failed! Too Slow");
                    Debug.Log(swipeDuration);
                    text.text = "Swipe Failed! Too Slow";
                    cardImage.color = new Color(1.0f,0.0f,0.0f);
                }
                else
                {
                    Debug.Log("Swipe Failed! Too Fast");
                    Debug.Log(swipeDuration);
                    text.text = "Swipe Failed! Too Fast";
                    cardImage.color = new Color(1.0f ,1.0f ,0.0f) ;
                }
            }
            else
            {
                Debug.Log("Swipe Failed!");
                cardImage.color = Color.black;
            }
            
            card.position = originalPosition;
        }
    }
}