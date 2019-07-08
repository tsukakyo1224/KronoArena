using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaData1 : MonoBehaviour
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
    public static GameObject AT1Text1;
    public static GameObject AT1Text2;
    public static GameObject AT1Text3;

    //攻撃したかのフラグ
    public static bool AttackFlag;

    // Start is called before the first frame update
    void Start()
    {
        CharaName = "UnityChan";
        hpSlider = GameObject.Find("BackGround").transform.Find("Player1HP").GetComponent<Slider>();
        hpSlider.maxValue = HP;
        hpSlider.value = HP;
        AttackTime = 3.0f;
        SpecialTime1 = 5.0f;
        SpecialTime2 = 10.0f;
        AttackFlag = false;

        /*
        AT1Text1 = GameObject.Find("ATime1");
        AT1Text2 = GameObject.Find("ATime2");
        AT1Text3 = GameObject.Find("ATime3");

        AT1Text1.SetActive(false);
        AT1Text2.SetActive(false);
        AT1Text3.SetActive(false);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //hpSlider.value -= 1;
    }
}
