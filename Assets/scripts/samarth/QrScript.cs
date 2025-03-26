using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QrScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHolding = false;
    public float holdTime = 1.0f; // Time required to hold
    private float timer = 0f;
    public Slider holdSlider; // Reference to the UI Slider
    public GameObject paidText;

    void Start()
    {
        if (holdSlider != null)
            holdSlider.value = 0f;
    }

    void Update()
    {
        if (isHolding)
        {
            timer += Time.deltaTime;
            if (timer >= holdTime)
            {
                Debug.Log($"Image held: {gameObject.name}");
                paidText.SetActive(true);
                isHolding = false; // Prevent multiple logs
                gameObject.transform.parent.gameObject.SetActive(false);

            }
        }
        else
        {
            if (timer > 0f)
                timer -= Time.deltaTime * 0.5f; // Decrease timer at a slower rate when not holding
        }

        if (holdSlider != null)
            holdSlider.value = timer / holdTime; // Update slider progress
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }
}