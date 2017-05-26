using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {

    private UILabel level;
    private UILabel playerName;
    private UILabel enery;
    private UILabel toughen;
    private UISprite headSprite;

    private UIButton addEnery;
    private UIButton addToughen;

    private UIProgressBar eneryBar;
    private UIProgressBar toughenBar;

    private PlayerController playerInfo;

    // Use this for initialization
    void Awake ()
    {
        headSprite = transform.FindChild("sp_head").GetComponent<UISprite>();
        level = transform.FindChild("tf_level").GetComponent<UILabel>();
        playerName = transform.FindChild("tf_name").GetComponent<UILabel>();
        enery = transform.FindChild("enery_bar/tf_enery").GetComponent<UILabel>();
        toughen = transform.FindChild("toughen_bar/tf_toughen").GetComponent<UILabel>();

        addEnery = transform.FindChild("btn_add_enery").GetComponent<UIButton>();
        addToughen = transform.FindChild("btn_add_toughen").GetComponent<UIButton>();

        eneryBar = transform.FindChild("enery_bar").GetComponent<UIProgressBar>();
        toughenBar = transform.FindChild("toughen_bar").GetComponent<UIProgressBar>();

        playerInfo = PlayerController._instance;
        playerInfo.playerInfochangeEvent += this.OnPlayerInfoChanged;
    }

    void OnPlayerInfoChanged(PlayerInfoType infoType)
    {
        if (infoType == PlayerInfoType.All || infoType == PlayerInfoType.Head)
        {
            headSprite.spriteName = playerInfo.Head;
        }
        if (infoType == PlayerInfoType.All || infoType == PlayerInfoType.Level)
        {
            level.text = playerInfo.Level.ToString();
        }
        if (infoType == PlayerInfoType.All || infoType == PlayerInfoType.Name)
        {
            playerName.text = playerInfo.Name.ToString();
        }
        if (infoType == PlayerInfoType.All || infoType == PlayerInfoType.Enery)
        {
            enery.text = playerInfo.Enery + "/100";
            eneryBar.value = playerInfo.Enery / 100f;
        }
        if (infoType == PlayerInfoType.All || infoType == PlayerInfoType.Toughen)
        {
            toughen.text = playerInfo.Toughen + "/50";
            toughenBar.value = playerInfo.Toughen / 100f;
        }
    }

    void OnDestroy()
    {
        if (playerInfo != null)
        {
            playerInfo.playerInfochangeEvent -= this.OnPlayerInfoChanged;
        }
    }
}
