using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    Rigidbody2D rb;
    public float speed;
    float horizontalMove = 0f;

    
    private float boostTimer;
    private bool boosting;

    public float jumpForce;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    private bool isFacingRight;

    private int extraJumps;
    public int extraJumpsValue;

    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = false;

        speed = 10f;
        boostTimer = 0;
        boosting = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, 0f);


        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                speed = 10f;
                boostTimer = 0;
                boosting = false;
            }
        }


        if (x == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        Vector3 theScale = transform.localScale;
        Vector3 characterScale = transform.localScale;
        //if (Input.GetAxis("Horizontal") < 0)
        //{
        //    isFacingRight = false;
        //    //characterScale.x = -1;
                      

        //}
        //if (Input.GetAxis("Horizontal") > 0)
        //{
        //    isFacingRight = true;
        //    //characterScale.x = 1;
            
        //}

        if(isFacingRight == false && Input.GetAxis("Horizontal") > 0)
        {
            theScale.x *= -1;
            isFacingRight = !isFacingRight;
        }
        if (isFacingRight == true && Input.GetAxis("Horizontal") < 0)
        {
            isFacingRight = !isFacingRight;
            theScale.x *= -1;
        }


        //transform.localScale = new Vector2(Mathf.Sign(Input.GetAxis("Horizontal")), 0.3f);
        transform.localScale = theScale;

        Jump();
        CheckIfGrounded();
    }

    void FixedUpdate()
    {
        
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        
      
        transform.Translate(Input.GetAxis("Horizontal") * 15f * Time.deltaTime, 0f, 0f);
        if (x == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }

        Quaternion characterRotation = transform.localRotation;
        if (moveBy < -0.1f)
        {
            characterRotation.y = 180f;
        }
        if (moveBy > 0.1f)
        {
            characterRotation.y = 0f;
        }
        transform.localRotation = characterRotation;


        //if (moveBy < 0)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 180, 0);
        //}
        //else if (moveBy > 0)
        //{
        //    transform.localRotation = Quaternion.Euler(0, 0, 0);
        //}
    }

   

    void Jump()
    {
        if(isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            extraJumps--;
        } else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //        extraJumps--;
        //    }
        //} else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0)
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //    }
        //}
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if (collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "SpeedBoost")
        {
            boosting = true;
            speed = 20f;
            Destroy(other.gameObject);
        }
    }

}

