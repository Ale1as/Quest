using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private BlockPlacer blockPlacer;
    private MiningArea miningArea;

    private int currentBlocks;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        blockPlacer = FindAnyObjectByType<BlockPlacer>();
        miningArea = GetComponentInChildren<MiningArea>();
    }

    void Update()
    {
        Move();
        Jump();
        HandleMining();
    }

    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // Get movement input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleMining()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click to place a block
        {
            if (currentBlocks == 0) return;
            blockPlacer.PlaceBlock();
            currentBlocks--;
        }
        if (Input.GetMouseButtonDown(1)) // Right-click to remove a block
        {
            miningArea.MineBlock();
            currentBlocks++;
            
        }
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to mine
        {
            blockPlacer.RemoveBlock();
        }
    }
}