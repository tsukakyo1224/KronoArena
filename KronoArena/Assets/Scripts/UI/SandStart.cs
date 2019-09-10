using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SandStart : MonoBehaviour
{
    public GameObject Title;
    public GameObject TitleMOVIE;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        //Title.SetActive (false);
        Destroy(Title, 0.4f);
        TitleMOVIE.GetComponent<VideoPlayer>().Play();
    }
}
