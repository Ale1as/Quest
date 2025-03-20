using System.Collections;
using UnityEngine;

public class interactprompt : MonoBehaviour
{
    public GameObject door_prompt;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door_prompt.SetActive(true);
            StartCoroutine(HidePromptAfterDelay());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door_prompt.SetActive(false);
            StopCoroutine(HidePromptAfterDelay()); // Stop coroutine if player exits early
        }
    }

    IEnumerator HidePromptAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        door_prompt.SetActive(false);
    }
}
