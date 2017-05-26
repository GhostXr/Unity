using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoDialog : MonoBehaviour {

    private UILabel titlLabel;
    private UILabel descLabel;
    private UILabel eneryTagLabel;
    private UILabel eneryLabel;

    private UIButton btnEnter;
    private UIButton btnClose;

    private TweenScale scaleTween;

    // Use this for initialization
    void Start () {
        titlLabel = transform.Find("tf_title").GetComponent<UILabel>();
        descLabel = transform.Find("Sprite/tf_desc").GetComponent<UILabel>();
        eneryTagLabel = transform.Find("Sprite/tf_tag_enery").GetComponent<UILabel>();
        eneryLabel = transform.Find("Sprite/tf_enery").GetComponent<UILabel>();

        btnEnter = transform.Find("Sprite/btn_start_game").GetComponent<UIButton>();
        btnClose = transform.Find("btn_close").GetComponent<UIButton>();

        scaleTween = this.GetComponent<TweenScale>();

        EventDelegate enterGame = new EventDelegate(this, "OnEnter");
        btnEnter.onClick.Add(enterGame);
        EventDelegate close = new EventDelegate(this, "OnClose");
        btnClose.onClick.Add(close);
    }

    void OnEnter()
    {
        scaleTween.PlayReverse();
    }

    void OnClose()
    {
        scaleTween.PlayReverse();
    }


    public void ShowDialog (BtnTranscript btn)
    {
        scaleTween.gameObject.SetActive(true);
        scaleTween.PlayForward();
        
        eneryTagLabel.enabled = true;
        eneryLabel.enabled = true;
        btnEnter.enabled = true;

        titlLabel.text = "第" + btn.id + "关";
        descLabel.text = btn.mapDesc;
        eneryLabel.text = btn.id+"";
    }

    public void ShowWarn(BtnTranscript btn)
    {
        scaleTween.gameObject.SetActive(true);
        scaleTween.PlayForward();
        
        eneryTagLabel.enabled = false;
        eneryLabel.enabled = false;
        btnEnter.enabled = false;

        descLabel.text = "等级不足，快去升级吧~";
    }
}
