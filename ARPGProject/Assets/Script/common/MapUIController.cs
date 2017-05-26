using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIController : MonoBehaviour {

    private static MapUIController _instance;
    private TweenPosition showTween;
    private MapInfoDialog dialog;

	// Use this for initialization
	void Start () {
        _instance = this;
        showTween = this.GetComponent<TweenPosition>();
        dialog = transform.Find("map_info_dialog").GetComponent<MapInfoDialog>();
    }

    void Show()
    {
        showTween.PlayForward();
    }

    void Hide()
    {
        showTween.PlayReverse();
    }

    // Update is called once per frame
    void OnClickEliteBtn(BtnTranscript btnMap) {
        if (btnMap.condition <= 2)
        {
            dialog.ShowDialog(btnMap);
        }
        else
        {
            dialog.ShowWarn(btnMap);
        }
	}

}
