using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    AudioSourceManager asm;

    //movement var
    public float speed;
    public float jumpForce;

    //groundcheck stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask isGroundLayer;
    public float groundCheckRadius;



    //soundclips
    public AudioClip jumpSound;


    Coroutine jumpForceChange;

    public int maxScore = 10;
    private int _score = 0;

    public int maxLives = 5;
    private int _lives = 3;

    public int lives
    {
        get { return _lives; }
        set
        {
            _lives = value;

            if (_lives > maxLives)
            {
                _lives = maxLives;
            }

            Debug.Log("Lives have been set to: " + _lives.ToString());
        }

    }

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            if (_score > maxScore)
            {
                _score = maxScore;
            }

            Debug.Log("Score is now: " + _score.ToString());
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        asm = GetComponent<AudioSourceManager>();

        if (speed <= 0 )
        {
            speed = 6.0f;
            Debug.Log("Speed was set incorrect, defaulting to " + speed.ToString());
        }

        if (jumpForce <= 0)
        {
            jumpForce = 350;
            Debug.Log("Jump force was set incorrect, defaulting to " + jumpForce.ToString());
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.2f;
            Debug.Log("Ground Check Radius was set incorrect, defaulting to " + groundCheckRadius.ToString());
        }

        if(!groundCheck) 
        {
            groundCheck = GameObject.FindGameObjectWithTag("GroundCheck").transform;
            Debug.Log("Ground Check not set, finding it manually!");

        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClip = anim.GetCurrentAnimatorClipInfo(0);
        float hInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        
        if (curPlayingClip.Length > 0)
        {
            if (Input.GetButtonDown("Fire1") && curPlayingClip[0].clip.name != "Fire" )
            {
                anim.SetTrigger("Fire");
            }
            else if (curPlayingClip[0].clip.name == "Fire")
            {
                rb.velocity = Vector2.zero;
            }
            else
            {
                Vector2 moveDirection = new Vector2(hInput * speed, rb.velocity.y);
                rb.velocity = moveDirection;
            }
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            asm.PlayOneShot(jumpSound, false);
        }
        
        if (isGrounded && Input.GetButton("Vertical"))
        {
            anim.SetTrigger("Climbing");
        }
        
        
        anim.SetFloat("hinput", Mathf.Abs(hInput));
        anim.SetBool("IsGrounded", isGrounded);
        
        
        //check for flipped and create an algorithm to flip your character
        
        if (hInput != 0)
        {
            sr.flipX = (hInput > 0);
        }

    }

    public void IncreaseGravity()
    {
        rb.gravityScale = 5;
    }

    public void StartJumpForceChange()
    {
        if (jumpForceChange == null)
        {
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
        else
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce /= 2;
            jumpForceChange = StartCoroutine(JumpForceChange());
        }
    }

    IEnumerator JumpForceChange()
    {
        jumpForce *= 2;

        
        yield return new WaitForSeconds(5.0f);

        jumpForce /= 2;
        jumpForceChange = null;

    }
}
