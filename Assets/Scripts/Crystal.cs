using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{
    protected override void OnRabitHit(HeroRabbit rabit)
    {
        LevelController.current.addCrystals(1);
        this.CollectedHide();
    }
}
