using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJulien : MonoBehaviour
{

    public GameObject Bullet;
    public float speed;
    List<Vector3> DirectionBullet = new List<Vector3>();
    public float time;
    public float test;

    private void Start()
    {
        //StartCoroutine(Fire());
    }

    void Update()
    {
        DirectionBullet = DrawAngle(test);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < DirectionBullet.Count; i++)
        {
                Fire(DirectionBullet[i]);
            }
        }

        for (int i = 0; i < DirectionBullet.Count; i++)
        {
            Debug.DrawLine(transform.position, DirectionBullet[i], Color.red, 0f);
        }

    }

    List<Vector3> DrawAngle(float angle)
    {
        float testA = angle;
        List<Vector3> Dir = new List<Vector3>();
        for (int i = 0; i < angle/10; i++)
        {
            testA = testA - 10;
            Dir.Add(Quaternion.Euler(0, 0, testA) * (transform.up * -10));
            Dir.Add(Quaternion.Euler(0, 0, testA*-1) * (transform.up * -10));
        }
        for(int i = 0; i < Dir.Count-1; i++)
        {
            if(Dir[i] == Dir[i+1])
            {
                Dir.RemoveAt(i);
            }
        }
        
        return Dir;
    }
    void Fire(Vector3 direction)
    {
        GameObject tampon = Instantiate(Bullet, transform.position, Quaternion.identity);
        tampon.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
    }
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < DirectionBullet.Count; i++)
        {
            Fire(DirectionBullet[i]);
        }
        StartCoroutine(Fire());
    }


    void FireAngle(float Angle, float Speed, GameObject Bullet)
    {        
        GameObject tampon = Instantiate(Bullet, transform.position, Quaternion.identity);
        tampon.GetComponent<Rigidbody2D>().velocity = (Quaternion.Euler(0, 0, Angle) * (transform.up * -1)).normalized * Speed;
    }
    void FireDir(Vector2 posTarget, float Speed, GameObject Bullet)
    {
        GameObject tampon = Instantiate(Bullet, transform.position, Quaternion.identity);
        tampon.GetComponent<Rigidbody2D>().velocity = new Vector2(posTarget.x - transform.position.x, posTarget.y - transform.position.y).normalized * Speed;
    }

}
