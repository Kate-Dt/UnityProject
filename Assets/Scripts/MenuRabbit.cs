using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRabbit : MonoBehaviour {

    public float speed = 10;
    Rigidbody2D myBody = null;
    public Animator animator;
    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 20f;

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        LevelController.current.setStartPosition(transform.position);
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        float diff = Time.deltaTime;
        float value = Input.GetAxis("Horizontal");
        Vector3 from = transform.position + Vector3.up * 15f;
        Vector3 to = transform.position + Vector3.down * 5f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);

        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
            Vector2 vel = myBody.velocity;
            vel.x = value * speed * 5;
            myBody.velocity = vel;
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", true);
        }

        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            this.JumpActive = true;
        }

        if (this.JumpActive)
        {
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.33f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
    }
}

