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
    public static bool AttackFlag;

    //持続時間
    public static float Skill2_Limit;

    //持続時間フラグ
    public static bool LimitFlag2;

    //エフェクト用フラグ
    public static bool EffectFlag;

    //アニメーター 
    private Animator animator;

    //Photonの
    private PhotonView photonView;

    //メディックのエフェクト
    [SerializeField] private static GameObject explosion;
    [SerializeField] private static GameObject HeelArea;
    [SerializeField] private static GameObject HeelShower;
    [SerializeField] private static GameObject Medic_Buff;
    [SerializeField] private static GameObject Medic_BuffSet;

    //位置情報
    public static Vector3 position;
    public static Vector3 forword;
    public static Quaternion quat;

    // Start is called before the first frame update
    void Start()
    {
        CharaName = "メディック";
        JobIconImage = Resources.Load<Sprite>("JobIcon/Medic");
        SkillTime1 = 20.0f;
        SkillTime2 = 10.0f;
        Skill2_Limit = 60.0f;
        SkillFlag1 = false;
        SkillFlag2 = false;
        LimitFlag2 = false;

        EffectFlag = false;

        AttackFlag = false;

        //エフェクト呼び出し
        explosion = Resources.Load<GameObject>("HolyBall");
        HeelArea = Resources.Load<GameObject>("Heel");
        HeelShower = Resources.Load<GameObject>("HeelShower");
        Medic_Buff = Resources.Load<GameObject>("Medic_Buff");
        Medic_BuffSet = Resources.Load<GameObject>("Medic_BuffSet");

        animator = this.GetComponent<Animator>();

        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        position = this.transform.position;
        forword = this.transform.forward;
        quat = this.transform.rotation;

        //if (photonView.isMine)
        if(PhotonNetwork.player.ID == 1 && this.tag == "Player1" ||
            PhotonNetwork.player.ID == 2 && this.tag == "Player2")
        {
            //スキル1発動
            if (SkillFlag1 == true && SkillFlag2 == false)
            {
                //スキル1時間減少
                SkillTime1 -= Time.deltaTime;

                //待機エフェクト発動
                if (EffectFlag == false)
                {
                    photonView.RPC("EffectCol", PhotonTargets.All, 1);
                }

                //スキル1時間が0になったら発動
                if (SkillTime1 <= 0)
                {
                    //エフェクト発動
                    photonView.RPC("HeelShowerEffect", PhotonTargets.All);
                    animator.SetBool("Skill1_Trigger", true);
                    SkillFlag1 = false;
                    SkillTime1 = 20.0f;

                    //回復処理
                    GameObject[] targets = GameObject.FindGameObjectsWithTag("Player1");
                    if (this.tag == "Player2")
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
                    //Medic_BuffEffect();
                    //エフェクト発動
                    photonView.RPC("Medic_BuffEffect", PhotonTargets.All);
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
                    //エフェクト発動

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

    //攻撃用エフェクト
    public void effect()
    {
        var instantiateEffect = GameObject.Instantiate(explosion, this.transform.position + 
            this.transform.forward + new Vector3(0.0f, 0.5f, 0.0f), this.transform.rotation) as GameObject;
        if(this.tag == "Player1")
        {
            instantiateEffect.tag = "Player1";
        }
        else if(this.tag == "Player2")
        {
            instantiateEffect.tag = "Player2";
        }
        instantiateEffect.GetComponent<Status>().Magic_Attack = this.GetComponent<Status>().Magic_Attack;

    }

    [PunRPC]
    public void EffectCol(int num)
    {
        if (num == 1)
        {
            animator.SetBool("Skill1", true);
            var instantiateEffect = GameObject.Instantiate(HeelArea, this.transform.position, Quaternion.identity) as GameObject;
            if((PhotonNetwork.player.ID == 1 && this.tag == "Player1") ||
                PhotonNetwork.player.ID == 2 && this.tag == "Player2")
            EffectFlag = true;
        }
    }


    public void HeelAreaEffect()
    {
        var instantiateEffect = GameObject.Instantiate(HeelArea, this.transform.position, Quaternion.identity) as GameObject;
    }

    [PunRPC]
    public void HeelShowerEffect()
    {
        var instantiateEffect = GameObject.Instantiate(HeelShower, this.transform.position, Quaternion.identity) as GameObject;
        EffectFlag = false;
    }
    [PunRPC]
    public void Medic_BuffEffect()
    {
        var instantiateEffect = GameObject.Instantiate(Medic_Buff, this.transform.position, Quaternion.identity) as GameObject;
    }

    public void Medic_BuffSetEffect()
    {
        var instantiateEffect = GameObject.Instantiate(Medic_BuffSet, this.transform.position, Quaternion.identity) as GameObject;
    }

    //周りにガーディアンがいて、ガーディアンが身代わりをしていたらガーディアンに攻撃
    void Guardian()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
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
