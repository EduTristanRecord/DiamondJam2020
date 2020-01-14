using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    public int life;
    public int maxLife;
    public float lifeStats;
    public bool switchPhase;
    public int countAttack;

    protected override void Awake()
    {
        maxLife = life;
        switchPhase = false;
    }

    public virtual void InitMouv(List<Transform> point)
    {
        transform.position = point[Random.Range(0, 3)].position;
    }

    public virtual void MouvRandom(List<Transform> point)
    {
        List<Transform> tamponPoint = point; 
        for (int i = 0; i < point.Count; i++)
        {
            if(transform.position == tamponPoint[i].position)
            {
                tamponPoint.RemoveAt(i);
            }
        }

        transform.position = tamponPoint[Random.Range(0, 2)].position;
    }

    public virtual void FireAngle(float Angle, float Speed, GameObject Bullet)
    {
        GameObject tampon = Instantiate(Bullet, transform.position, Quaternion.identity);
        tampon.GetComponent<Rigidbody2D>().velocity = (Quaternion.Euler(0, 0, Angle) * (transform.up * -1)).normalized * Speed;
    }
    public virtual void FireDir(Vector2 posTarget, float Speed, GameObject Bullet)
    {
        GameObject tampon = Instantiate(Bullet, transform.position, Quaternion.identity);
        tampon.GetComponent<Rigidbody2D>().velocity = new Vector2(posTarget.x - transform.position.x, posTarget.y - transform.position.y).normalized * Speed;
    }


}
