using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHelpth : MonoBehaviour {

    public int hp = 100;
    public GameObject tankExplosionPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TankDanmeg()
    {
        if (hp <= 0)
        {
            return;
        }

        hp -= Random.Range(10, 20);
        if (hp <= 0)
        {
            GameObject.Instantiate(tankExplosionPrefab, transform.position + Vector3.up, transform.rotation);
            GameObject.Destroy(this.gameObject);
        }

    }
}
