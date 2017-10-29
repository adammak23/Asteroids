using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    public int NumberOfAsteroids;
    public float IncrementAsteroidsPerLvl;
    public float InitialMaxThrust = 100;
    public float InitialMaxTurn = 100;
    public float PercentageOffsetOutsideScreen;
    public GameObject[] AsteroidsArr;


    void Start()
    {
        Init(NumberOfAsteroids);
    }

    void Update()
    {

    }

    void Init(int NumberOfAsteroids)
    {
        for (int i = 0; i < NumberOfAsteroids; i++)
        {
            GameObject Asteroid = (GameObject)Instantiate(AsteroidsArr[Random.Range(0, 8)]);
            Asteroid.GetComponent<Asteroids>().Size = 3;
            //disable collider and wrapping at the beginning
            Asteroid.GetComponent<ScreenWrapping>().IsOn = false;
            Asteroid.GetComponent<CapsuleCollider>().isTrigger = true;

            //position outside screen
            Vector3 PositionOutsideScreen = GenerateOutside();
            Vector2 StartPos = Camera.main.ScreenToWorldPoint(PositionOutsideScreen);
            Asteroid.transform.position = StartPos;

            //launch in the screen direction
            Vector3 TargetInsideScreen = new Vector2(Random.Range(0.01f, Screen.width), Random.Range(0.01f, Screen.height));
            TargetInsideScreen = Camera.main.ScreenToWorldPoint(TargetInsideScreen);
            Vector3 SemiRandomDirection = (TargetInsideScreen - Asteroid.transform.position).normalized;
            Asteroid.GetComponent<Rigidbody>().AddForce(SemiRandomDirection * InitialMaxThrust);
            Vector3 StartTurn = new Vector3(Random.Range(-InitialMaxTurn, InitialMaxTurn), Random.Range(-InitialMaxTurn, InitialMaxTurn), Random.Range(-InitialMaxTurn, InitialMaxTurn));
            Asteroid.GetComponent<Rigidbody>().AddTorque(StartTurn);
        }
    }
    Vector3 GenerateOutside()
    {
        float ScreenWidth = Screen.width;
        float ScreenHeight = Screen.height;
        float x=0;
        float y=0;


        x = Random.Range((0.1f - PercentageOffsetOutsideScreen) * ScreenWidth, ScreenWidth * (1.1f + PercentageOffsetOutsideScreen));

        if (x > 0 && x < ScreenWidth)
            if (Random.value < .5f)
            {
                y = Random.Range((0.1f - PercentageOffsetOutsideScreen) * ScreenHeight, 0f);
            }
            else
                y = Random.Range(ScreenHeight, ScreenHeight * (1.01f + PercentageOffsetOutsideScreen));
        return new Vector3(x, y, 0);
    }
    void RespawnAsteroids()
    {
        Init(NumberOfAsteroids);
    }
    void TryGoToNextLvl()
    {
        
        int AsteroidsNumber = GameObject.FindGameObjectsWithTag("asteroid").Length;
        if (AsteroidsNumber-1 == 0)
        {
            NumberOfAsteroids = (int)(NumberOfAsteroids * IncrementAsteroidsPerLvl);
            Init(NumberOfAsteroids);
        }
        
    }

}
