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
        Sun.transform.Rotate(Time.deltaTime*100,0,0);
        
    }
}
