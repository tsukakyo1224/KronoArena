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


    // Start is called before the first frame update
    void Start()
    {
        MyUp = GameObject.Find("Sand_MyUp");
        MyDown = GameObject.Find("Sand_MyDown");
        YouUp = GameObject.Find("Sand_YouUp");
        YouDown = GameObject.Find("Sand_YouDown");

        MyUp.SetActive(false);
        MyDown.SetActive(false);
        YouUp.SetActive(false);
        YouDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true) ||
            (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true))
        {
            MyUp.SetActive(true);
            MyDown.SetActive(true);
            YouUp.SetActive(false);
            YouDown.SetActive(false);
            UIobj1.fillAmount = 1 - (TimerScript.TotalTime / 30.0f);
            UIobj2.fillAmount = TimerScript.TotalTime / 30.0f;
        }
        else
        {
            MyUp.SetActive(false);
            MyDown.SetActive(false);
            YouUp.SetActive(true);
            YouDown.SetActive(true);
            UIobj3.fillAmount = TimerScript.TotalTime / 30.0f;
            UIobj4.fillAmount = 1 - (TimerScript.TotalTime / 30.0f);
        }

    }
}
