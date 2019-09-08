
﻿using System.Collections;
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

    //攻撃判定用時間
    public static float Skill_Start;

    //攻撃したかのフラグ
    public static bool SkillFlag1;
    public static bool SkillFlag2;
    public static bool AttackFlag;

    //持続時間フラグ
    public static bool LimitFlag1;
    public static bool LimitFlag2;

    //エフェクト用フラグ
    public static bool EffectFlag;

    public static Slider YourHP;

    //アニメーター 
    private Animator animator;

    private PhotonView photonView;

    //ナイトのエフェクト
    [SerializeField] private static GameObject Skill1_Set;
    [SerializeField] private static GameObject Skill1;
    [SerializeField] private static GameObject Skill2_Set;
    [SerializeField] private static GameObject Skill2;

    //オーディオ
    private AudioSource AttackAudio;

    private GameObject SkillArea;

    //強制終了用
    public float EndTime = 0.0f;

    //
    public bool RollFlag;


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
        SkillTime1 = 15.0f;
        SkillTime2 = 5.0f;
        Skill1_Limit = 3.0f;
        Skill2_Limit = 10.0f;
        Skill_Start = 0.0f;
        SkillFlag1 = false;
        SkillFlag2 = false;
        LimitFlag1 = false;
        LimitFlag2 = false;

        RollFlag = false;

        AttackFlag = false;

        EffectFlag = false;

        animator = this.GetComponent<Animator>();

        photonView = GetComponent<PhotonView>();

        //エフェクト呼び出し
        Skill1_Set = Resources.Load<GameObject>("Knight_RollSet");
        Skill1 = Resources.Load<GameObject>("Knight_Roll");
        Skill2_Set = Resources.Load<GameObject>("Knight_BuffSet");
        Skill2 = Resources.Load<GameObject>("Knight_Buff");

        //オーディオ代入
        AttackAudio = GetComponent<AudioSource>();


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
                Skill_Start += 1.0f / SkillTime1 * Time.deltaTime;
                //Debug.Log(Skill_Start);
                //SkillTime1 -= Time.deltaTime;

                //待機エフェクト発動
                if (EffectFlag == false)
                {
                    photonView.RPC("Knight_Effect", PhotonTargets.All, 1);
                    //animator.SetBool("Skill1", true);
                }

                //スキル1時間が0になったら発動
                if (Skill_Start >= 1.0f)
                {

                    //エフェクト発動
                    photonView.RPC("Knight_Effect", PhotonTargets.All, 2);
                    animator.SetBool("Skill1_Trigger", true);
                    this.GetComponent<Status>().Attack += 1500.0f;
                    //回転攻撃
                    photonView.RPC("RollDamage", PhotonTargets.All);
                    SkillFlag1 = false;
                    Skill_Start = 0.0f;
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
                    this.GetComponent<Status>().Attack -= 1500.0f;
                    LimitFlag1 = false;
                    Skill1_Limit = 3.0f;
                }

            }

            //スキル2発動
            if (SkillFlag2 == true && SkillFlag1 == false)
            {
                //スキル2時間減少
                //SkillTime2 -= Time.deltaTime;
                Skill_Start += 1.0f / SkillTime2 * Time.deltaTime;

                //待機エフェクト発動
                if (EffectFlag == false)
                {
                    photonView.RPC("Knight_Effect", PhotonTargets.All, 3);
                    animator.SetBool("Skill2", true);
                }

                //スキル2時間が0になったら発動
                if (Skill_Start >= 1.0f)
                {
                    photonView.RPC("Knight_Effect", PhotonTargets.All, 4);
                    animator.SetBool("Skill2_Trigger", true);
                    SkillFlag2 = false;
                    Skill_Start = 0.0f;
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
        //アニメーター強制終了
        AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipinfo[0].clip.name == "rollwait")
        {
            EndTime += Time.deltaTime;
            if (EndTime >= 17.0f)
            {
                photonView.RPC("Knight_Effect", PhotonTargets.All, 5);
            }
        }
        else if (clipinfo[0].clip.name == "skillwait")
        {
            EndTime += Time.deltaTime;
            if (EndTime >= 7.0f)
            {
                photonView.RPC("Knight_Effect", PhotonTargets.All, 5);
            }
        }
        if (clipinfo[0].clip.name == "run")
        {
            EndTime += Time.deltaTime;
            if (EndTime >= 1.0f)
            {
                photonView.RPC("Knight_Effect", PhotonTargets.All, 5);
            }
        }
        //waitなら0秒に
        if (clipinfo[0].clip.name == "Wait")
        {
            animator.SetBool("AnimEnd", false);
            EndTime = 0.0f;
        }
    }

    //ナイトのエフェクト
    [PunRPC]
    public void Knight_Effect(int num)
    {
        if (num == 1)
        {
            animator.SetBool("Skill1_Trigger", false);
            animator.SetBool("Skill1", true);
            var instantiateEffect = GameObject.Instantiate(Skill1_Set, this.transform.position, Quaternion.identity) as GameObject;
            EffectFlag = true;
            this.GetComponent<Status>().ActionFlag = true;
        }
        //EndTime=0をwaitno時に
        else if (num == 2)
        {
            animator.SetBool("Skill1", false);
            animator.SetBool("Skill1_Trigger", true);
            var instantiateEffect = GameObject.Instantiate(Skill1, this.transform.position, Quaternion.identity) as GameObject;
            EffectFlag = false;
            this.GetComponent<Status>().ActionFlag = false;
            //EndTime = 0.0f;
        }
        else if (num == 3)
        {
            animator.SetBool("Skill2_Trigger", false);
            animator.SetBool("Skill2", true);
            var instantiateEffect = GameObject.Instantiate(Skill2_Set, this.transform.position, Quaternion.identity) as GameObject;
            EffectFlag = true;
            this.GetComponent<Status>().ActionFlag = true;
        }
        else if (num == 4)
        {
            animator.SetBool("Skill2", false);
            animator.SetBool("Skill2_Trigger", true);
            var instantiateEffect = GameObject.Instantiate(Skill2, this.transform.position, Quaternion.identity) as GameObject;
            EffectFlag = false;
            this.GetComponent<Status>().ActionFlag = false;
            //EndTime = 0.0f;
        }
        else if(num == 5)
        {
            animator.SetBool("AnimEnd", true);
            //EndTime = 0.0f;
        }
    }

    //スキル2(回転攻撃)
    [PunRPC]
    public void RollDamage()
    {
        //回転攻撃判定
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if (this.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        Guardian2();
        /*
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //6m以下なら体力攻撃判定
            if (dist < 6 && AttackFlag == false)
            {
                float random = Random.Range(0.9f, 1.1f);    //ランダム関数
                float damage;   //ダメージ量
                                //ダメージを与える
                damage = (this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 10));
                damage *= random;
                obj.GetComponent<Status>().HP -= (int)damage;
                //表示
                Debug.Log(this.name + "が" + obj + "に" + (int)damage + "ダメージ");
            }
            AttackFlag = false;
        }*/

    }

    //通常攻撃
    public void Damage()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if(this.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //対象キャラとの距離表示
            if (dist < 1.0 && obj.tag != this.tag)
            {
                Guardian();
                if (AttackFlag == false)
                {
                    Vector3 eyeDir = this.transform.forward; // プレイヤーの視線ベクトル。
                    Vector3 playerPos = this.transform.position; // プレイヤーの位置
                    Vector3 enemyPos = obj.transform.position; // 敵の位置

                    float angle = 30.0f;    //攻撃範囲内の角度

                    // プレイヤーと敵を結ぶ線と視線の角度差がangle以内なら当たり
                    if (Vector3.Angle((enemyPos - playerPos).normalized, eyeDir) <= angle)
                    {
                        float random = Random.Range(0.9f, 1.1f);    //ランダム関数
                        float damage;   //ダメージ量
                        //ダメージを与える
                        damage = (this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 5));
                        damage *= random;
                        obj.GetComponent<Status>().HP -= (int)damage;
                        //表示
                        Debug.Log(this.name + "が" + obj + "に" + (int)damage + "ダメージ");
                        AttackAudio.PlayOneShot(AttackAudio.clip);
                    }
                }
                AttackFlag = false;
            }
        }


    }


    //周りにガーディアンがいて、ガーディアンが身代わりをしていたらガーディアンに攻撃
    void Guardian()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if (this.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //対象キャラとの距離表示
            if(obj.GetComponent<Status>().Name == "Guardian" && dist < 2.0f)
            {
                if (obj.GetComponent<Guardian_Data>().GuardFlag == true)
                {
                    float random = Random.Range(0.9f, 1.1f);    //ランダム関数
                    float damage;   //ダメージ量
                    //ダメージを与える
                    damage = (this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 10));
                    damage *= random;
                    obj.GetComponent<Status>().HP -= (int)damage;
                    //表示
                    Debug.Log(this.name + "が" + obj + "に" + (int)damage + "ダメージ");
                    AttackAudio.PlayOneShot(AttackAudio.clip);
                    AttackFlag = true;
                }
            }
        }
    }

    //回転ダメージ用
    void Guardian2()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if (this.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //対象キャラとの距離表示
            //if (obj.GetComponent<Status>().Name == "Guardian" && dist < 6.0f)
            if(dist < 6.0f && obj.GetComponent<Status>().Name != "Guardian")
            {
                foreach(GameObject obj2 in targets)
                {
                    float dist2 = Vector3.Distance(obj2.transform.position, obj.transform.position);
                    //Debug.Log(obj + "と" + obj2 + "の距離は" + dist2);
                    if(obj2.GetComponent<Status>().Name == "Guardian" && dist2 < 2.0f)
                    {
                        if(obj2.GetComponent<Guardian_Data>().GuardFlag == true)
                        {
                            float random = Random.Range(0.9f, 1.1f);    //ランダム関数
                            float damage;   //ダメージ量
                                            //ダメージを与える
                            damage = (this.GetComponent<Status>().Attack / ((1 + obj2.GetComponent<Status>().Defense) / 10));
                            damage *= random;
                            obj2.GetComponent<Status>().HP -= (int)damage;
                            //表示
                            //Debug.Log(obj + "を肩代わりした。" + this.name + "が" + obj2 + "に" + (int)damage + "ダメージ");
                            AttackAudio.PlayOneShot(AttackAudio.clip);
                            AttackFlag = true;
                        }
                    }
                }
            }
            if(AttackFlag == false)
            {
                float random = Random.Range(0.9f, 1.1f);    //ランダム関数
                float damage;   //ダメージ量
                                //ダメージを与える
                damage = (this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 10));
                damage *= random;
                obj.GetComponent<Status>().HP -= (int)damage;
                //表示
                Debug.Log(this.name + "が" + obj + "に" + (int)damage + "ダメージ");
            }
            AttackFlag = false;
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
