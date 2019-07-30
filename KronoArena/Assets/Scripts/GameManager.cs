using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //キャラごとの攻撃までの時間用テキスト(左上)
    public static GameObject CharaAttackTime1;
    public static GameObject CharaAttackTime2;
    public static GameObject CharaAttackTime3;

    //キャラ切り替えボタン用オブジェクト
    public static GameObject CharaChangeButton1;
    public static GameObject CharaChangeButton2;
    public static GameObject CharaChangeButton3;

    //キャラ攻撃ボタン用オブジェクト
    public static GameObject AttackButton1;
    public static GameObject AttackButton2;
    public static GameObject AttackButton3;


    //操作キャラクター用オブジェクト
    public static GameObject OpeCharaIcon;  //キャラアイコン
    public static GameObject OpeCharaName;  //キャラの名前
    public static Image OpeCharaJobIcon;   //ジョブアイコン
    public static Slider OpeCharaHPSlider;    //キャラのHP
    public static GameObject OpeCharaHPText;    //キャラのHPテキスト

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

        //キャラ攻撃ボタン用オブジェクト
        AttackButton1 = GameObject.Find("Attack1");
        AttackButton2 = GameObject.Find("Attack2");
        AttackButton3 = GameObject.Find("Attack3");

        //キャラ切り替えボタン用オブジェクト
        CharaChangeButton1 = GameObject.Find("ChangeChara1");
        CharaChangeButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("MiniCharaIcon/MiniIcon1");
        CharaChangeButton2 = GameObject.Find("ChangeChara2");
        CharaChangeButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("MiniCharaIcon/MiniIcon2");
        CharaChangeButton3 = GameObject.Find("ChangeChara3");
        CharaChangeButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("MiniCharaIcon/MiniIcon3");

        //操作キャラクター用オブジェクト(右下)
        OpeCharaIcon = GameObject.Find("OpeCharaIcon");
        OpeCharaName = GameObject.Find("OpeCharaName");
        OpeCharaJobIcon = GameObject.Find("OpeCharaJobIcon").GetComponent<Image>();
        OpeCharaHPSlider = GameObject.Find("BackGround").transform.Find("OpeCharaHPSlider").GetComponent<Slider>();
        OpeCharaHPText = GameObject.Find("OpeCharaHPText");


        TurnChangeButton = GameObject.Find("ChangeTurn");


        TurnText = GameObject.Find("TurnText");


        //最初は非表示に
        CharaAttackTime1.SetActive(false);
        CharaAttackTime2.SetActive(false);
        CharaAttackTime3.SetActive(false);

        //最初は非表示に
        AttackButton1.SetActive(true);
        AttackButton2.SetActive(true);
        AttackButton3.SetActive(true);

        //最初は押せないように
        CharaChangeButton1.GetComponent<Button>().interactable = true;
        CharaChangeButton2.GetComponent<Button>().interactable = true;
        CharaChangeButton3.GetComponent<Button>().interactable = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Network_01.gameplayflag == true)
        {
            //操作キャラ変更時に操作キャラクター表示の変更
            if (ChangeChara.nowChara == 0)
            {
                OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon1");
                OpeCharaName.GetComponent<Text>().text = Knight_Data.CharaName;
                OpeCharaJobIcon.sprite = Knight_Data.JobIconImage;
                OpeCharaHPSlider.maxValue = Knight_Data.hpSlider.maxValue;
                OpeCharaHPSlider.value = Knight_Data.hpSlider.value;
                OpeCharaHPText.GetComponent<Text>().text = Knight_Data.hpSlider.value + "/" + Knight_Data.MaxHP;
            }
            else if (ChangeChara.nowChara == 1)
            {
                //OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon2");
                OpeCharaName.GetComponent<Text>().text = CharaData2.CharaName;
                OpeCharaJobIcon.sprite = CharaData2.JobIconImage;
                //OpeCharaHPSlider.maxValue = CharaData2.hpSlider.maxValue;
                //OpeCharaHPSlider.value = CharaData2.hpSlider.value;
                //OpeCharaHPText.GetComponent<Text>().text = CharaData2.hpSlider.value + "/" + CharaData2.MaxHP;
            }
            else if (ChangeChara.nowChara == 2)
            {
                //OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon3");
                OpeCharaName.GetComponent<Text>().text = CharaData3.CharaName;
                OpeCharaJobIcon.sprite = CharaData3.JobIconImage;
                OpeCharaHPSlider.maxValue = CharaData3.hpSlider.maxValue;
                OpeCharaHPSlider.value = CharaData3.hpSlider.value;
                OpeCharaHPText.GetComponent<Text>().text = CharaData3.hpSlider.value + "/" + CharaData3.MaxHP;
            }


        }


        //ターン切り替えの時の処理
        if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
        {
            CharaChangeButton1.GetComponent<Button>().interactable = true;
            CharaChangeButton2.GetComponent<Button>().interactable = true;
            CharaChangeButton3.GetComponent<Button>().interactable = true;
            AttackButton1.SetActive(true);
            AttackButton2.SetActive(true);
            AttackButton3.SetActive(true);
            TurnText.GetComponent<Text>().text = "My turn";
            //TurnChangeButton.SetActive(true);
        }
        else if((TurnCol.P1_Turn == false && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == false && PhotonNetwork.player.ID == 2)) 
        {
            AttackButton1.SetActive(false);
            AttackButton2.SetActive(false);
            AttackButton3.SetActive(false);
            CharaChangeButton1.GetComponent<Button>().interactable = false;
            CharaChangeButton2.GetComponent<Button>().interactable = false;
            CharaChangeButton3.GetComponent<Button>().interactable = false;
            TurnText.GetComponent<Text>().text = "Your turn";
            //TurnChangeButton.SetActive(false);
        }
    }
}
