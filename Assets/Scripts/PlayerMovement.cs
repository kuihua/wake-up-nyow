using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float horizontalInput;

    [SerializeField] private Transform cameraTarget;

    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private LayerMask groundLayer;

    private SpriteRenderer sr;
    [SerializeField] private GameObject leftReach, rightReach;

    [SerializeField] private AudioClip meowClip;
    [SerializeField] private AudioClip jumpClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rightReach.SetActive(true);
        leftReach.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(horizontalInput > 0) {
            sr.flipX = false;
            rightReach.SetActive(true);
            leftReach.SetActive(false);
        }
        else if(horizontalInput < 0) {
            sr.flipX = true;
            leftReach.SetActive(true);
            rightReach.SetActive(false);
        }

        // jump
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundFXManager.instance.PlaySoundFXClip(jumpClip, transform, 0.1f);
            // rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        && rb.velocity.y > 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y/2);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SoundFXManager.instance.PlaySoundFXClip(meowClip, transform, .5f);
        }

        // if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        // && rb.linearVelocity.y > 0) {
        //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y / 2);
        // }

        cameraTarget.position = new Vector2(transform.position.x, cameraTarget.position.y);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        // rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }

    private bool isGrounded() {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    // prob have to delete before build
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
