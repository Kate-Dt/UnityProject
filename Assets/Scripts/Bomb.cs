using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable
{

    Vector3 rabbitScaleDecrement = new Vector3(0.5f, 0.5f, 0);

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        if (HeroRabbit.isBig)
        {
            rabit.transform.localScale -= rabbitScaleDecrement;
            HeroRabbit.isBig = false;
        } else
        {
           rabit.die();
        }
        this.CollectedHide();
    }
}
