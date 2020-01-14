using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    public Physic physic;
    public View view;
    private Animateur anim;

    void Awake()
    {
        physic = new Physic();
        physic.rigidbody = GetComponent<Rigidbody2D>();
        view = new View();
        view.display = GetComponent<SpriteRenderer>();
        anim = new Animateur();
        anim.animator = GetComponent<Animator>();
    }
}
public class Physic
{
    public Rigidbody2D rigidbody;
}
public class View
{
    public SpriteRenderer display;
}
public class Animateur
{
    public Animator animator;
}