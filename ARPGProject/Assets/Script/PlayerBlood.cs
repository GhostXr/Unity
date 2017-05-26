using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlood : MonoBehaviour {

    public static PlayerBlood _instance;
    public static PlayerBlood Instance
    {
        get { return _instance; }
    }

    private TweenAlpha tweenA;

	// Use this for initialization
	void Awake () {
        _instance = this;
        tweenA = transform.GetComponent<TweenAlpha>();
    }
	
	// Update is called once per frame
	public void ShowBloodScene () {
        tweenA.ResetToBeginning();
        tweenA.PlayForward();
    }
}
