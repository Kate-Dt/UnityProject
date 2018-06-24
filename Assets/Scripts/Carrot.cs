using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    Rigidbody2D rigidBody;
    Vector3 currentPosition;
    float speed = 3;

    // Use this for initialization
    void Start () {
        StartCoroutine(DestroyLater());
	}

    IEnumerator DestroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
	
	public void launch(Vector3 direction)
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        currentPosition = this.transform.position;

        rigidBody.velocity = direction.normalized * speed;

        if (direction.x >= 0)
        {

            this.transform.position += new Vector3(1, 1, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.transform.position += new Vector3(-1, 1, 0);
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
