using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaData2 : MonoBehaviour
{
    //キャラクター名
    public static string CharaName;
    //キャラクタジョブ番号
    public static int job;
    //キャラクターアイコン
    public static Sprite CharaIconImage;
    //ジョブアイコン
    public static Sprite JobIconImage;

    //HP
    public static int MaxHP = 500;
    public static int HP = 500;
    //　HP表示用スライダー
    public Slider hpSlider;
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

    // Start is called before the first frame update
    void Start()
    {
        //if(this.name==)
        CharaName = "Chara2";
        JobIconImage = Resources.Load<Sprite>("JobIcon/guardian");
        hpSlider = GameObject.Find("BackGround").transform.Find("Player2HP").GetComponent<Slider>();
        hpSlider.maxValue = MaxHP;
        hpSlider.value = MaxHP;
        AttackTime = 3.0f;
        SpecialTime1 = 5.0f;
        SpecialTime2 = 10.0f;
        AttackFlag = false;

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
        //hpSlider.value -= 5;
    }
}
