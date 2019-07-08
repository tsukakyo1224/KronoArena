using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaData2 : MonoBehaviour
{
    //キャラクター名
    public static string CharaName;
    //HP
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

    // Start is called before the first frame update
    void Start()
    {
        CharaName = "UnityChan";
        hpSlider = GameObject.Find("BackGround").transform.Find("Player2HP").GetComponent<Slider>();
        hpSlider.maxValue = HP;
        hpSlider.value = HP;
        AttackTime = 3.0f;
        SpecialTime1 = 5.0f;
        SpecialTime2 = 10.0f;
        AttackFlag = false;

        /*
        ATText1 = GameObject.Find("ATime1");
        ATText2 = GameObject.Find("ATime2");
        ATText3 = GameObject.Find("ATime3");

        ATText1.SetActive(false);
        ATText2.SetActive(false);
        ATText3.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        //hpSlider.value -= 5;
    }
}
