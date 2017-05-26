using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffect : MonoBehaviour {

    private Renderer[] rendererArray;
    private NcCurveAnimation[] curvAnimArray;
    private GameObject effectOffset;

	// Use this for initialization
	void Start () {
        rendererArray = GetComponentsInChildren<Renderer>();
        curvAnimArray = GetComponentsInChildren<NcCurveAnimation>();
        if(transform.Find("EffectOffset"))
        {
            effectOffset = transform.Find("EffectOffset").gameObject;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //ShowEffect();
        }
    }

    public void ShowEffect()
    {
        if(effectOffset != null)
        {
            effectOffset.SetActive(false);
            effectOffset.SetActive(true);
        }
        else
        {
            foreach (Renderer renderer in rendererArray)
            {
                renderer.enabled = true;
            }
            foreach (NcCurveAnimation ncAnim in curvAnimArray)
            {
                ncAnim.ResetAnimation();
            }
        }
    }

    public void ShowStrikeEffect()
    {
         
    }
}
