using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable
{

    Vector3 rabbitScaleIncrement = new Vector3(0.5f, 0.5f, 0);

    protected override void OnRabitHit(HeroRabbit rabit)
    {
        if (!rabit.isBig)
        {
            rabit.transform.localScale += rabbitScaleIncrement;
            rabit.isBig = true;
        }
        this.CollectedHide();
    }
}
