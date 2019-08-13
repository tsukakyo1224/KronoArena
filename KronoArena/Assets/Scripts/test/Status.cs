using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{

    [SerializeField] public float HP;
    [SerializeField] public float Attack;
    [SerializeField] public float Magic_Attack;
    [SerializeField] public float Defense;
    [SerializeField] public float Magic_Defense;
    [SerializeField] public float Speed;
    [SerializeField] public Sprite AttackIcon;
    [SerializeField] public Sprite SkillIcon1;
    [SerializeField] public Sprite SkillIcon2;

    public Slider hpSlider;

    public float MaxHP;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HP;
        StutusPut();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void StutusPut()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            if (this.name == "P1_Chara1")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player1HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }
            else if (this.name == "P1_Chara2")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player2HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }
            else if(this.name == "P1_Chara3")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player3HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }

        }
        else if(PhotonNetwork.player.ID == 2)
        {
            if (this.name == "P2_Chara1")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player1HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }
            else if (this.name == "P2_Chara2")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player2HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }
            else if (this.name == "P2_Chara3")
            {
                hpSlider = GameObject.Find("BackGround").transform.Find("Player3HP").GetComponent<Slider>();
                hpSlider.maxValue = HP;
                hpSlider.value = HP;
            }
        }
    }


    //名前とtagの送受信
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(this.hpSlider.value);
        }
        else
        {
            //データの受信
            this.hpSlider.value = (int)stream.ReceiveNext();
        }
    }


}
