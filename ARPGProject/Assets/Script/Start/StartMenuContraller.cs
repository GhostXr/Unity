using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuContraller : MonoBehaviour {

    public static StartMenuContraller _instance;

    public TweenPosition enterGameTween;
    public TweenPosition selcerCharacterTwee;
    public TweenPosition showCharacterTwee;

    public GameObject[] characterArray;
    private GameObject curCharacter;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnEnterGame()
    {
        //1.connect server

        //2.enter select character dialog
        enterGameTween.PlayForward();
        HidePanel(enterGameTween.gameObject);

        selcerCharacterTwee.gameObject.SetActive(true);
        selcerCharacterTwee.PlayForward();
    }

    public void OnChangeCharacter()
    {
        //1.connect server

        //2.enter select character dialog
        selcerCharacterTwee.PlayReverse();
        HidePanel(selcerCharacterTwee.gameObject);

        showCharacterTwee.gameObject.SetActive(true);
        showCharacterTwee.PlayForward();
    }

    public void OnBackselecttCharacter()
    {
        //1.connect server

        //2.enter select character dialog
        showCharacterTwee.PlayReverse();
        HidePanel(showCharacterTwee.gameObject);

        selcerCharacterTwee.gameObject.SetActive(true);
        selcerCharacterTwee.PlayForward();
    }

    IEnumerable HidePanel(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }

    public void OnCharacterClick(GameObject go)
    {
        if (curCharacter == go)
        {
            return;
        }
        iTween.ScaleTo(go, new Vector3(1.2f, 1.2f, 1.2f), 0.4f);
        if (curCharacter != null)
        {
            iTween.ScaleTo(curCharacter, new Vector3(1f, 1f, 1f), 0.4f);
        }
        curCharacter = go;
    }
}
