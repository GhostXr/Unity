using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour {

    public PosType posType = PosType.Basic;
    public float coldTime = 8;
    public float coldTimer = 0;
    
    private UISprite maskSprite;
    private PlayAnimation playAnimation;
    private BoxCollider boxCollider;
    private UIButton button;

    void Awake()
    {
        if(transform.FindChild("mask") != null)
        {
            maskSprite = transform.FindChild("mask").GetComponent<UISprite>();
        }
        boxCollider = this.GetComponent<BoxCollider>();
        button = this.GetComponent<UIButton>();
    }

    void Start()
    {
        playAnimation = TranscriptManager._instance.player.GetComponent<PlayAnimation>();
    }

    void Update()
    {
        if (maskSprite == null)
        {
            return;
        }
        if(coldTimer > 0)
        {
            coldTimer -= Time.deltaTime;
            maskSprite.fillAmount = coldTimer / coldTime;
            if(coldTimer <= 0)
            {
                ButtonEnable();
            }
        }
        else
        {
            maskSprite.fillAmount = 0;
        }
    }

    void OnPress(bool isPress)
    {
        playAnimation.OnAttackButtonIsClick(isPress, posType);
        if(isPress == true && maskSprite != null)
        {
            coldTimer = coldTime;
            ButtonDisable();
        }
    }

    void ButtonDisable()
    {
        boxCollider.enabled = false;
        button.SetState(UIButton.State.Normal, true);
    }

    void ButtonEnable()
    {
        boxCollider.enabled = true;
    }
}