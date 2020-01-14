using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    public int life;
    public int maxLife;
    public float lifeStats;
    public bool switchPhase;

    protected override void Awake()
    {
        maxLife = life;
        switchPhase = false;
    }

    public virtual void MouvRandom()
    {

    }
    

    

}
