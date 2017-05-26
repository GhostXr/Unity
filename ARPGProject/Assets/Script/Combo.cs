using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    public static Combo _instance;
    public float comboTime = 2f;
    
    private float timer = 0;
    private int comboCount = 0;
    private UILabel comboLabel;

    void Awake()
    {
        _instance = this;
        gameObject.SetActive(false);
        comboLabel = transform.Find("combo_label").GetComponent<UILabel>();
    }

	// Use this for initialization
	void Update ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            comboCount = 0;
            gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	public void SubCombo()
    {
        gameObject.SetActive(true);
        timer = comboTime;
        comboCount++;
        comboLabel.text = "x"+comboCount.ToString();
        gameObject.transform.localScale = Vector3.zero;
        iTween.ScaleTo(gameObject, new Vector3(1.2f,1.2f,1.2f), 0.2f);
        iTween.ShakePosition(gameObject, new Vector3(0.2f, 0.2f, 0.2f), 0.2f);
    }
}
