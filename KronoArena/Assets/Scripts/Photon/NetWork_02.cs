using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWork_02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Network_01.gamestartflag == true)
        {
            if (PhotonNetwork.player.ID == 1)
            {
                Network_01.gamestartflag = false;
                // 第1引数にResourcesフォルダの中にあるプレハブの名前(文字列)
                // 第2引数にposition
                // 第3引数にrotation
                // 第4引数にView ID(指定しない場合は0)
                //キャラクター一人目生成
                Vector3 pos = new Vector3(0f, 0f, 0f);
                GameObject player1 = PhotonNetwork.Instantiate("Chara_Model/Knight", pos, Quaternion.identity, 0);
                player1.name = "P1_Chara1";
                player1.tag = "Player1";
                //キャラクター二人目生成
                pos = new Vector3(3.5f, 0f, 0f);
                GameObject player2 = PhotonNetwork.Instantiate("Chara_Model/Medic", pos, Quaternion.identity, 0);
                player2.name = "P1_Chara2";
                player2.tag = "Player1";
                //キャラクター三人目生成
                pos = new Vector3(-3.5f, 0f, 0f);
                GameObject player3 = PhotonNetwork.Instantiate("Chara_Model/Guardian", pos, Quaternion.identity, 0);
                player3.name = "P1_Chara3";
                player3.tag = "Player1";
                FollowingCamera.cameraflag = true;
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                Network_01.gamestartflag = false;
                //キャラクター一人目生成
                Vector3 pos = new Vector3(0f, 0f, 5f);
                GameObject player1 = PhotonNetwork.Instantiate("Chara_Model/Knight", pos, Quaternion.identity, 0);
                player1.name = "P2_Chara1";
                player1.tag = "Player2";
                //キャラクター二人目生成
                pos = new Vector3(3.5f, 0f, 5f);
                GameObject player2 = PhotonNetwork.Instantiate("Chara_Model/Medic", pos, Quaternion.identity, 0);
                player2.name = "P2_Chara2";
                player2.tag = "Player2";
                //キャラクター三人目生成
                pos = new Vector3(-3.5f, 0f, 5f);
                GameObject player3 = PhotonNetwork.Instantiate("Chara_Model/Guardian", pos, Quaternion.identity, 0);
                player3.name = "P2_Chara3";
                player3.tag = "Player2";
                FollowingCamera.cameraflag = true;
            }
        }

        // 左クリックをしたらマッチング環境にCubeのインスタンスを生成する
        if (Input.GetMouseButtonDown(0))
        {

            // 生成したオブジェクトに力を加える
            //Rigidbody objRB = obj.GetComponent<Rigidbody>();
            //objRB.AddForce(Vector3.forward * 20f, ForceMode.Impulse);
        }
    }
}
