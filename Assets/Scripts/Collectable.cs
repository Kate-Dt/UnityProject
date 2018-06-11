using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    protected bool hideAnimation;

    protected virtual void OnRabitHit(HeroRabbit rabbit)
    { }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!this.hideAnimation)
        {
            HeroRabbit rabbit = collider.GetComponent<HeroRabbit>();
            if (rabbit != null)
            {
                this.OnRabitHit(rabbit);
            }
        }
    }
        public void CollectedHide()
        {
            Destroy(this.gameObject);
        }
    }

