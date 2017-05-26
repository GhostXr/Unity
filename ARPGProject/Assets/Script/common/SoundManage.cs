using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class SoundManage : MonoBehaviour {

    public static SoundManage _instance;

    public AudioClip[] audioClipArray;
    public Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;
    public bool isQuiet = false;

	// Use this for initialization
	void Awake ()
    {
        _instance = this;
    }
	
	// Update is called once per frame
	void Start ()
    {
        foreach(AudioClip audio in audioClipArray)
        {
            audioDic.Add(audio.name, audio);
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string audioName)
    {
        if (isQuiet) return;
        if(audioDic[audioName])
        {
            audioSource.PlayOneShot(audioDic[audioName]);
        }
    }

    public void PlaySound(string audioName, AudioSource audioSource)
    {
        if (isQuiet) return;
        if (audioDic[audioName])
        {
            audioSource.PlayOneShot(audioDic[audioName]);
        }
    }
}
