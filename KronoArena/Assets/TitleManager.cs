using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    private AudioSource OPBGM;
    public AudioSource SandBGM;
    public bool AudioFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        AudioSource[] SandSources = GameObject.Find("Main Camera").GetComponents<AudioSource>();
        OPBGM = audioSources[0];
        SandBGM = SandSources[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioFlag == true)
        {
            OPBGM.GetComponent<AudioSource>().volume -= 0.002f;
            SandBGM.GetComponent<AudioSource>().volume -= 0.002f;
        }

    }

    public void SandAudio()
    {
        SandBGM.PlayOneShot(SandBGM.clip);
    }
}
