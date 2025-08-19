using UnityEngine;

public class TempTestPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    public BoxCollider2D player_collider;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private byte jumps = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
	player_collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        // Jump
        if (Input.GetButtonDown("Jump")){
		if(isGrounded || jumps > 0){
			if(!isGrounded) jumps -= 1;
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
			Debug.Log("JUMP!");
			isGrounded = false;
		}
	}
    }

    void FixedUpdate()
    {
        // Move player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision){
	    Debug.Log("Colliding!");
	    GameObject collision_object = collision.gameObject;

	    if(collision_object.name == "Floor"){
		    isGrounded = true;
		    jumps += 1;
	    } 
    }

    void OnDrawGizmosSelected()
    {
        // Visualize ground check
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
