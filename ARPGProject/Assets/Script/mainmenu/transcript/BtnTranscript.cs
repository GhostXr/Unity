using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnTranscript : MonoBehaviour {

    public int id;
    public int condition;
    public string sceneName;
    public string mapDesc = "哈哈哈哈哈啊哈哈哈哈哈哈";

    public void OnClick()
    {
        transform.parent.SendMessage("OnClickEliteBtn", this);
    }
}
