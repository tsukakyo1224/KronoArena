using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //-------------------------UI(イメージ、テキスト)-------------------------

    //キャラごとの攻撃までの時間用テキスト(左上)
    public static GameObject CharaAttackTime1;
    public static GameObject CharaAttackTime2;
    public static GameObject CharaAttackTime3;

    //キャラごとの攻撃までの時間用砂時計(左上)
    public static Image CharaAttackHours1;
    public static Image CharaAttackHours2;
    public static Image CharaAttackHours3;

    //キャラ切り替えボタン用オブジェクト
    public static GameObject CharaChangeButton1;
    public static GameObject CharaChangeButton2;
    public static GameObject CharaChangeButton3;

    //キャラクターバー(左上)
    public static GameObject CharaBar1;
    public static GameObject CharaBar2;
    public static GameObject CharaBar3;

    //キャラクターミニHP(左上)
    public static GameObject CharaHP1;
    public static GameObject CharaHP2;
    public static GameObject CharaHP3;

    //キャラ攻撃ボタン用オブジェクト
    public static GameObject AttackButton1;
    public static GameObject AttackButton2;
    public static GameObject AttackButton3;

    //キャラ攻撃時間テキスト
    //public static GameObject ATime2;
    //public static GameObject ATime3;
    public static GameObject SkillGaugeIcon2;
    public static GameObject SkillGaugeIcon3;

    //ターン用テキスト
    public static GameObject TurnText;

    //タイマーテキスト
    public static GameObject TimeText;

    //操作キャラクター用オブジェクト
    public static GameObject OpeCharaIcon;  //キャラアイコン
    public static GameObject OpeCharaName;  //キャラの名前
    public static Image OpeCharaJobIcon;   //ジョブアイコン
    public static Slider OpeCharaHPSlider;    //キャラのHP
    public static GameObject OpeCharaHPText;    //キャラのHPテキスト

    //結果画面(Win or Lose)
    public static Image WinLose;

    //+3秒用テキスト
    public static Image SecondUp;

    //ターン用イメージ
    public static Image TurnImage;

    //ターン切り替えボタン
    public static GameObject TurnChangeButton;
    //砂時計の鎖
    public static Image Kusari;

    //UIの親
    private GameObject Button;
    private GameObject BackGround;

    public GameObject TurnChangeUI;

    //-------------------------オブジェクト-------------------------

    //キャラクター用オブジェクト
    public static GameObject Chara1;
    public static GameObject Chara2;
    public static GameObject Chara3;

    //キャラの頭上の三角
    public static GameObject OperateImage1;
    public static GameObject OperateImage2;
    public static GameObject OperateImage3;

    //カメラ
    public static GameObject Camera;

    //Photon同期用
    //private PhotonView photonView;
    //private PhotonTransformView photonTransformView;


    //ゲーム勝利用ポイント
    public static int P1_GP = 0;
    public static int P2_GP = 0;

    private AudioSource BGM;
    private AudioSource MedicAudio;
    private AudioSource BattleStartAudio;

    //-------------------------フラグ-------------------------
    //キャラクター生成フラグ
    public static bool CharaMakeFlag = false;

    //ゲーム準備フラグ

    //ゲームスタート時フラグ
    public static bool GameStartFlag = false;

    //ゲームプレイ中フラグ
    public static bool GamePlayFlag = false;

    //カメラフラグ(スタート時にキャラクターをズームして写すカメラフラグ)(falseで)
    public static bool CameraFlag = false;

    public static bool CameraStartFlag = false;

    //ターン変更時に時間を止めるフラグ
    public bool StopTimeFlag = false;

    //バフのフラグ、バフの時間
    public bool BuffFlag = false;
    public float BuffTime = 0.0f;
    public int BuffNum = 0; //攻撃UP:1, 防御UP:2

    //-------------------------アニメーター-------------------------
    public Animator CharaBarAnim1;
    public Animator CharaButtonAnim1;
    public Animator CharaBarAnim2;
    public Animator CharaButtonAnim2;
    public Animator CharaBarAnim3;
    public Animator CharaButtonAnim3;


    // Start is called before the first frame update
    void Start()
    {

        //キャラごとの攻撃までの時間用テキスト代入
        //CharaAttackTime1 = GameObject.Find("AttackTime1");
        //CharaAttackTime2 = GameObject.Find("AttackTime2");
        //CharaAttackTime3 = GameObject.Find("AttackTime3");

        CharaAttackHours1 = GameObject.Find("Character1_HourGlass").GetComponent<Image>();
        CharaAttackHours2 = GameObject.Find("Character2_HourGlass").GetComponent<Image>();
        CharaAttackHours3 = GameObject.Find("Character3_HourGlass").GetComponent<Image>();
        CharaAttackHours1.gameObject.SetActive(false);
        CharaAttackHours2.gameObject.SetActive(false);
        CharaAttackHours3.gameObject.SetActive(false);

        //キャラクターバー用オブジェクト代入
        CharaBar1 = GameObject.Find("CharacterBar1");
        CharaBar2 = GameObject.Find("CharacterBar2");
        CharaBar3 = GameObject.Find("CharacterBar3");
        CharaBarAnim1 = CharaBar1.GetComponent<Animator>();
        CharaBarAnim2 = CharaBar2.GetComponent<Animator>();
        CharaBarAnim3 = CharaBar3.GetComponent<Animator>();

        CharaHP1 = GameObject.Find("Player1HP");
        CharaHP2 = GameObject.Find("Player2HP");
        CharaHP3 = GameObject.Find("Player3HP");


        //キャラ攻撃ボタン用オブジェクト
        AttackButton1 = GameObject.Find("Attack1");
        AttackButton2 = GameObject.Find("Attack2");
        AttackButton3 = GameObject.Find("Attack3");

        //攻撃時間用テキスト
        //ATime2 = GameObject.Find("ATime2");
        //ATime3 = GameObject.Find("ATime3");

        //キャラ切り替えボタン用オブジェクト
        CharaChangeButton1 = GameObject.Find("ChangeChara1");
        CharaChangeButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Knight");
        CharaButtonAnim1 = CharaChangeButton1.GetComponent<Animator>();
        CharaChangeButton2 = GameObject.Find("ChangeChara2");
        CharaChangeButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Medic");
        CharaButtonAnim2 = CharaChangeButton2.GetComponent<Animator>();
        CharaChangeButton3 = GameObject.Find("ChangeChara3");
        CharaChangeButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("CharaIcon/CharaMiniIcon_Guardion");
        CharaButtonAnim3 = CharaChangeButton3.GetComponent<Animator>();

        //操作キャラクター用オブジェクト(右下)
        OpeCharaIcon = GameObject.Find("OpeCharaIcon");
        OpeCharaName = GameObject.Find("OpeCharaName");
        OpeCharaJobIcon = GameObject.Find("OpeCharaJobIcon").GetComponent<Image>();
        OpeCharaHPSlider = GameObject.Find("OpeCharaHPSlider").GetComponent<Slider>();
        OpeCharaHPText = GameObject.Find("OpeCharaHPText");

        //砂時計ボタン(ターンチェンジ)
        TurnChangeButton = GameObject.Find("HourGlass");
        TurnChangeButton.GetComponent<Button>().enabled = false;
        //鎖
        Kusari = GameObject.Find("Kusari").GetComponent<Image>();
        Kusari.gameObject.SetActive(true);

        //ターン表示用Image
        TurnImage = GameObject.Find("TurnImage").GetComponent<Image>();

        TimeText = GameObject.Find("Time");

        //
        TurnChangeUI = GameObject.Find("TurnChangeUI");
        //TurnChangeUI.GetComponent<Image>().enabled = false;
        TurnChangeUI.SetActive(false);

        //+3秒テキスト
        //SecondUp = GameObject.Find("+3seconds").GetComponent<Image>();
        //SecondUp.gameObject.SetActive(false);

        //スキルゲージ
        SkillGaugeIcon2 = GameObject.Find("SkillGaugeIcon2");
        SkillGaugeIcon3 = GameObject.Find("SkillGaugeIcon3");

        //カメラオブジェクト
        Camera = GameObject.Find("Main Camera");

        WinLose = GameObject.Find("WinLose").GetComponent<Image>();
        WinLose.gameObject.SetActive(false);

        //最初は表示に
        AttackButton1.SetActive(true);
        AttackButton2.SetActive(true);
        AttackButton3.SetActive(true);

        //最初は表示
        CharaChangeButton1.GetComponent<Button>().interactable = true;
        CharaChangeButton2.GetComponent<Button>().interactable = true;
        CharaChangeButton3.GetComponent<Button>().interactable = true;

        //オーディオ
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BGM = audioSources[0];
        MedicAudio = audioSources[1];
        BattleStartAudio = audioSources[2];

        //UIの親
        Button = GameObject.Find("Button");
        BackGround = GameObject.Find("BackGround");
        Button.SetActive(false);
        BackGround.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Network_01.gameplayflag == true &&
            GameObject.Find("GameManager").GetComponent<StartCol>().TimeStartFlag == true)
        {
            //キャラクターを入れる
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
            //左上のバーを初期位置に
            //CharaChangeButton1.GetComponent<RectTransform>().localPosition = new Vector3(-305.0f, 221.9f, 0.0f);
            //CharaBar1.GetComponent<RectTransform>().localPosition = new Vector3(-468.0f, 216.5f, 0.0f);
            //CharaHP1.GetComponent<RectTransform>().localPosition = new Vector3(-388.0f, 195.0f, 0.0f);

            //CharaChangeButton2.GetComponent<RectTransform>().localPosition = new Vector3(-305.0f, 137.2f, 0.0f);
            //CharaBar2.GetComponent<RectTransform>().localPosition = new Vector3(-468.0f, 133.0f, 0.0f);
            //CharaHP2.GetComponent<RectTransform>().localPosition = new Vector3(-388.0f, 110.0f, 0.0f);

            //CharaChangeButton3.GetComponent<RectTransform>().localPosition = new Vector3(-305.0f, 50.0f, 0.0f);
            //CharaBar3.GetComponent<RectTransform>().localPosition = new Vector3(-468.0f, 46.0f, 0.0f);
            //CharaHP3.GetComponent<RectTransform>().localPosition = new Vector3(-388.0f, 25.0f, 0.0f);

            //キャラの頭上の三角
            if(Chara1 != null)
            {
                OperateImage1 = Chara1.transform.GetChild(5).gameObject;
                OperateImage1.SetActive(false);
            }
            if (Chara2 != null)
            {
                OperateImage2 = Chara2.transform.GetChild(5).gameObject;
                OperateImage2.SetActive(false);
            }
            if (Chara3 != null)
            {
                OperateImage3 = Chara3.transform.GetChild(5).gameObject;
                OperateImage3.SetActive(false);
            }

            //操作キャラ変更時に操作キャラクター表示の変更
            //現時点でナイト確定
            if (ChangeChara.nowChara == 0)
            {
                if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
                    (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
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
                if (Chara1 != null)
                {
                    OpeCharaHPSlider.maxValue = Chara1.GetComponent<Status>().MaxHP;
                    OpeCharaHPSlider.value = Chara1.GetComponent<Status>().HP;
                    OpeCharaHPText.GetComponent<Text>().text = Chara1.GetComponent<Status>().HP +
                       "/" + Chara1.GetComponent<Status>().MaxHP;

                    //頭上の三角を付ける
                    OperateImage1.SetActive(true);
                }
                else
                {
                    OpeCharaHPSlider.value = 0;
                    OpeCharaHPText.GetComponent<Text>().text = 0 +
                       "/" + OpeCharaHPSlider.maxValue;
                }
                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon1");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon2");



                //スキルゲージ
                if(Knight_Data.SkillFlag1 == true)
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = Knight_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = 0.0f;
                }
                if(Knight_Data.SkillFlag2 == true)
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = Knight_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = 0.0f;
                }

                //攻撃時間用テキスト
                //ATime2.GetComponent<Text>().text = ("" + Knight_Data.SkillTime1.ToString("f2"));
                //ATime3.GetComponent<Text>().text = ("" + Knight_Data.SkillTime2.ToString("f2"));


            }
            //現時点でメディック確定
            else if (ChangeChara.nowChara == 1)
            {
                if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
                    (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
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
                    OpeCharaHPSlider.maxValue = Chara2.GetComponent<Status>().MaxHP;
                    OpeCharaHPSlider.value = Chara2.GetComponent<Status>().HP;
                    OpeCharaHPText.GetComponent<Text>().text = Chara2.GetComponent<Status>().HP +
                       "/" + Chara2.GetComponent<Status>().MaxHP;

                    //頭上の三角を付ける
                    OperateImage2.SetActive(true);
                }
                else
                {
                    OpeCharaHPSlider.value = 0;
                    OpeCharaHPText.GetComponent<Text>().text = 0 +
                       "/" + OpeCharaHPSlider.maxValue;
                }

                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon2");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/MedicSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/MedicSkillIcon2");

                //スキルゲージ
                if (Medic_Data.SkillFlag1 == true)
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = Medic_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = 0.0f;
                }
                if (Medic_Data.SkillFlag2 == true)
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = Medic_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = 0.0f;
                }

                //攻撃時間用テキスト
                //ATime2.GetComponent<Text>().text = ("" + Medic_Data.SkillTime1.ToString("f2"));
                //ATime3.GetComponent<Text>().text = ("" + Medic_Data.SkillTime2.ToString("f2"));
            }
            //現時点でガーディアン確定
            else if (ChangeChara.nowChara == 2)
            {
                if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
                    (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
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
                    OpeCharaHPSlider.maxValue = Chara3.GetComponent<Status>().MaxHP;
                    OpeCharaHPSlider.value = Chara3.GetComponent<Status>().HP;
                    OpeCharaHPText.GetComponent<Text>().text = Chara3.GetComponent<Status>().HP +
                       "/" + Chara3.GetComponent<Status>().MaxHP;
                    //頭上の三角を付ける
                    OperateImage3.SetActive(true);
                }
                else
                {
                    OpeCharaHPSlider.value = 0;
                    OpeCharaHPText.GetComponent<Text>().text = 0 +
                       "/" + OpeCharaHPSlider.maxValue;
                }
                //攻撃ボタン
                AttackButton1.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/attackIcon1");
                AttackButton2.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/GuardianSkillIcon1");
                AttackButton3.GetComponent<Image>().sprite = Resources.Load<Sprite>("AttackIcon/GuardianSkillIcon2");


                //スキルゲージ
                if (Guardian_Data.SkillFlag1 == true)
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = Guardian_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon2.GetComponent<Image>().fillAmount = 0.0f;
                }
                if (Guardian_Data.SkillFlag2 == true)
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = Guardian_Data.Skill_Start;
                }
                else
                {
                    SkillGaugeIcon3.GetComponent<Image>().fillAmount = 0.0f;
                }

                //攻撃時間用テキスト
                //ATime2.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime1.ToString("f2"));
                //ATime3.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime2.ToString("f2"));
            }

            //キャラが死んだ時にチェンジできないように
            if(Chara1 == null)
            {
                CharaChangeButton1.GetComponent<Button>().interactable = false;
            }
            else
            {
                CharaChangeButton1.GetComponent<Button>().interactable = true;
            }

            if(Chara2 == null)
            {
                CharaChangeButton2.GetComponent<Button>().interactable = false;
            }
            else
            {
                CharaChangeButton2.GetComponent<Button>().interactable = true;
            }

            if (Chara3 == null)
            {
                CharaChangeButton3.GetComponent<Button>().interactable = false;
            }
            else
            {
                CharaChangeButton3.GetComponent<Button>().interactable = true;
            }

            //左上の小さい砂時計表示判定
            CharaAttackText();

            //ターン入れ替え時に画面反転
            TurnChangeImage();

            //キャラの数を数える
            //CharaCheck();

            //バフ判定
            if(BuffFlag == true)
            {
                BuffTime -= Time.deltaTime;
                if(BuffTime <= 0.0f)
                {
                    BuffClose();
                }
            }



            //----------------------------------終了判定----------------------------------
            //Player1の倒されたキャラが3体を越えたなら
            if (P1_GP == 3)
            {
                Network_01.gameplayflag = false;
                Network_01.gamestartflag = false;
                if (PhotonNetwork.player.ID == 1)
                {
                    GameLose();
                }
                if (PhotonNetwork.player.ID == 2)
                {
                    GameWin();
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
                    GameWin();
                }
            }

        }

        //-------------------------------------ターン切り替えの時の処理-------------------------------------
        if ((TurnCol.P1_Turn == true && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == true && PhotonNetwork.player.ID == 2))
        {
            //10秒以上経ったらターン入れ替えボタンを押せるように
            if(TimerScript.HourGlassFlag == true)
            {
                TurnChangeButton.GetComponent<Button>().enabled = true;
                Kusari.gameObject.SetActive(false);
            }
            AttackButton1.GetComponent<Image>().enabled = true;
            AttackButton1.GetComponent<Button>().enabled = true;
            AttackButton2.GetComponent<Image>().enabled = true;
            AttackButton2.GetComponent<Button>().enabled = true;
            AttackButton3.GetComponent<Image>().enabled = true;
            AttackButton3.GetComponent<Button>().enabled = true;
            TurnImage.sprite = Resources.Load<Sprite>("YourTurn");
            SkillGaugeIcon2.SetActive(true);
            SkillGaugeIcon3.SetActive(true);
        }
        else if((TurnCol.P1_Turn == false && PhotonNetwork.player.ID == 1) ||
            (TurnCol.P2_Turn == false && PhotonNetwork.player.ID == 2)) 
        {
            TurnChangeButton.GetComponent<Button>().enabled = false;
            Kusari.gameObject.SetActive(true);
            AttackButton1.GetComponent<Image>().enabled = false;
            AttackButton1.GetComponent<Button>().enabled = false;
            AttackButton2.GetComponent<Image>().enabled = false;
            AttackButton2.GetComponent<Button>().enabled = false;
            AttackButton3.GetComponent<Image>().enabled = false;
            AttackButton3.GetComponent<Button>().enabled = false;
            TurnImage.sprite = Resources.Load<Sprite>("EnemyTurn");
            SkillGaugeIcon2.SetActive(false);
            SkillGaugeIcon3.SetActive(false);
        }
    }
    //-------------------------------------UpDate終了-------------------------------------

    //-------------------------------------時間を止める、動かす-------------------------------------
    public void StopTime()
    {
        TurnChangeUI.SetActive(true);
        StopTimeFlag = true;
        //Time.timeScale = 0f;
    }

    public void StartTime()
    {
        StopTimeFlag = false;
        TurnChangeUI.SetActive(false);
        //Time.timeScale = 1f;
    }

    //-------------------------------------砂時計でターンチェンジした際に自分のキャラにバフ-------------------------------------
    public void Buff()
    {
        if (BuffTime > 0.0f)
        {
            BuffClose();
        }

        //int random = Random.Range(1,100);    //ランダム関数
        BuffTime = TimerScript.TotalTime * 3.0f;
        BuffFlag = true;

        //キャラクター攻撃力アップ
        if(TimerScript.TotalTime >= 10.0f)
        {
            if (Chara1 != null)
            {
                Chara1.GetComponent<Status>().Attack += 200.0f;
                Chara1.GetComponent<Status>().Magic_Attack += 200.0f;
            }
            if(Chara2 != null)
            {
                Chara2.GetComponent<Status>().Attack += 200.0f;
                Chara2.GetComponent<Status>().Magic_Attack += 200.0f;
            }
            if(Chara3 != null)
            {
                Chara3.GetComponent<Status>().Attack += 200.0f;
                Chara3.GetComponent<Status>().Magic_Attack += 200.0f;
            }

            BuffNum = 1;

            Debug.Log(BuffTime + "秒間、攻撃力が100アップ。");
        }
        //キャラクター防御アップ
        else if(TimerScript.TotalTime < 10.0f)
        {
            if(Chara1 != null)
            {
                Chara1.GetComponent<Status>().Defense += 50.0f;
                Chara1.GetComponent<Status>().Magic_Defense += 50.0f;
            }
            if(Chara2 != null)
            {
                Chara2.GetComponent<Status>().Defense += 50.0f;
                Chara2.GetComponent<Status>().Magic_Defense += 50.0f;
            }
            if(Chara3 != null)
            {
                Chara3.GetComponent<Status>().Defense += 50.0f;
                Chara3.GetComponent<Status>().Magic_Defense += 50.0f;
            }

            BuffNum = 2;

            Debug.Log(BuffTime + "秒間、防御力が100アップ。");
        }

    }

    public void BuffClose()
    {
        if(BuffNum == 1)
        {
            if (Chara1 != null)
            {
                Chara1.GetComponent<Status>().Attack -= 200.0f;
                Chara1.GetComponent<Status>().Magic_Attack -= 200.0f;
            }
            if (Chara2 != null)
            {
                Chara2.GetComponent<Status>().Attack -= 200.0f;
                Chara2.GetComponent<Status>().Magic_Attack -= 200.0f;
            }
            if (Chara3 != null)
            {
                Chara3.GetComponent<Status>().Attack -= 200.0f;
                Chara3.GetComponent<Status>().Magic_Attack -= 200.0f;
            }
            Debug.Log("攻撃バフが終わった。");
        }
        else if (BuffNum == 2)
        {
            if (Chara1 != null)
            {
                Chara1.GetComponent<Status>().Defense -= 50.0f;
                Chara1.GetComponent<Status>().Magic_Defense -= 50.0f;
            }
            if (Chara2 != null)
            {
                Chara2.GetComponent<Status>().Defense -= 50.0f;
                Chara2.GetComponent<Status>().Magic_Defense -= 50.0f;
            }
            if (Chara3 != null)
            {
                Chara3.GetComponent<Status>().Defense -= 50.0f;
                Chara3.GetComponent<Status>().Magic_Defense -= 50.0f;
            }

            Debug.Log("防御バフが終わった。");
        }
        BuffTime = 0.0f;
        BuffFlag = false;
    }

    //-------------------------------------左上の攻撃時間砂時計用-------------------------------------
    public void CharaAttackText()
    {
        //ナイト
        if (Knight_Data.SkillFlag1 == true || Knight_Data.SkillFlag2 == true)
        {
            CharaAttackHours1.gameObject.SetActive(true);
        }
        else if (Knight_Data.SkillFlag1 == false && Knight_Data.SkillFlag2 == false)
        {
            CharaAttackHours1.gameObject.SetActive(false);
        }

        //メディック
        if (Medic_Data.SkillFlag1 == true || Medic_Data.SkillFlag2 == true)
        {
            CharaAttackHours2.gameObject.SetActive(true);
        }
        else if (Medic_Data.SkillFlag1 == false && Medic_Data.SkillFlag2 == false)
        {
            CharaAttackHours2.gameObject.SetActive(false);
        }

        //ガーディアン
        if (Guardian_Data.SkillFlag1 == true || Guardian_Data.SkillFlag2 == true)
        {
            //CharaAttackTime3.GetComponent<Text>().text = ("" + Guardian_Data.SkillTime1.ToString("f2"));
            //CharaAttackTime3.SetActive(true);
            CharaAttackHours3.gameObject.SetActive(true);
        }
        else if (Guardian_Data.SkillFlag1 == false && Guardian_Data.SkillFlag2 == false)
        {
            //CharaAttackTime3.SetActive(false);
            CharaAttackHours3.gameObject.SetActive(false);
        }
    }

    //-------------------------------------ターン入れ替え時にUI反転-------------------------------------
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

            WinLose.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            TurnImage.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        else
        {
            OpeCharaName.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            OpeCharaJobIcon.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            OpeCharaHPText.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

            CharaChangeButton1.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            CharaChangeButton2.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            CharaChangeButton3.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

            WinLose.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
            TurnImage.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
    }
    //-------------------------------------スタート前処理------------------------------------
    public void UIAnim()
    {
        BackGround.SetActive(true);
        Button.SetActive(true);
        GameStartFlag = true;
        GamePlayFlag = true;
    }

    //-------------------------------------オーディオ------------------------------------
    public void AudioPlay()
    {
        MedicAudio.PlayOneShot(MedicAudio.clip);
    }

    public void BattleStartPlay()
    {
        BattleStartAudio.PlayOneShot(BattleStartAudio.clip);
        //BGMPlayを0.5秒後に呼び出す
        Invoke("BGMPlay", 0.5f);

    }

    //BGM再生
    public void BGMPlay()
    {
        GameObject.Find("GameManager").GetComponent<StartCol>().TimeStartFlag = true;
        Debug.Log("Start");
        BGM.PlayOneShot(BGM.clip);
    }

    //------------------------------------勝敗判定------------------------------------
    void GameWin()
    {
        Debug.Log("GAME CLEAR");
        WinLose.sprite = Resources.Load<Sprite>("Win");
        WinLose.gameObject.SetActive(true);
    }

    void GameLose()
    {
        Debug.Log("GAME LOSE");
        WinLose.sprite = Resources.Load<Sprite>("Lose");
        WinLose.gameObject.SetActive(true);
    }
}
