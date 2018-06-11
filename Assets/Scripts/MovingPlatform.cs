using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public Vector3 MoveBy;
    
    Vector3 pointA;
    Vector3 pointB;
    Vector3 target;
    public static double time_to_wait = 0.1;
    bool going_to_a = false;
    public float speed;
    double initialWait = time_to_wait;
    double time_to_wait_copy;

    // Use this for initialization
    void Start () {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
    }
	
    bool isArrived (Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.02f;
    }

	// Update is called once per frame
	void FixedUpdate () {

        Vector3 my_pos = this.transform.position;

        if (going_to_a)
        {
            target = this.pointA;
        }
        else
        {
            target = this.pointB;
        }
        
        //Vector3 destination = target - my_pos;
        //destination.z = 0;

        time_to_wait_copy -= Time.deltaTime;
        if (time_to_wait_copy <= 0)
        {
            this.transform.position = Vector3.Lerp (my_pos, target, speed * Time.deltaTime);
            if (isArrived(this.transform.position, target))
            {
                going_to_a = !going_to_a;
                time_to_wait_copy = initialWait;
            }
        }
        
    }
}
