using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sand : MonoBehaviour
{
    public Image UIobj1;
    public Image UIobj2;
    public Image UIobj3;
    public Image UIobj4;

    public GameObject MyUp;
    public GameObject MyDown;
    public GameObject YouUp;
    public GameObject YouDown;
    public static float sand_time;


    // Start is called before the first frame update
    void Start()
    {
        MyUp = GameObject.Find("Sand_MyUp");
        MyDown = GameObject.Find("Sand_MyDown");
        YouUp = GameObject.Find("Sand_YouUp");
        YouDown = GameObject.Find("Sand_YouDown");

        MyUp.GetComponent<Image>().enabled = false;
        MyDown.GetComponent<Image>().enabled = false;
        YouUp.GetComponent<Image>().enabled = false;
        YouDown.GetComponent<Image>().enabled = false;
        sand_time = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true) ||
            (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true))
        {
            MyUp.GetComponent<Image>().enabled = true;
            MyDown.GetComponent<Image>().enabled = true;
            YouUp.GetComponent<Image>().enabled = false;
            YouDown.GetComponent<Image>().enabled = false;
            UIobj1.fillAmount = 1 - (TimerScript.TotalTime / sand_time);
            UIobj2.fillAmount = TimerScript.TotalTime / sand_time;
        }
        else
        {
            MyUp.GetComponent<Image>().enabled = false;
            MyDown.GetComponent<Image>().enabled = false;
            YouUp.GetComponent<Image>().enabled = true;
            YouDown.GetComponent<Image>().enabled = true;
            UIobj3.fillAmount = TimerScript.TotalTime / sand_time;
            UIobj4.fillAmount = 1 - (TimerScript.TotalTime / sand_time);
        }
    }
}
