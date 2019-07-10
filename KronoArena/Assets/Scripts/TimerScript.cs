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
        TotalTime = 30.0f;
        //タイマーテキスト
        TimeText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        //時間
        if (!(PhotonNetwork.playerList.Length == 1))
        {
            TotalTime -= Time.deltaTime;
            TimeText.GetComponent<Text>().text = ("" + TotalTime.ToString("f2"));
        }

        //ターン切り替え
        if(TotalTime < 0.0f)
        {
            TotalTime = 30.0f;
            if (TurnCol.P1_Turn == true)
            {
                TurnCol.P2_Turn = true;
                TurnCol.P1_Turn = false;
            }
            else
            {
                TurnCol.P1_Turn = true;
                TurnCol.P2_Turn = false;
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
