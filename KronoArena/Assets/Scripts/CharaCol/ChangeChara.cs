using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChara : MonoBehaviour
{
    //　現在どのキャラクターを操作しているか
    public static int nowChara;
    //　操作可能なゲームキャラクター
    [SerializeField]
    private List<GameObject> charaLists;

    public GameObject GM;

    void Start()
    {
        GM = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (GameManager.CameraFlag == true)
        {
            //キャラリスト作成
            if (PhotonNetwork.player.ID == 1)
            {
                charaLists.Add(GameObject.Find("P1_Chara1"));
                charaLists.Add(GameObject.Find("P1_Chara2"));
                charaLists.Add(GameObject.Find("P1_Chara3"));
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                charaLists.Add(GameObject.Find("P2_Chara1"));
                charaLists.Add(GameObject.Find("P2_Chara2"));
                charaLists.Add(GameObject.Find("P2_Chara3"));

            }
            //　最初の操作キャラクターを0番目のキャラクターにする為、キャラクターの総数をnowCharaに設定し最初のキャラクターが設定されるようにする
            nowChara = charaLists.Count;
            //Debug.Log("現在のキャラ番号" + nowChara);
            ChangeCharacter(nowChara);
            GameManager.CameraFlag = false;
        }
    }

    public void Change1(int tempNowChara)
    {
        bool flag;  //　オン・オフのフラグ
        //　次の操作キャラクターを指定
        int nextChara = 0;



        //　次の操作キャラクターだったら動く機能を有効にし、それ以外は無効にする
        for (var i = 0; i < charaLists.Count; i++)
        {

            if (i == nextChara)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            //　操作するキャラクターと操作しないキャラクターで機能のオン・オフをする
            charaLists[i].GetComponent<CharaCol_test>().enabled = flag;
            //　キャラクターのアニメーションを最初の状態にする為アニメーションパラメータのSpeedを0にする
            //charaLists[i].GetComponent<Animator>().SetFloat("Speed", 0);
        }
        //　次の操作キャラクターを現在操作しているキャラクターに設定して終了
        nowChara = nextChara;
        GM.GetComponent<GameManager>().CharaBarAnim1.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaButtonAnim1.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaBarAnim1.SetBool("Off", false);
        GM.GetComponent<GameManager>().CharaButtonAnim1.SetBool("Off", false);
        GM.GetComponent<GameManager>().CharaBarAnim2.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim2.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaBarAnim3.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim3.SetBool("Off", true);
    }

    public void Change2(int tempNowChara)
    {
        bool flag;  //　オン・オフのフラグ
        //　次の操作キャラクターを指定
        int nextChara = 1;

        //　次の操作キャラクターだったら動く機能を有効にし、それ以外は無効にする
        for (var i = 0; i < charaLists.Count; i++)
        {

            if (i == nextChara)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            //　操作するキャラクターと操作しないキャラクターで機能のオン・オフをする
            charaLists[i].GetComponent<CharaCol_test>().enabled = flag;
            //　キャラクターのアニメーションを最初の状態にする為アニメーションパラメータのSpeedを0にする
            //charaLists[i].GetComponent<Animator>().SetFloat("Speed", 0);
        }
        //　次の操作キャラクターを現在操作しているキャラクターに設定して終了
        nowChara = nextChara;
        GM.GetComponent<GameManager>().CharaBarAnim1.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim1.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaBarAnim2.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaButtonAnim2.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaBarAnim2.SetBool("Off", false);
        GM.GetComponent<GameManager>().CharaButtonAnim2.SetBool("Off", false);
        GM.GetComponent<GameManager>().CharaBarAnim3.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim3.SetBool("Off", true);

    }

    public void Change3(int tempNowChara)
    {
        bool flag;  //　オン・オフのフラグ
        //　次の操作キャラクターを指定
        int nextChara = 2;

        //　次の操作キャラクターだったら動く機能を有効にし、それ以外は無効にする
        for (var i = 0; i < charaLists.Count; i++)
        {

            if (i == nextChara)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            //　操作するキャラクターと操作しないキャラクターで機能のオン・オフをする
            charaLists[i].GetComponent<CharaCol_test>().enabled = flag;
            //　キャラクターのアニメーションを最初の状態にする為アニメーションパラメータのSpeedを0にする
            //charaLists[i].GetComponent<Animator>().SetFloat("Speed", 0);
        }
        //　次の操作キャラクターを現在操作しているキャラクターに設定して終了
        nowChara = nextChara;
        GM.GetComponent<GameManager>().CharaBarAnim1.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim1.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaBarAnim2.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaButtonAnim2.SetBool("Off", true);
        GM.GetComponent<GameManager>().CharaBarAnim3.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaButtonAnim3.SetBool("On", true);
        GM.GetComponent<GameManager>().CharaBarAnim3.SetBool("Off", false);
        GM.GetComponent<GameManager>().CharaButtonAnim3.SetBool("Off", false);
    }


    //　操作キャラクター変更メソッド
    void ChangeCharacter(int tempNowChara)
    {

        bool flag;  //　オン・オフのフラグ
        //　次の操作キャラクターを指定
        int nextChara = tempNowChara + 1;
        //　次の操作キャラクターがいない時は最初のキャラクターに設定
        if (nextChara >= charaLists.Count)
        {
            nextChara = 0;
        }
        //　次の操作キャラクターだったら動く機能を有効にし、それ以外は無効にする
        for (var i = 0; i < charaLists.Count; i++)
        {

            if (i == nextChara)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            //　操作するキャラクターと操作しないキャラクターで機能のオン・オフをする
            charaLists[i].GetComponent<CharaCol_test>().enabled = flag;
            //　キャラクターのアニメーションを最初の状態にする為アニメーションパラメータのSpeedを0にする
            //charaLists[i].GetComponent<Animator>().SetFloat("Speed", 0);
        }
        //　次の操作キャラクターを現在操作しているキャラクターに設定して終了
        nowChara = nextChara;
    }

}
