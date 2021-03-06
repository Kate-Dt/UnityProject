﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current;
    Vector3 startingPosition;

    void Awake()
    {
        current = this;
    }

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public void onRabbitDeath(HeroRabbit rabbit)
    {

        rabbit.transform.position = this.startingPosition;
    }

    public void addCoins(int number)
    {

    }

    public void addApples(int number)
    {

    }

    public void addCrystals(int number)
    {

    }
}
