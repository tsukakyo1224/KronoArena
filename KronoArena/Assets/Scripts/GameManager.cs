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

    //キャラクター用オブジェクト
    //キャラ攻撃時間テキスト
    public static GameObject ATime2;
    public static GameObject ATime3;


    //キャラクター用オブジェクト
    public static GameObject Chara1;
    public static GameObject Chara2;
    public static GameObject Chara3;


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

    //タイマーテキスト
    public static GameObject TimeText;

    //カメラ
    public static GameObject Camera;

    //Photon同期用
    private PhotonView photonView;
    private PhotonTransformView photonTransformView;

    public static bool cameraflag = false;

    //ゲーム勝利用ポイント
    public static int P1_GP = 0;
    public static int P2_GP = 0;



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

        //攻撃時間用テキスト
        ATime2 = GameObject.Find("ATime2");
        ATime3 = GameObject.Find("ATime3");

        //キャラ切り替えボタン用オブジェクト
        CharaChangeButton1 = GameObject.Find("ChangeChara1");
        CharaChangeButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Knight");
        CharaChangeButton2 = GameObject.Find("ChangeChara2");
        CharaChangeButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Medic");
        CharaChangeButton3 = GameObject.Find("ChangeChara3");
        CharaChangeButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Guardion");

        //操作キャラクター用オブジェクト(右下)
        OpeCharaIcon = GameObject.Find("OpeCharaIcon");
        OpeCharaName = GameObject.Find("OpeCharaName");
        OpeCharaJobIcon = GameObject.Find("OpeCharaJobIcon").GetComponent<Image>();
        OpeCharaHPSlider = GameObject.Find("BackGround").transform.Find("OpeCharaHPSlider").GetComponent<Slider>();
        OpeCharaHPText = GameObject.Find("OpeCharaHPText");


        TurnChangeButton = GameObject.Find("ChangeTurn");

        TurnText = GameObject.Find("TurnText");

        TimeText = GameObject.Find("Time");

        //カメラオブジェクト
        Camera = GameObject.Find("Main Camera");


        //最初は非表示に
        CharaAttackTime1.SetActive(false);
        CharaAttackTime2.SetActive(false);
        CharaAttackTime3.SetActive(false);

        //最初は表示に
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
            if(PhotonNetwork.player.ID == 1)
            {
                Chara1 = GameObject.Find("P1_Chara1");
                Chara2 = GameObject.Find("P1_Chara2");
                Chara3 = GameObject.Find("P1_Chara3");
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                Chara1 = GameObject.Find("P2_Chara1");
                Chara2 = GameObject.Find("P2_Chara2");
                Chara3 = GameObject.Find("P2_Chara3");
            }

            //操作キャラ変更時に操作キャラクター表示の変更
            //現時点でナイト確定
            if (ChangeChara.nowChara == 0)
            {
                if (PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true)
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Knight");
                }
                else
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Knight_Reverse");
                }
                OpeCharaName.GetComponent<Text>().text = Knight_Data.CharaName;
                OpeCharaJobIcon.sprite = Knight_Data.JobIconImage;
                //存在しているのなら表示
                //Debug.Log(Chara1);
                if (Chara1 != null)
                {
                    OpeCharaHPSlider.maxValue = Chara1.GetComponent<Status>().hpSlider.maxValue;
                    OpeCharaHPSlider.value = Chara1.GetComponent<Status>().hpSlider.value;
                    OpeCharaHPText.GetComponent<Text>().text = Chara1.GetComponent<Status>().hpSlider.value +
                       "/" + Chara1.GetComponent<Status>().MaxHP;
                }
                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon1");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon2");

                //攻撃時間用テキスト
                ATime2.GetComponent<Text>().text = ("" + Knight_Data.SkillTime1.ToString("f2"));
                ATime3.GetComponent<Text>().text = ("" + Knight_Data.SkillTime2.ToString("f2"));
            }
            //現時点でメディック確定
            else if (ChangeChara.nowChara == 1)
            {
                if (PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true)
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Medic");
                }
                else
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Medic_Reverse");
                }
                OpeCharaName.GetComponent<Text>().text = Medic_Data.CharaName;
                OpeCharaJobIcon.sprite = Medic_Data.JobIconImage;
                if (Chara2 != null)
                {
                    OpeCharaHPSlider.maxValue = Chara2.GetComponent<Status>().hpSlider.maxValue;
                    OpeCharaHPSlider.value = Chara2.GetComponent<Status>().hpSlider.value;
                    OpeCharaHPText.GetComponent<Text>().text = Chara2.GetComponent<Status>().hpSlider.value +
                       "/" + Chara2.GetComponent<Status>().MaxHP;
                }

                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon2");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/MedicSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/MedicSkillIcon2");

                //攻撃時間用テキスト
                ATime2.GetComponent<Text>().text = ("" + Medic_Data.SkillTime1.ToString("f2"));
                ATime3.GetComponent<Text>().text = ("" + Medic_Data.SkillTime2.ToString("f2"));
            }
            //現時点でガーディアン確定
            else if (ChangeChara.nowChara == 2)
            {
                if (PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true)
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Guardian");
                }
                else
                {
                    OpeCharaIcon.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaIcon_Guardian_Reverse");
                }
                OpeCharaName.GetComponent<Text>().text = Guardian_Data.CharaName;
                OpeCharaJobIcon.sprite = Guardian_Data.JobIconImage;
                if (Chara3 != null)
                {
                    OpeCharaHPSlider.maxValue = Chara3.GetComponent<Status>().hpSlider.maxValue;
                    OpeCharaHPSlider.value = Chara3.GetComponent<Status>().hpSlider.value;
                    OpeCharaHPText.GetComponent<Text>().text = Chara3.GetComponent<Status>().hpSlider.value +
                       "/" + Chara3.GetComponent<Status>().MaxHP;
                }

                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon1");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/GuardianSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/GuardianSkillIcon2");


                //攻撃時間用テキスト
                ATime2.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime1.ToString("f2"));
                ATime3.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime2.ToString("f2"));
            }

            //左上の攻撃時間表示判定
            CharaAttackText();

            //
            TurnChangeImage();


            //終了判定
            //Player1の倒されたキャラが3体を越えたなら
            if(P1_GP == 3)
            {
                Network_01.gameplayflag = false;
                Network_01.gamestartflag = false;
                if (PhotonNetwork.player.ID == 1)
                {
                    GameLose();
                }
                if (PhotonNetwork.player.ID == 2)
                {
                    Gamewin();
                }
            }
            //Player2の倒されたキャラが3体を越えたなら
            if (P2_GP == 3)
            {
                Network_01.gameplayflag = false;
                Network_01.gamestartflag = false;
                if (PhotonNetwork.player.ID == 2)
                {
                    GameLose();
                }
                if (PhotonNetwork.player.ID == 1)
                {
                    Gamewin();
                }
            }

            //プレイヤー1とプレイヤー2の残りのキャラの数表示
            Debug.Log("プレイヤー1の残りのキャラ" + (3.0 - P1_GP) + "体");
            Debug.Log("プレイヤー2の残りのキャラ" + (3.0 - P2_GP) + "体");

        }


        //ターン切り替えの時の処理
        if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
        {
            //CharaChangeButton1.GetComponent<Button>().interactable = true;
            //CharaChangeButton2.GetComponent<Button>().interactable = true;
            //CharaChangeButton3.GetComponent<Button>().interactable = true;
            AttackButton1.SetActive(true);
            AttackButton2.SetActive(true);
            AttackButton3.SetActive(true);
            TurnText.GetComponent<Text>().text = "My turn";
        }
        else if((TurnCol.P1_Turn == false && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == false && PhotonNetwork.player.ID == 2)) 
        {
            //CharaChangeButton1.GetComponent<Button>().interactable = false;
            //CharaChangeButton2.GetComponent<Button>().interactable = false;
            //CharaChangeButton3.GetComponent<Button>().interactable = false;
            AttackButton1.SetActive(false);
            AttackButton2.SetActive(false);
            AttackButton3.SetActive(false);
            TurnText.GetComponent<Text>().text = "Your turn";
        }
    }

    //左上の攻撃時間テキスト表示判定
    public void CharaAttackText()
    {
        //ナイト
        if (Knight_Data.SkillFlag1 == true)
        {
            CharaAttackTime1.GetComponent<Text>().text = ("" + Knight_Data.SkillTime1.ToString("f2"));
            CharaAttackTime1.SetActive(true);
        }
        if (Knight_Data.SkillFlag2 == true)
        {
            CharaAttackTime1.GetComponent<Text>().text = ("" + Knight_Data.SkillTime2.ToString("f2"));
            CharaAttackTime1.SetActive(true);
        }
        if (Knight_Data.SkillFlag1 == false && Knight_Data.SkillFlag2 == false)
        {
            CharaAttackTime1.SetActive(false);
        }

        //メディック
        if (Medic_Data.SkillFlag1 == true)
        {
            CharaAttackTime2.GetComponent<Text>().text = ("" + Medic_Data.SkillTime1.ToString("f2"));
            CharaAttackTime2.SetActive(true);
        }
        if (Medic_Data.SkillFlag2 == true)
        {
            CharaAttackTime2.GetComponent<Text>().text = ("" + Medic_Data.SkillTime2.ToString("f2"));
            CharaAttackTime2.SetActive(true);
        }
        if (Medic_Data.SkillFlag1 == false && Medic_Data.SkillFlag2 == false)
        {
            CharaAttackTime2.SetActive(false);
        }

        //ガーディアン
        if (Guardian_Data.SkillFlag1 == true)
        {
            CharaAttackTime3.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime1.ToString("f2"));
            CharaAttackTime3.SetActive(true);
        }
        if (Guardian_Data.SkillFlag2 == true)
        {
            CharaAttackTime3.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime1.ToString("f2"));
            CharaAttackTime3.SetActive(true);
        }
        if (Guardian_Data.SkillFlag1 == false && Guardian_Data.SkillFlag2 == false)
        {
            CharaAttackTime3.SetActive(false);
        }
    }

    public static void TurnChangeImage()
    {
        if((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true) ||
           (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true))
        {
            OpeCharaName.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            OpeCharaJobIcon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            OpeCharaHPText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            CharaChangeButton1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            CharaChangeButton2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            CharaChangeButton3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            TimeText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            Camera.transform.rotation = Quaternion.Euler(20.0f, -180.0f, 0.0f);
        }
        else
        {
            OpeCharaName.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            OpeCharaJobIcon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            OpeCharaHPText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

            CharaChangeButton1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            CharaChangeButton2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            CharaChangeButton3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

            TimeText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

            Camera.transform.rotation = Quaternion.Euler(20.0f, -180.0f, 180.0f);

        }
    }


    void Gamewin()
    {
        Debug.Log("GAME CLEAR");
        TurnText.GetComponent<Text>().text = "Game Win";
    }

    void GameLose()
    {
        Debug.Log("GAME LOSE");
        TurnText.GetComponent<Text>().text = "Game Lose";
    }

    //名前とtagの送受信
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(P1_GP);
            stream.SendNext(P2_GP);
        }
        else
        {
            //データの受信
            P1_GP = (int)stream.ReceiveNext();
            P2_GP = (int)stream.ReceiveNext();
        }
    }




}
