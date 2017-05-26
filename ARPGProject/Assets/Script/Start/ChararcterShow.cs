using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChararcterShow : MonoBehaviour {

	// Use this for initialization
	void Start () {
		}
	
	// Update is called once per frame
	void Update () {
		}

    public void OnPress(bool isPress)
    {
        if (isPress == false)
        {
            StartMenuContraller._instance.OnCharacterClick(transform.parent.gameObject);
        }
    }
}
