﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public string username;
    public bool human;
    public HUD hud;
    public WorldObject SelectedObject { get; set; }

    void Start () {
        hud = GetComponentInChildren<HUD>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
