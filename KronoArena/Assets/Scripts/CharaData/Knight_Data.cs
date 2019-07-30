using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    CharaData2 CharaData2_hp;

    //HP
    public static int MaxHP = 200;
    public static int HP = 200;
    //　HP表示用スライダー
    public static Slider hpSlider;
    //攻撃までの時間
    public static float AttackTime;
    public static float SpecialTime1;
    public static float SpecialTime2;

    //攻撃までの時間テキスト
    public static GameObject ATText1;
    public static GameObject ATText2;
    public static GameObject ATText3;

    //攻撃したかのフラグ
    public static bool AttackFlag;

    public static Collider Sword;

    public static Slider YourHP;

    // Start is called before the first frame update
    void Start()
    {
        //キャラクター情報入力
        CharaName = "ナイト";
        CharaIconImage = Resources.Load<Sprite>("CharaIcon/CharaIcon1");
        MiniIcon = Resources.Load<Sprite>("MiniCharaIcon/MiniIcon1");
        JobIconImage = Resources.Load<Sprite>("JobIcon/knite");
        hpSlider = GameObject.Find("BackGround").transform.Find("Player1HP").GetComponent<Slider>();
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;
        AttackTime = 3.0f;
        SpecialTime1 = 5.0f;
        SpecialTime2 = 10.0f;
        AttackFlag = false;
        Sword = GameObject.Find("Sword_Collider").GetComponent<BoxCollider>();
        //剣コライダーをオンにする
        Sword.enabled = false;

        if(PhotonNetwork.player.ID == 1)
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
        //hpSlider.value -= 1;
    }

    void OnTriggerExit(Collider other)
    {
        if (PhotonNetwork.player.ID == 1)
        {
            if (other.tag == "Player1")
            {
                if (other.name == "P1_Chara2")
                {
                    Debug.Log(other + "に5ダメージ");
                    other.GetComponent<CharaData2>().hpSlider.value -= 5.0f;

                }
                else if (other.name == "P1_Chara3")
                {
                    CharaData3.hpSlider.value -= 5.0f;
                    Debug.Log(other + "に5ダメージ");
                }
            }
        }
    }
    public static void ColliderReset()
    {
        Sword.enabled = false;
    }
}
