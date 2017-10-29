using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HScoreUpdate : MonoBehaviour {

    public Text HighScore;

    void Start ()
    {
        HighScore.text = "Highest Score: " + PlayerPrefs.GetInt("highscore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
