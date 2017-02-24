using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCycle : MonoBehaviour {


    public Light Sun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Sun.transform.Rotate(Time.deltaTime * 10, 0, 0);
        if (Sun.transform.eulerAngles.x == 360)
        {
            Sun.transform.Rotate(0, 0, 0);
        }

        if (Sun.transform.eulerAngles.x > 181)
        {
            Sun.enabled = false;
        }
        if (Sun.transform.eulerAngles.x > 359)
        {
            Sun.enabled = true;
        }

    }
}
