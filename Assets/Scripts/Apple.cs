using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addApples(1);
        this.CollectedHide();
    }
}
