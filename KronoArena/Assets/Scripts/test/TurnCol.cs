using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCol : MonoBehaviour
{
    [SerializeField]
    public static bool P1_Turn = false;
    [SerializeField]
    public static bool P2_Turn = false;

    [SerializeField]
    public bool p1turn;
    [SerializeField]
    public bool p2turn;


    // Start is called before the first frame update
    void Start()
    {
        //今の所prayer1が先行
        P1_Turn = true;
        P2_Turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //見えるように
        p1turn = P1_Turn;
        p2turn = P2_Turn;
    }

    public static void ChangeTurn()
    {
        //プレイヤー2に移る
        if (P1_Turn==true)
        {
            P2_Turn = true;
            P1_Turn = false;
            GameObject.Find("TurnCol").GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        }
        //プレイヤー1に移る
        else if(P2_Turn == true)
        {
            P1_Turn = true;
            P2_Turn = false;
        }

    }


    [PunRPC]
    public void ColorRpc(Vector3 col)
    {
       
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
