using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : MonoBehaviour {

    public Vector3 pointA, pointB;
    public Vector3 radius;
    public float speed = 1;
    public float carrotRadius = 5.0f;
    Vector3 currentPosition;
    Vector3 rabbitPosition;
    Rigidbody2D body = null;
    Animator animator = null;
    SpriteRenderer spriteRenderer = null;
    float lastCarrot;
    float interval = 2;

    public GameObject prefabCarrot;

    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
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
        }
        if (isRabitWithinRadius())
        {
            mode = Mode.Attack;
            if (rabbitWon())
            {
                animator.SetTrigger("die");
            }
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

    private bool isArrived(Vector3 destination)
    {
        return Mathf.Abs(currentPosition.x - destination.x) < 0.05f;
    }

    void launchCarrot(Vector3 direction)
    {
        if (Time.time - lastCarrot > interval)
        {
            lastCarrot = Time.time;
            GameObject obj = GameObject.Instantiate(this.prefabCarrot);
            obj.transform.position = this.transform.position;

            Carrot carrot = obj.GetComponent<Carrot>();
            carrot.launch(direction);
        }
    }

    void attack()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        currentPosition = this.transform.position;
        if (isRabitWithinRadius())
        {
            speed = 0;
            animator.SetBool("attack", true);
            animator.SetBool("walk", false);
            launchCarrot(rabbitPosition - currentPosition);
       
        } else
        {
            speed = 1;
            animator.SetBool("attack", false);
            animator.SetBool("walk", true);
        }        
    }

    bool isRabitWithinRadius()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        currentPosition = this.transform.position;
        return (Mathf.Abs(rabbitPosition.x - currentPosition.x) < carrotRadius);
    }

    bool isRabbitBetweenPoints()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        return rabbitPosition.x >= pointA.x && rabbitPosition.x <= pointB.x
            || rabbitPosition.x <= pointA.x && rabbitPosition.x >= pointB.x;
    }

    bool rabbitWon()
    {
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;
        currentPosition = this.transform.position;
        return rabbitPosition.y > currentPosition.y + 0.9f && Mathf.Abs(rabbitPosition.x - currentPosition.x) < 1f
            && HeroRabbit.isDead == false && rabbitPosition.y < currentPosition.y + 1.65;
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

    float getDirection()
    {
        currentPosition = this.transform.position;
        rabbitPosition = HeroRabbit.lastRabbit.transform.position;

        if (mode == Mode.GoToA)
        {
            if (currentPosition.x < pointA.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else if (mode == Mode.GoToB)
        {
            if (currentPosition.x < pointB.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else if (mode == Mode.Attack)
        {
            if (currentPosition.x < rabbitPosition.x)
            {
                return 1;
            }
            else
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
