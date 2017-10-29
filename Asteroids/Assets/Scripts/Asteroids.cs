using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float AdditionalThrust;
    public float AdditionalTurn;
    public Rigidbody RB;
    public GameObject[] AsteroidsArr;
    public float Size;
    public GameObject PlayerScore;
    public GameObject AsteroidCollisionParticles;
    public GameObject AsteroidDeathParticles;
    public AudioClip SmallSound;
    public AudioClip MediumSound;
    public AudioClip BigSound;
    public AudioSource Sound;



    void Start()
    {
        if (Size == 3) transform.localScale = new Vector3(70, 70, 70);
        if (Size == 2) transform.localScale = new Vector3(50, 50, 50);
        if (Size == 1) transform.localScale = new Vector3(20, 20, 20);

        if (Size != 3)
        {
            RB.AddRelativeForce(Random.Range(-AdditionalThrust, AdditionalThrust), Random.Range(-AdditionalThrust, AdditionalThrust), 0);
            RB.AddTorque(Random.Range(-AdditionalTurn, AdditionalTurn), Random.Range(-AdditionalTurn, AdditionalTurn), 0);
        }

        PlayerScore = GameObject.Find("PlayerScore");
    }

    void Update()
    {
        Vector2 AsteroidPos = Camera.main.WorldToScreenPoint(transform.position);
        if (AsteroidPos.y < Screen.height && AsteroidPos.y > 0 && AsteroidPos.x < Screen.width && AsteroidPos.x > 0)
        {
            //after asteroid enters screen, enable wrapping and collision with others
            this.GetComponent<ScreenWrapping>().IsOn = true;
            this.GetComponent<CapsuleCollider>().isTrigger = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);
            if (Size == 3)
            {
                GameObject MediumAsteroid1 = Instantiate(AsteroidsArr[Random.Range(0, 8)]);
                MediumAsteroid1.GetComponent<Asteroids>().Size = 2;
                MediumAsteroid1.transform.position = transform.position;
                GameObject MediumAsteroid2 = Instantiate(AsteroidsArr[Random.Range(0, 8)]);
                MediumAsteroid2.GetComponent<Asteroids>().Size = 2;
                MediumAsteroid2.transform.position = transform.position;
                GameObject DestroyedAsteroidParticles = Instantiate(AsteroidDeathParticles, transform.position, transform.rotation);
                DestroyedAsteroidParticles.transform.localScale = new Vector3(2f, 2f, 2f);
                AudioSource.PlayClipAtPoint(BigSound,Vector3.zero);
            }

            else if (Size == 2)
            {
                GameObject SmallAsteroid1 = Instantiate(AsteroidsArr[Random.Range(0, 8)]);
                SmallAsteroid1.GetComponent<Asteroids>().Size = 1;
                SmallAsteroid1.transform.position = transform.position;
                GameObject SmallAsteroid2 = Instantiate(AsteroidsArr[Random.Range(0, 8)]);
                SmallAsteroid2.GetComponent<Asteroids>().Size = 1;
                SmallAsteroid2.transform.position = transform.position;
                GameObject DestroyedAsteroidParticles = Instantiate(AsteroidDeathParticles, transform.position, transform.rotation);
                DestroyedAsteroidParticles.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                AudioSource.PlayClipAtPoint(MediumSound, Vector3.zero);
            }

            else if (Size == 1)
            {
                GameObject DestroyedAsteroidParticles = Instantiate(AsteroidDeathParticles, transform.position, transform.rotation);
                DestroyedAsteroidParticles.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                AudioSource.PlayClipAtPoint(SmallSound, Vector3.zero);
            }
            PlayerScore.SendMessage("ScorePoints", Size);
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        ContactPoint point = other.contacts[0];
        Instantiate(AsteroidCollisionParticles, point.point, transform.rotation);

    }
}
