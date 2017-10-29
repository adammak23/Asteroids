using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{

    public Rigidbody RB;
    public float thrust;
    public float turn;
    public GameObject bullet;
    public float bulletForce;
    public GameObject PlayerScore;
    public GameObject DeathExplosion;
    public ParticleSystem EngineFlame;
    public GameObject spawner;
    public AudioSource AudioSource;

    private float thrustInput;
    private float turnInput;
    private bool Dead;

    void Start()
    {
        EngineFlame.Stop();
        PlayerScore = GameObject.Find("PlayerScore");
    }

    void Update()
    {
        if ( (Input.GetAxis("Vertical") > 0.1f ))
        {
            EngineFlame.Play();
        }
        else
        {
            EngineFlame.Stop();
        }
        if (Dead)
        {
            EngineFlame.Stop();
        }

        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //fire
        if (Input.GetButtonDown("Fire1") && !Dead)
        {
            GameObject newBullet = Instantiate(bullet, transform.position+transform.up*0.5f, transform.rotation);
            //speeding up bullets with force + ship velocity
            newBullet.GetComponent<Rigidbody>().AddRelativeForce(-transform.up * bulletForce - RB.velocity*bulletForce*0.1f);
            Destroy(newBullet, 2f);
        }
    }

    void FixedUpdate()
    {
        RB.AddRelativeForce(Vector2.up * thrustInput);
        transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turn);
    }
    void OnCollisionEnter(Collision col)
    {
        Death();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other);
            Death();
        }
    }

    void Death()
    {
        PlayerScore.SendMessage("Death");
        Instantiate(DeathExplosion, transform.position, transform.rotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        RB.isKinematic = true;
        Dead = true;

        //Destroying all asteroids
        int AsteroidsNumber = GameObject.FindGameObjectsWithTag("asteroid").Length;
        for (int i = 0; i < AsteroidsNumber; i++)
        {
            Destroy(GameObject.FindGameObjectsWithTag("asteroid")[i]);
        }

    }
    void Respawn(int RespawnTime)
    {
        AudioSource.PlayDelayed(0.5f);
        Invoke("RespawnFunction", RespawnTime);
    }
    void RespawnFunction()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        transform.position = Vector3.zero;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        RB.isKinematic = false;
        Dead = false;
        //respawning asteroids
        spawner.SendMessage("RespawnAsteroids");

    }

}
