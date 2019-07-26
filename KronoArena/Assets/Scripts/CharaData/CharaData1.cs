using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaData1 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        CharaName = "Chara1";
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

    /*void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Enemy")
        {
            Debug.Log(hit.gameObject.name);
        }
    }*/

    void OnTriggerExit(Collider other)
    {
        //if(other.tag == "Enemy")
        //{
        //Debug.Log("sasa");
        //other.gameObject.GetComponent<Slider>().value -= 5.0f;
        //other.GetComponent;

        //}
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log(other.name);
        }
    }*/
}
