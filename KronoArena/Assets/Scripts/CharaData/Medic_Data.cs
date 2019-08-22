using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medic_Data : MonoBehaviour
{
    //キャラクター名
    public static string CharaName;
    //キャラクタジョブ番号
    public static int job;
    //キャラクターアイコン
    public static Sprite CharaIconImage;
    //ジョブアイコン
    public static Sprite JobIconImage;

    //攻撃までの時間
    public static float SkillTime1;
    public static float SkillTime2;

    //攻撃までの時間テキスト
    public static GameObject ATText1;
    public static GameObject ATText2;
    public static GameObject ATText3;

    //攻撃したかのフラグ
    public static bool SkillFlag1;
    public static bool SkillFlag2;

    //持続時間
    public static float Skill2_Limit;

    //持続時間フラグ
    public static bool LimitFlag2;

    //アニメーター 
    private Animator animator;

    //Photonの
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        //if(this.name==)
        CharaName = "メディック";
        JobIconImage = Resources.Load<Sprite>("JobIcon/Medic");
        SkillTime1 = 20.0f;
        SkillTime2 = 10.0f;
        Skill2_Limit = 60.0f;
        SkillFlag1 = false;
        SkillFlag2 = false;
        LimitFlag2 = false;

        animator = this.GetComponent<Animator>();

        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            //スキル1発動
            if (SkillFlag1 == true && SkillFlag2 == false)
            {
                //スキル1時間減少
                SkillTime1 -= Time.deltaTime;
                //スキル1時間が0になったら発動
                if (SkillTime1 <= 0)
                {
                    animator.SetBool("Skill1_Trigger", true);
                    SkillFlag1 = false;
                    SkillTime1 = 20.0f;

                    //回復処理
                    GameObject[] targets = GameObject.FindGameObjectsWithTag("Player1");
                    if (PhotonNetwork.player.ID == 2)
                    {
                        targets = GameObject.FindGameObjectsWithTag("Player2");
                    }
                    foreach (GameObject obj in targets)
                    {
                        // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
                        float dist = Vector3.Distance(obj.transform.position, transform.position);
                        //対象キャラとの距離表示
                        Debug.Log(obj.name + "との距離は" + dist + "m");
                        //3m以下なら体力回復判定
                        if (dist < 3)
                        {
                            obj.GetComponent<Status>().HP += this.GetComponent<Status>().Heel;
                            if (obj.GetComponent<Status>().HP > obj.GetComponent<Status>().MaxHP)
                            {
                                obj.GetComponent<Status>().HP = obj.GetComponent<Status>().MaxHP;
                            }
                            Debug.Log(obj.name + "を" + this.GetComponent<Status>().Heel + "回復");
                        }
                    }


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
                    this.GetComponent<Status>().Defense += 100.0f;
                    this.GetComponent<Status>().Magic_Defense += 100.0f;
                    this.GetComponent<Status>().Heel += 100.0f;
                    animator.SetBool("Skill2_Trigger", true);
                    SkillFlag2 = false;
                    SkillTime2 = 10.0f;
                    LimitFlag2 = true;
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
                    this.GetComponent<Status>().Heel -= 100.0f;
                    LimitFlag2 = false;
                    Skill2_Limit = 60.0f;
                    Debug.Log("メディックのステータスが元に戻った");
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
                    (int)(this.GetComponent<Status>().Magic_Attack / ((1 + other.GetComponent<Status>().Magic_Defense) / 10));
            Debug.Log(other + "に" + (int)(this.GetComponent<Status>().Magic_Attack / 
                ((1 + other.GetComponent<Status>().Magic_Defense) / 10)) + "ダメージ");

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
