using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour {

    public bool IsOn = true;
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        if(IsOn) Wrapping(this.gameObject);
    }
    void Wrapping(GameObject Object)
    {
        Vector2 newPos = Object.transform.position;
        if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height)
        {
            newPos.y = -(newPos.y - 0.1f);
        }
        if (Camera.main.WorldToScreenPoint(transform.position).y < 0)
        {
            newPos.y = -(newPos.y + 0.1f);
        }
        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width)
        {
            newPos.x = -(newPos.x - 0.1f);
        }
        if (Camera.main.WorldToScreenPoint(transform.position).x < 0)
        {
            newPos.x = -(newPos.x + 0.1f);
        }
        Object.transform.position = newPos;
    }
}
