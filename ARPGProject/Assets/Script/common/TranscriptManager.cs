using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranscriptManager : MonoBehaviour {

    public static TranscriptManager _instance;
    public List<GameObject> enemyList = new List<GameObject>();

    public GameObject player; 

	// Use this for initialization
	void Awake ()
    {
        _instance = this;
        player = GameObject.FindGameObjectWithTag("Player");

    }
	
	// Update is called once per frame
	void start ()
    {
    }
}
