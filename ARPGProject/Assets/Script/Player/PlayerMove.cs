using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float velocity = 5.0f;

    private Rigidbody rigibody;
    private Animator anim;

    // Use this for initialization
    void Start () {
        rigibody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState") == false)
        {
            return;
        }

        float h = -Input.GetAxis("Horizontal");
        float v = -Input.GetAxis("Vertical");
        Vector3 nowV3 = rigibody.velocity;

        if (Mathf.Abs(h) > 0.005f || Mathf.Abs(v) > 0.005f)
        {
            anim.SetBool("move",true);
            rigibody.velocity = new Vector3(velocity * -h, nowV3.y, velocity * -v);
            transform.LookAt(new Vector3(h, 0, v) + transform.position);
        }
        else
        {
            anim.SetBool("move", false);
            rigibody.velocity = new Vector3(0, nowV3.y, 0);
        }
    }
}
