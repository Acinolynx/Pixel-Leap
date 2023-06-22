using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;          //Variable for character physic
    private SpriteRenderer sprite;   //Variable for rendering a character 
    private Animator anim;           //Variable for animation
    private bool isJumping;          //

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;    //Variable for movement speed (SerializeField so u can change it in the editor inside unity)
    [SerializeField] private float jumpForce = 14f;   //Variable for jump power (SerializeField so u can change it in the editor inside unity)

    private enum MovementState { idle, running, jumping, falling } //Variable for the animation
    
    
    // Start is called before the first frame update
    private void Start()
    {
       rb = GetComponent<Rigidbody2D>();         //code if the variable gets called, it called the correct thing
       sprite = GetComponent<SpriteRenderer>();  //code if the variable gets called, it called the correct thing
       anim = GetComponent<Animator>();          //code if the variable gets called, it called the correct thing
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");                          //code for movement in direction X (left, right)
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)                                //code for jumping when specific key is pressed
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
            }
        
            UpdateAnimationState();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
    }

    private void UpdateAnimationState()
    {
        MovementState state;                        //code for the animation

        if (dirX > 0f)
            {
                state = MovementState.running;      //code for the running animation    
                sprite.flipX = false;
            }
        
        else if (dirX < 0f)
            {
                state = MovementState.running;      //code for the running animation (flip character)
                sprite.flipX = true;
            }

        else
            {
                state = MovementState.idle;         //code for the idling animation
            }
        
        if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;      //code for the jumping animation
            }
        
        else if (rb.velocity.y < -.1f)
            {
                state = MovementState.falling;      //code for the falling animation
            }
            
        anim.SetInteger("state", (int)state);       //code for changing the bool var into int
    }
}
