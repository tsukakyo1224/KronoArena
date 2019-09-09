using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWork_02 : MonoBehaviour
{

    GameObject P1_Chara1;
    GameObject P1_Chara2;
    GameObject P1_Chara3;
    GameObject P2_Chara1;
    GameObject P2_Chara2;
    GameObject P2_Chara3;
    public float time;
    public bool StartFlag;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        StartFlag = false;
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
                Vector3 pos = new Vector3(0f, 0f, 4.0f);
                P1_Chara1 = PhotonNetwork.Instantiate("Chara_Model/Knight", pos, Quaternion.identity, 0);
                P1_Chara1.transform.rotation = Quaternion.Euler(0.0f, -135.0f, 0.0f);
                //キャラクター二人目生成
                pos = new Vector3(2.0f, 0f, 2.0f);
                P1_Chara2 = PhotonNetwork.Instantiate("Chara_Model/Medic", pos, Quaternion.identity, 0);
                P1_Chara2.transform.rotation = Quaternion.Euler(0.0f, -135.0f, 0.0f);
                //キャラクター三人目生成
                pos = new Vector3(4.0f, 0f, 0.0f);
                P1_Chara3 = PhotonNetwork.Instantiate("Chara_Model/Guardian", pos, Quaternion.identity, 0);
                P1_Chara3.transform.rotation = Quaternion.Euler(0.0f, -135.0f, 0.0f);
                //キャラクターの名前、tag設定
                P1_Chara1.name = "P1_Chara1";
                P1_Chara1.tag = "Player1";
                P1_Chara2.name = "P1_Chara2";
                P1_Chara2.tag = "Player1";
                P1_Chara3.name = "P1_Chara3";
                P1_Chara3.tag = "Player1";

                GameManager.CameraFlag = true;
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                Network_01.gamestartflag = false;
                //キャラクター一人目生成
                Vector3 pos = new Vector3(0f, 0f, -4.0f);
                P2_Chara1 = PhotonNetwork.Instantiate("Chara_Model/Knight", pos, Quaternion.identity, 0);
                P2_Chara1.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
                //キャラクター二人目生成
                pos = new Vector3(-2.0f, 0f, -2.0f);
                P2_Chara2 = PhotonNetwork.Instantiate("Chara_Model/Medic", pos, Quaternion.identity, 0);
                P2_Chara2.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);
                //キャラクター三人目生成
                pos = new Vector3(-4.0f, 0f, 0.0f);
                P2_Chara3 = PhotonNetwork.Instantiate("Chara_Model/Guardian", pos, Quaternion.identity, 0);
                P2_Chara3.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);

                //キャラクターの名前、tag設定
                P2_Chara1.name = "P2_Chara1";
                P2_Chara1.tag = "Player2";
                P2_Chara2.name = "P2_Chara2";
                P2_Chara2.tag = "Player2";
                P2_Chara3.name = "P2_Chara3";
                P2_Chara3.tag = "Player2";

                GameManager.CameraFlag = true;
            }
        }
        if(PhotonNetwork.playerList.Length == 2)
        {
            time += Time.deltaTime;
        }
        if (PhotonNetwork.player.ID == 2 && time >= 3.0f && StartFlag == false)
        {
            GameObject.Find("CameraParent").GetComponent<MultipleTargetCamera>().AddCharaOn();
            StartFlag = true;
        }
    }

    public void CharaNameChange()
    {

    }

    public void CharaGenerate()
    {

    }


}
