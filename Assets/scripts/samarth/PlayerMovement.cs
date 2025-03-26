using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public bool canJump = false;
    public bool canInteract = false;
    public bool canPause = false; // ✅ New variable to control pausing
    public GameObject MiniGame;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isPaused = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isPaused)
        {
            // Always allow left-right movement
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            // Jump if allowed and grounded
            if (canJump && isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            // Interact if allowed
            if (canInteract && Input.GetKeyDown(KeyCode.E))
            {
                TryInteract();
            }

            // Toggle MiniGame
            if (Input.GetKeyDown(KeyCode.V))
            {
                MiniGame.SetActive(!MiniGame.activeInHierarchy);
            }
        }

        // Pause game if allowed
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void TryInteract()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f; // ✅ Pause/unpause game
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }
}
