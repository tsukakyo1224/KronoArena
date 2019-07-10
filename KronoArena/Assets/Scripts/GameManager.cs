using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //キャラごとの攻撃までの時間用テキスト
    public static GameObject CharaAttackTime1;
    public static GameObject CharaAttackTime2;
    public static GameObject CharaAttackTime3;

    //キャラ切り替えボタン用オブジェクト
    public static GameObject CharaChangeButton1;
    public static GameObject CharaChangeButton2;
    public static GameObject CharaChangeButton3;

    //ターン切り替えボタン
    public static GameObject TurnChangeButton;

    //ターン(turn==0:自分、turn==1:相手)
    //public static int turn;

    //ターン用テキスト
    public static GameObject TurnText;

    //Photon同期用
    private PhotonView photonView;
    private PhotonTransformView photonTransformView;



    // Start is called before the first frame update
    void Start()
    {

        //Photon同期用
        photonTransformView = GetComponent<PhotonTransformView>();
        photonView = PhotonView.Get(this);

        //キャラごとの攻撃までの時間用テキスト代入
        CharaAttackTime1 = GameObject.Find("AttackTime1");
        CharaAttackTime2 = GameObject.Find("AttackTime2");
        CharaAttackTime3 = GameObject.Find("AttackTime3");

        //キャラ切り替えボタン用オブジェクト
        CharaChangeButton1 = GameObject.Find("ChangeChara1");
        CharaChangeButton2 = GameObject.Find("ChangeChara2");
        CharaChangeButton3 = GameObject.Find("ChangeChara3");

        TurnChangeButton = GameObject.Find("ChangeTurn");

        TurnText = GameObject.Find("TurnText");


        //最初は非表示に
        CharaAttackTime1.SetActive(false);
        CharaAttackTime2.SetActive(false);
        CharaAttackTime3.SetActive(false);

        //最初は押せないように
        CharaChangeButton1.GetComponent<Button>().interactable = false;
        CharaChangeButton2.GetComponent<Button>().interactable = false;
        CharaChangeButton3.GetComponent<Button>().interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (TurnCol.P1_Turn == true && PhotonNetwork.player.ID==1)
        {
            CharaChangeButton1.GetComponent<Button>().interactable = true;
            CharaChangeButton2.GetComponent<Button>().interactable = true;
            CharaChangeButton3.GetComponent<Button>().interactable = true;
            TurnText.GetComponent<Text>().text = "My turn";
            //TurnChangeButton.SetActive(true);
        }
        else if(TurnCol.P1_Turn == false && PhotonNetwork.player.ID == 1) 
        {
            CharaChangeButton1.GetComponent<Button>().interactable = false;
            CharaChangeButton2.GetComponent<Button>().interactable = false;
            CharaChangeButton3.GetComponent<Button>().interactable = false;
            TurnText.GetComponent<Text>().text = "Your turn";
            //TurnChangeButton.SetActive(false);
        }
        if (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2)
        {
            CharaChangeButton1.GetComponent<Button>().interactable = true;
            CharaChangeButton2.GetComponent<Button>().interactable = true;
            CharaChangeButton3.GetComponent<Button>().interactable = true;
            TurnText.GetComponent<Text>().text = "My turn";
            //TurnChangeButton.SetActive(true);
        }
        else if (TurnCol.P2_Turn == false && PhotonNetwork.player.ID == 2)
        {
            CharaChangeButton1.GetComponent<Button>().interactable = false;
            CharaChangeButton2.GetComponent<Button>().interactable = false;
            CharaChangeButton3.GetComponent<Button>().interactable = false;
            TurnText.GetComponent<Text>().text = "Your turn";
            //TurnChangeButton.SetActive(f alse);
        }
    }
}
