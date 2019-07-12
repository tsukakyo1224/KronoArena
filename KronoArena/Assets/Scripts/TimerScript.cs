using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    //時間制限
    public static float TotalTime;

    //時間制限テキスト
    private GameObject TimeText;

    // Start is called before the first frame update
    void Start()
    {
        //初期値
        TotalTime = 5.0f;
        //タイマーテキスト
        TimeText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //if(PhotonNetwork.playerList.Length == 2)
        if (Network_01.gameplayflag == true)
        {
            //時間
            if (TurnCol.P1_Turn == true)
            {
                TotalTime -= Time.deltaTime;
                TimeText.GetComponent<Text>().text = ("" + TotalTime.ToString("f2"));
            }
            else
            {
                TotalTime += Time.deltaTime;
                TimeText.GetComponent<Text>().text = ("" + TotalTime.ToString("f2"));
            }

            //ターン切り替え
            if (TotalTime < 0.0f)
            {
                //TotalTime = 5.0f;
                TurnCol.ChangeTurn();
            }
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(TotalTime);
        }
        else
        {
            //データの受信
            TotalTime = (int)(float)stream.ReceiveNext();
        }
    }
}
