using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {

    public Vector3 pointA, pointB;
    public float speed = 1;
    Vector3 currentPosition;
    Vector3 rabbitPosition;
    Rigidbody2D body = null;
    Animator animator = null;
    SpriteRenderer spriteRenderer = null;
    

    public enum Mode
    {
        GoToA,
        GoToB, 
        Attack,
        Die
    }

    Mode mode = Mode.GoToA;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void FixedUpdate () {

        currentPosition = this.transform.position;
 
        if (rabbitWon())
        {
            animator.SetTrigger("die");
            mode = Mode.Die;
        }
        if (readyToAttack())
        {
            mode = Mode.Attack;
            if (rabbitWon())
            {
                animator.SetTrigger("die");
                mode = Mode.Die;
            }
        }
        if (!readyToAttack() && mode == Mode.Attack)
        {
            animator.SetBool("attack", false);
            animator.SetBool("run", false);
            animator.SetBool("walk", true);
            mode = Mode.GoToA;
        }
        else if (mode == Mode.GoToA || mode == Mode.Attack)
        {
            if (isArrived(pointA))
            {
                mode = Mode.GoToB;
            }
        }
        else if (mode == Mode.GoToB || mode == Mode.Attack)
        {
            if (isArrived(pointB))
            {
                mode = Mode.GoToA;
            }
        }

        move();
        attack();
    }



    void move()
    {
        float value = getDirection();
        Vector2 velocity = body.velocity;
        if (value != 0)
        {
            velocity.x = value * speed;
            body.velocity = velocity;
        }
        if (value == -1)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    void attack()
    {
        if (mode == Mode.Attack)
        {
            animator.SetBool("run", true);
            animator.SetBool("walk", false);
            speed = 2.4f;
            if (readyToHit())
            {
                StartCoroutine(Hit());
                if (rabbitWon())
                { 
                 animator.SetTrigger("die");
                //animator.SetBool("run", false);
                //animator.SetBool("walk", false);
                //animator.SetBool("attack", false);
            }
                else
                {
                    StartCoroutine(DeathOfRabbit());
                }
            }
        }
    }

    bool readyToAttack()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        return rabbitPosition.x >= pointA.x && rabbitPosition.x <= pointB.x
            || rabbitPosition.x <= pointA.x && rabbitPosition.x >= pointB.x;
    }

    IEnumerator DeathOfRabbit()
    {
        yield return new WaitForSeconds(0.2f);

        HeroRabbit.isDead = true;
    }

    IEnumerator Hit()
    {
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(1f);

        speed = 2f;
        mode = Mode.GoToB;
        animator.SetBool("attack", false);
        animator.SetBool("run", false);
        animator.SetBool("walk", true);
    }

    bool rabbitWon()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        currentPosition = this.transform.position;
        return rabbitPosition.y > currentPosition.y + 0.8f && Mathf.Abs(rabbitPosition.x - currentPosition.x) < 1f 
            && HeroRabbit.isDead == false && rabbitPosition.y < currentPosition.y + 1.65;
    }

    bool readyToHit()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        currentPosition = this.transform.position;

        if (HeroRabbit.isBig)
        {
            return Mathf.Abs(rabbitPosition.x - currentPosition.x) < 1.8f
             && Mathf.Abs(rabbitPosition.y - currentPosition.y) < 3f;
        }
        else
        {
            return Mathf.Abs(rabbitPosition.x - currentPosition.x) < 0.9f
                && Mathf.Abs(rabbitPosition.y - currentPosition.y) < 1f;
        }
    }

    private bool isArrived(Vector3 destination)
    {
        return Mathf.Abs(currentPosition.x - destination.x) < 0.05f;
    }


    float getDirection()
    {
        currentPosition = this.transform.position;
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;

        if (mode == Mode.GoToA)
        {
            if (currentPosition.x < pointA.x)
            {
                return 1;
            } else
            {
                return -1;
            }
        } else if (mode == Mode.GoToB)
        {
            if (currentPosition.x < pointB.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        } else if (mode == Mode.Attack)
        {
            if (currentPosition.x < rabbitPosition.x)
            {
                return 1;
            } else
            {
                return -1;
            }
        }
        return 0;
    }
    
    void die()
    {
        Destroy(this.gameObject);
    }

}
