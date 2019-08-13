using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ナイトのデータ一覧
public class Knight_Data : MonoBehaviour
{
    //キャラクター名
    public static string CharaName;
    //キャラクタジョブ番号
    public static int job;
    //キャラクターアイコン
    public static Sprite CharaIconImage;
    //ジョブアイコン
    public static Sprite JobIconImage;
    //ミニキャラクターアイコン
    public static Sprite MiniIcon;
    //通常攻撃アイコン
    public static Sprite AttackIcon;
    //スキル攻撃アイコン1
    public static Sprite SkillIcon1;
    //スキル攻撃アイコン2
    public static Sprite SkillIcon2;

    //HP
    public static int MaxHP = 200;
    public static int HP = 200;
    //　HP表示用スライダー
    public static Slider hpSlider;
    //攻撃までの時間
    public static float AttackTime;
    public static float SkillTime1;
    public static float SkillTime2;

    //攻撃までの時間テキスト
    public static GameObject ATText1;
    public static GameObject ATText2;
    public static GameObject ATText3;

    //攻撃したかのフラグ
    public static bool SkillFlag1;
    public static bool SkillFlag2;

    //


    public static Collider Sword;

    public static Slider YourHP;

    //アニメーター 
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //キャラクター情報入力
        CharaName = "ナイト";
        CharaIconImage = Resources.Load<Sprite>("CharaIcon/CharaIcon1");
        MiniIcon = Resources.Load<Sprite>("MiniCharaIcon/MiniIcon1");
        JobIconImage = Resources.Load<Sprite>("JobIcon/knite");
        AttackIcon = Resources.Load<Sprite>("AttackIcon/AttackIcon1");
        SkillIcon1 = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon1");
        SkillIcon2 = Resources.Load<Sprite>("AttackIcon/KnightSkillIcon2");
        hpSlider = GameObject.Find("BackGround").transform.Find("Player1HP").GetComponent<Slider>();
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;
        AttackTime = 3.0f;
        SkillTime1 = 5.0f;
        SkillTime2 = 10.0f;
        SkillFlag1 = false;
        SkillFlag2 = false;
        Sword = GameObject.Find("Sword_Collider").GetComponent<BoxCollider>();
        animator = this.GetComponent<Animator>();
        //剣コライダーをオンにする
        Sword.enabled = false;


        ATText2 = GameObject.Find("ATime2");
        ATText3 = GameObject.Find("ATime3");
        //ATText2.SetActive(false);
        //ATText3.SetActive(false);

        if (PhotonNetwork.player.ID == 1)
        {
            this.tag = "Player1";
        }
        else
        {
            this.tag = "Player2";
        }

    }

    // Update is called once per frame
    void Update()
    {
        ATText2.GetComponent<Text>().text = ("" + SkillTime1.ToString("f2"));
        ATText3.GetComponent<Text>().text = ("" + SkillTime2.ToString("f2"));
        //hpSlider.value -= 1;

        //スキル1発動
        if (SkillFlag1 == true && SkillFlag2 == false)
        {
            //ATText2.SetActive(true);

            SkillTime1 -= Time.deltaTime;
            if (SkillTime1 <= 0)
            {
                animator.SetBool("Skill1_Trigger", true);
                SkillFlag1 = false;
                SkillTime1 = 5.0f;
            }
        }

        //スキル2発動
        if (SkillFlag2 == true && SkillFlag1 == false)
        {
            //ATText2.SetActive(true);

            SkillTime2 -= Time.deltaTime;
            if (SkillTime2 <= 0)
            {
                animator.SetBool("Skill2_Trigger", true);
                SkillFlag2 = false;
                SkillTime2 = 10.0f;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.player.ID == 1 && other.tag == "Player1")
        {
            if (other.name == "P1_Chara2")
            {
                other.GetComponent<Status>().hpSlider.value -= 
                    (int)(this.GetComponent<Status>().Attack / ((1 + other.GetComponent<Status>().Defense) / 10));
                Debug.Log(other + "に"+ (int)(this.GetComponent<Status>().Attack / 
                    ((1 + other.GetComponent<Status>().Defense) / 10)) + "ダメージ");
            }
            else if (other.name == "P1_Chara3")
            {
                other.GetComponent<Status>().hpSlider.value -=
                    (int)(this.GetComponent<Status>().Attack / ((1 + other.GetComponent<Status>().Defense) / 10));
                Debug.Log(other + "に" + (int)(this.GetComponent<Status>().Attack / 
                    ((1 + other.GetComponent<Status>().Defense) / 10)) + "ダメージ");
            }
        }
    }
    public void ColliderReset()
    {
        Sword.enabled = false;
    }
}
