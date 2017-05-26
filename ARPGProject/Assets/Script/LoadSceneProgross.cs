using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneProgross : MonoBehaviour {

    public static LoadSceneProgross _instance;

    private GameObject bg;
    private UISlider progressBar;
    private bool isAsyn = false;
    private AsyncOperation ao;

	// Use this for initialization
	void Awake () {
        _instance = this;
        bg = transform.Find("bg").gameObject;
        gameObject.SetActive(false);

        progressBar = transform.Find("bg/progressBar").GetComponent<UISlider>();
    }

    void updata()
    {
        if (isAsyn)
        {
            progressBar.value = ao.progress;
        }
    }
	
	// Update is called once per frame
	public void Show (AsyncOperation ao) {
        _instance.gameObject.SetActive(true);
        bg.SetActive(true);

        isAsyn = true;
        this.ao = ao;
    }
}
