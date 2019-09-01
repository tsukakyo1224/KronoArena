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

    //砂時計押せるようになりまでのフラグ
    public static bool HourGlassFlag;

    //
    public static float FlagTime;

    //秒数増えるカウント
    public static int Count;

    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        //初期時間(現時点ではお互いに15秒)
        TotalTime = 15.0f;

        //タイマーテキスト
        TimeText = GameObject.Find("Time");

        //ターン替えできるflag
        HourGlassFlag = false;

        //秒数増えるカウント
        Count = 0;

        photonView = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.playerList.Length == 2 && Network_01.gameplayflag == true)
        //if (Network_01.gameplayflag == true)
        {
            //時間
            if ((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true) ||
                (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true))
            {
                TotalTime -= Time.deltaTime;
                TimeText.GetComponent<Text>().text = ("" + TotalTime.ToString("f2"));

                FlagTime += Time.deltaTime;
                if (FlagTime >= 10.0f)
                {
                    HourGlassFlag = true;
                }
            }
            else
            {
                TotalTime += Time.deltaTime;
                TimeText.GetComponent<Text>().text = ("" + TotalTime.ToString("f2"));
            }

            //ターン切り替え
            if (TotalTime < 0.0f)
            {
                GameObject.Find("TurnCol").GetComponent<TurnCol>().ChangeTurn();
                photonView.RPC("SecondUp", PhotonTargets.All);
                //TotalTime += 10.0f;
                Debug.Log("TimeTurnChange");
            }
        }

    }


    [PunRPC]
    public void SecondUp()
    {
        if((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true) ||
            (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true))
        {
            TotalTime += 3.0f;
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
