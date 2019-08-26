using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    //同期する変数1
    public int hensu1 = 0;
    public float hensu2 = 0f;


    private PhotonView photonView;
    private PhotonTransformView photonTransformView;

    // Use this for initialization
    void Start()
    {
        photonTransformView = GetComponent<PhotonTransformView>();
        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            //現在の移動速度
            Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
            //移動速度を指定
            photonTransformView.SetSynchronizedValues(velocity, 0);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(hensu1);
            stream.SendNext(hensu2);
        }
        else
        {
            //データの受信
            this.hensu1 = (int)stream.ReceiveNext();
            this.hensu2 = (float)stream.ReceiveNext();
        }
    }
}
