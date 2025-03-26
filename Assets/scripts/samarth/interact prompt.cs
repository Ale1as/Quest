using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class interactprompt : MonoBehaviour
{
    public GameObject prompt;
    public TextMeshProUGUI promptText;
    public String HeadsUpPrompt;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(true);
            promptText.text = HeadsUpPrompt;
            StartCoroutine(HidePromptAfterDelay());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            prompt.SetActive(false);
            StopCoroutine(HidePromptAfterDelay()); // Stop coroutine if player exits early
        }
    }

    IEnumerator HidePromptAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        prompt.SetActive(false);
    }
}
