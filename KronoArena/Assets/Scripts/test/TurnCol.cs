using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCol : MonoBehaviour
{

    public static bool P1_Turn=false;
    public static bool P2_Turn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤー1なら
        if (PhotonNetwork.playerList.Length == 1)
        {

        }
        //プレイヤー2なら
        else
        {

        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(P1_Turn);
            stream.SendNext(P2_Turn);
        }
        else
        {
            //データの受信
            P1_Turn = (bool)stream.ReceiveNext();
            P2_Turn = (bool)stream.ReceiveNext();
        }
    }
}
