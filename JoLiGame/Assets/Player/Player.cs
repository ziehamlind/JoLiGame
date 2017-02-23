using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string username;
    public bool human;
    public HUD hud;
    public WorldObject SelectedObject { get; set; }
    public Light Sun;

    void Start () {
        hud = GetComponentInChildren<HUD>();
    }
	
	// Update is called once per frame
	void Update () {
        Sun.transform.Rotate(Time.deltaTime * 10, 0, 0);
        if (Sun.transform.eulerAngles.x==360)
        {
            Sun.transform.Rotate(0,0,0);  
        }

        if (Sun.transform.eulerAngles.x > 183 )
        {
            Sun.enabled = false;
        }
        if (Sun.transform.eulerAngles.x > 357)
        {
            Sun.enabled = true;
        }


    }
}
