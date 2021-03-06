﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Concentration : MonoBehaviour
{
    
    private Slider levelConcentration;
    private State state;
    private Object sd;
    private int mulpitplierScore;

    private void Start()
    {
        mulpitplierScore = 1;
        state = State.Disabled;
        levelConcentration = GetComponent<Slider>();
        Player.DetectEvent += PlayerСollisionWithPlatform;
    }

    private void PlayerСollisionWithPlatform(Player.TypeEvent type, Collider collider)
    {
        
        if (state == State.Disabled)
        {
            if (type == Player.TypeEvent.Abyss)
            {
                if (collider.tag != "Ground" && collider.tag != "Let")
                    IncreaseLevelConcentration();

            }
            else
            if (type == Player.TypeEvent.Let)
            {
                ResetLevelConcentration();
            }
            else
            if (type == Player.TypeEvent.Ground)
            {
                ResetLevelConcentration();
            }
        }
        else
        {

            if (type == Player.TypeEvent.Let)
            {
                ResetLevelConcentration();
            }
            else
            if (type == Player.TypeEvent.Ground)
            {
                if(sd)
                {
                    if(Object.ReferenceEquals(collider.gameObject, sd))
                    {
                        ResetLevelConcentration();
                    }
                    sd = collider.gameObject;
                }
            }
        }
    }

    public int Multiplier()
    {
        return mulpitplierScore;
    }
    
    public void IncreaseLevelConcentration()
    {
        levelConcentration.value += 0.1f;
        if (levelConcentration.value == 1)
        {
            state = State.Enabled;
            mulpitplierScore = 2;
        }
    }

    public void ResetLevelConcentration()
    {
        levelConcentration.value = 0;
        mulpitplierScore = 1;
        state = State.Disabled;
    }

    public enum State
    {
        Enabled,
        Disabled
    }
}
