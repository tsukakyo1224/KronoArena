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

    //攻撃までの時間
    public static float SkillTime1;
    public static float SkillTime2;

    //持続時間
    public static float Skill1_Limit;
    public static float Skill2_Limit;

    //攻撃までの時間テキスト
    public static GameObject ATText1;
    public static GameObject ATText2;
    public static GameObject ATText3;

    //攻撃したかのフラグ
    public static bool SkillFlag1;
    public static bool SkillFlag2;

    //持続時間フラグ
    public static bool LimitFlag1;
    public static bool LimitFlag2;

    public static Slider YourHP;

    //アニメーター 
    private Animator animator;

    private PhotonView photonView;


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
        SkillTime1 = 20.0f;
        SkillTime2 = 10.0f;
        Skill1_Limit = 3.0f;
        Skill2_Limit = 10.0f;
        SkillFlag1 = false;
        SkillFlag2 = false;
        LimitFlag1 = false;
        LimitFlag2 = false;

        animator = this.GetComponent<Animator>();

        photonView = PhotonView.Get(this);

    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.isMine)
        {
            //スキル1発動
            if (SkillFlag1 == true && SkillFlag2 == false)
            {
                //スキル1時間減少
                SkillTime1 -= Time.deltaTime;
                //スキル1時間が0になったら発動
                if (SkillTime1 <= 0)
                {
                    this.GetComponent<Status>().Attack += 100.0f;
                    animator.SetBool("Skill1_Trigger", true);
                    SkillFlag1 = false;
                    SkillTime1 = 20.0f;
                    //持続時間フラグをオン
                    LimitFlag1 = true;
                    Debug.Log("ナイトの回転攻撃!");
                }
            }

            //スキル1の持続時間が終わるまで
            if (LimitFlag1 == true)
            {
                Skill1_Limit -= Time.deltaTime;
                if (Skill1_Limit <= 0)
                {
                    this.GetComponent<Status>().Attack -= 100.0f;
                    LimitFlag1 = false;
                    Skill1_Limit = 3.0f;
                }

            }

            //スキル2発動
            if (SkillFlag2 == true && SkillFlag1 == false)
            {
                //スキル2時間減少
                SkillTime2 -= Time.deltaTime;
                //スキル2時間が0になったら発動
                if (SkillTime2 <= 0)
                {
                    animator.SetBool("Skill2_Trigger", true);
                    SkillFlag2 = false;
                    SkillTime2 = 10.0f;
                    this.GetComponent<Status>().Attack += 300.0f;
                    LimitFlag2 = true;   //持続時間フラグをオン
                    Debug.Log("ナイトの攻撃力が300UP!");
                }
            }
            //スキル2の持続時間が終わるまで
            if (LimitFlag2 == true)
            {
                Skill2_Limit -= Time.deltaTime;
                if (Skill2_Limit <= 0)
                {
                    this.GetComponent<Status>().Attack -= 300.0f;
                    LimitFlag2 = false;
                    Skill2_Limit = 10.0f;
                    Debug.Log("ナイトの攻撃力が元に戻った");
                }

            }
        }
    }

    //ダメージ計算
    void OnTriggerExit(Collider other)
    {
        if (other.tag != this.tag)
        {
            other.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Attack / ((1 + other.GetComponent<Status>().Defense) / 10));
            Debug.Log(other.tag);
            Debug.Log(other + "に" + (int)(this.GetComponent<Status>().Attack / 
                ((1 + other.GetComponent<Status>().Defense) / 10)) + "ダメージ");

        }
    }


    //名前とtagの送受信
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(this.name);
            stream.SendNext(this.tag);
        }
        else
        {
            //データの受信
            this.name = (string)stream.ReceiveNext();
            this.tag = (string)stream.ReceiveNext();
        }
    }


}
