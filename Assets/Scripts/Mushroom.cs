using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{

    Vector3 rabbitScaleIncrement = new Vector3(0.5f, 0.5f, 0);

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        if (!HeroRabbit.isBig)
        {
            rabit.transform.localScale += rabbitScaleIncrement;
            HeroRabbit.isBig = true;
        }
        this.CollectedHide();
    }
}
