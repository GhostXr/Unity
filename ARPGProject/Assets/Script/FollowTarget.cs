using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Vector3 offset;
    public float speed = 1;
    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("Bip01");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(player != null)
        {
            Vector3 tragetPos = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, tragetPos, speed*Time.deltaTime);
        }
            //transform.position = player.position + offset;
	}
}
