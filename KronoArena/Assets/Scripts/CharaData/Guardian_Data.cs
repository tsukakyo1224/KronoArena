using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guardian_Data : MonoBehaviour
{
    //キャラクター名
    public static string CharaName;
    //キャラクタジョブ番号
    public static int job;
    //キャラクターアイコン
    public static Sprite CharaIconImage;
    //ジョブアイコン
    public static Sprite JobIconImage;

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
    public static bool AttackFlag;

    //持続時間フラグ
    public static bool LimitFlag1;
    public static bool LimitFlag2;

    //身代わりフラグ
    public bool GuardFlag;

    //アニメーター 
    private Animator animator;

    //Photonの
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        CharaName = "ガーディアン";
        JobIconImage = Resources.Load<Sprite>("JobIcon/Guardian");
        SkillTime1 = 15.0f;     //スキル1発動時間
        SkillTime2 = 20.0f;     //スキル2発動時間
        Skill1_Limit = 30.0f;   //スキル1持続時間
        Skill2_Limit = 10.0f;   //スキル2持続時間
        SkillFlag1 = false;     //スキル1発動判定フラグ
        SkillFlag2 = false;     //スキル2発動判定フラグ
        LimitFlag1 = false;     //スキル1持続判定フラグ
        LimitFlag2 = false;     //スキル2持続判定フラグ

        AttackFlag = false;

        GuardFlag = false;

        animator = this.GetComponent<Animator>();

        photonView = PhotonView.Get(this);

    }

    // Update is called once per frame
    void Update()
    {

        if (photonView.isMine)
        {
            //スキル1発動
            //発動効果：物理防御力と魔法防御力を200UP
            if (SkillFlag1 == true && SkillFlag2 == false)
            {
                //スキル1時間減少
                SkillTime1 -= Time.deltaTime;
                //スキル1時間が0になったら発動
                if (SkillTime1 <= 0)
                {
                    this.GetComponent<Status>().Defense += 200.0f;
                    this.GetComponent<Status>().Magic_Defense += 200.0f;

                    //アニメーション発動
                    animator.SetBool("Skill1_Trigger", true);
                    //スキル1発動系を初期値に
                    SkillFlag1 = false;
                    SkillTime1 = 15.0f;

                    //持続時間フラグをオン
                    LimitFlag1 = true;
                    Debug.Log("ガーディアンの物理防御力と魔法防御力が200UP!");
                }
            }

            //スキル1の持続時間が終わるまで
            if (LimitFlag1 == true)
            {
                Skill1_Limit -= Time.deltaTime;
                if (Skill1_Limit <= 0)
                {
                    this.GetComponent<Status>().Defense -= 200.0f;
                    this.GetComponent<Status>().Magic_Defense -= 200.0f;
                    //持続時間フラグを初期値に
                    LimitFlag1 = false;
                    Skill1_Limit = 30.0f;
                    Debug.Log("ガーディアンの物理防御力と魔法防御力が元に戻った");
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
                    //身代わりフラグをオン
                    GuardFlag = true;

                    this.GetComponent<Status>().Defense += 100.0f;
                    this.GetComponent<Status>().Magic_Defense += 100.0f;

                    animator.SetBool("Skill2_Trigger", true);
                    //スキル1発動系を初期値に
                    SkillFlag2 = false;
                    SkillTime2 = 20.0f;
                    //持続時間フラグをオン
                    LimitFlag2 = true;
                    Debug.Log("ガーディアン肩代わりの効果が発動");

                }
            }

            //スキル2の持続時間が終わるまで
            if (LimitFlag2 == true)
            {
                Skill2_Limit -= Time.deltaTime;

                if (Skill2_Limit <= 0)
                {
                    this.GetComponent<Status>().Defense -= 100.0f;
                    this.GetComponent<Status>().Magic_Defense -= 100.0f;
                    //持続時間フラグを初期値に
                    LimitFlag2 = false;
                    Skill2_Limit = 10.0f;
                    GuardFlag = false;
                    Debug.Log("ガーディアン肩代わりの効果が終わった");
                }
            }
        }

    }


    //ダメージ計算
    void OnTriggerExit(Collider other)
    {
        if (other.tag != this.tag)
        {
            Guardian();
            if(AttackFlag == false)
            {
                other.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Attack / ((1 + other.GetComponent<Status>().Defense) / 10));
                Debug.Log(other + "に" + (int)(this.GetComponent<Status>().Attack /
                    ((1 + other.GetComponent<Status>().Defense) / 10)) + "ダメージ");
            }
            AttackFlag = false;
        }
    }

    //周りにガーディアンがいて、ガーディアンが身代わりをしていたらガーディアンに攻撃
    void Guardian()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player1");
        if (PhotonNetwork.player.ID == 2)
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //対象キャラとの距離表示
            if (obj.GetComponent<Status>().Name == "Guardian" && dist < 2.0)
            {
                if (obj.GetComponent<Guardian_Data>().GuardFlag == true)
                {
                    obj.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 10));
                    AttackFlag = true;
                }
            }
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
