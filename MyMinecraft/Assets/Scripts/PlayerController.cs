using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject chunckPreFab;
    public int viewRange = 30;
	
	// Update is called once per frame
	void Update () {
        for (float x = transform.position.x - viewRange; x < transform.position.x + viewRange; x += Chunck.width)
        {
            for (float z = transform.position.z - viewRange; z < transform.position.z + viewRange; z += Chunck.height)
            {
                int xx = Mathf.FloorToInt(x / Chunck.width) * Chunck.width;
                int zz = Mathf.FloorToInt(z / Chunck.width) * Chunck.width;

                Chunck chunck = Chunck.GetChunck(Mathf.FloorToInt(xx), 0, Mathf.FloorToInt(zz));
                if (chunck == null)
                {
                    Instantiate(chunckPreFab, new Vector3(xx, 0, zz), Quaternion.identity);
                }
            }
        }
	}
}
