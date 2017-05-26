using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollwerTarget : MonoBehaviour {

    public Transform player1;
    public Transform player2;
    public float Distance;

    private Vector3 offset;
    private Camera camera;

    // Use this for initialization
    void Start () {
        offset = transform.position - (player1.position + player2.position) / 2;
        camera = this.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {

        if (player1 && player2)
        {
            transform.position = offset + (player1.position + player2.position) / 2;
            float distance = Vector3.Distance(player1.position, player2.position);
            float size = distance * Distance;
            if (size < 10)
            {
                size = 10;
            }
            camera.orthographicSize = size;
        }
        else if (player1 && player2 == null)
        {
            transform.position = offset + player1.position;
            camera.orthographicSize = 10;
        }
        else if (player2 && player1 == null)
        {
            transform.position = offset + player2.position;
            camera.orthographicSize = 10;
        }
    }
}
