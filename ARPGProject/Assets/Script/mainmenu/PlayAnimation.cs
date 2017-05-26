using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

    private Animator animtor;

    void Start()
    {
        animtor = GetComponent<Animator>();
    }

    public void OnAttackButtonIsClick(bool isPress, PosType posType)
    {
        if (posType == PosType.Basic)
        {
            if(isPress == true)
            {
                animtor.SetTrigger("attack");
            }
        }
        else
        {
            if(posType == PosType.One)
                animtor.SetTrigger("skill1");
            else if(posType == PosType.Two)
                animtor.SetTrigger("skill2");
            else if (posType == PosType.Three)
                animtor.SetTrigger("skill3");
        }
    }
}
