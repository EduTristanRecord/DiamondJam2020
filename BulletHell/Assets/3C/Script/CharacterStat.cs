using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CharacterStat : MonoBehaviour
{
    public static CharacterStat instance;
    public enum State {Move,BulletTime,Death};

    [Header("PlayerVariables")]
    public State state;
    public int pv;
    public int pvMax;
    public float timerDeath;
    public string tagBullet;
    public float pm;
    public float slowTimer;
    public float fRalenti;

    [Header("input")]
    public int playerId = 0;
    private Player player;

    [Header("move")]
    private Rigidbody2D rb2d;
    public float maxSpeed = 300; // vitesse maximale
    public float acceleration = 1000; // acceleration avant vitesse max
    public float speedFactor = 0;//vitesse player
    public float frotement = 1000;// ralentissement player
    public float rotationSpeed = 150f;//vitesse de rotation
    private float moveHorizontal;
    private float moveVertical;
    private float orientz = 1f;
    private float orientx = 1f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        rb2d = GetComponent<Rigidbody2D>();
        state = State.Move;
        pv = pvMax;
        pm = slowTimer;
    }

    void Update()
    {
        UpdateControl();
        //UpdateMove();
        if (player.GetButtonDown("SlowMotion"))
        {
            ChangeState(State.BulletTime);
            DoBulletTime();
        }
        if (player.GetButtonUp("SlowMotion"))
        {
            ChangeState(State.Move);
            ReleaseTime();
        }
        if (state == State.BulletTime)
        {
            CheckSpell();

            if (pm > 0)
            {
                pm -= Time.deltaTime;
            }
            else
            {
                pm = 0;
                ChangeState(State.Move);
            }
        }

        if (pm < slowTimer)
        {
            pm += Time.deltaTime;

            if (pm > slowTimer)
            {
                pm = slowTimer;
            }
        }
    }
    
    //Fonction for Variable
    public void ChangeState(State NewState)
    {
        //Change état du player
        state = NewState;
    }

    public void ChangePv(int diff)
    {
        //Degat et soin
        pv += diff;
    }

    private void DoBulletTime()
    {
        //ralenti
        //Time.timeScale = fRalenti;
        maxSpeed /= 10;
        acceleration /= 10;
        frotement *= 10;
    }

    private void ReleaseTime()
    {
        //Time.timeScale = 1.0f;
        maxSpeed *= 10;
        acceleration *= 10;
        frotement /= 10;
    }

    //Fonction for damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagBullet)
        {
            ChangePv(-1);
            if (pv == 0)
            {
                ChangeState(State.Death);
            }
        }
    }

    //Fonction for move
    void UpdateControl()
    {
        //input déplacement axe x et z
        moveHorizontal = player.GetAxis("Move Horizontal");
        moveVertical = player.GetAxis("Move Vertical");

        // déplace le player avec accéleration et décélération
        if (Mathf.Abs(moveVertical) > 0.1f || Mathf.Abs(moveHorizontal) > 0.1f)
        {

            // vérification que le joystick gauche n'est pas utilisé
            if (speedFactor > 0 && (orientz * moveVertical < 0 && orientx * moveHorizontal < 0))
            {
                speedFactor -= frotement * Time.deltaTime;
            }
            else
            {

                orientz = Mathf.Sign(moveVertical);
                orientx = Mathf.Sign(moveHorizontal);
                // fait accélérer le joueur jusqu'à ce qu'il atteigne sa vitesse max
                speedFactor += acceleration * Time.deltaTime;
                if (speedFactor > maxSpeed)
                {
                    speedFactor = maxSpeed;
                }
            }
        }
        else
        {
            // fait ralentir le joueur jusqu'à ce que ca vitesse atteigne 0
            speedFactor -= frotement * Time.deltaTime;
            if (speedFactor < 0)
            {
                speedFactor = 0;
            }
        }


        

        rb2d.velocity = new Vector2(moveHorizontal, moveVertical) * speedFactor;

    }

    void UpdateMove()
    {
        // déplace le player avec accéleration et décélération
        if (Mathf.Abs(moveVertical) > 0.1f || Mathf.Abs(moveHorizontal) > 0.1f)
        {
            
            //oriente le pplayer en fonction de l'orientation du joystick gauche
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(-moveHorizontal, moveVertical) * Mathf.Rad2Deg);
            
            // vérification que le joystick gauche n'est pas utilisé
            if (speedFactor > 0 && (orientz * moveVertical < 0 && orientx * moveHorizontal < 0))
            {
                speedFactor -= frotement * Time.deltaTime;
            }
            else
            {
                orientz = Mathf.Sign(moveVertical);
                orientx = Mathf.Sign(moveHorizontal);
                // fait accélérer le joueur jusqu'à ce qu'il atteigne sa vitesse max
                speedFactor += acceleration * Time.deltaTime;
                if (speedFactor > maxSpeed)
                {
                    speedFactor = maxSpeed;
                }
            }
        }
        else
        {
            // fait ralentir le joueur jusqu'à ce que ca vitesse atteigne 0
            speedFactor -= frotement * Time.deltaTime;
            if (speedFactor < 0)
            {
                speedFactor = 0;
            }
        }
        // ajoute une force au rigidbody, le forward permet de déplacer le player tout en effectuant des rotations
        // rb2d.AddForce(transform.forward * speedFactor, ForceMode.Force);
        rb2d.velocity = transform.up * speedFactor;
    }

    public void CheckSpell()
    {

    }
}
