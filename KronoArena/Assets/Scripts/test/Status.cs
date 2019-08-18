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

    public bool DiedFlag;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HP;
        DiedFlag = false;
        StutusPut();

    }

    // Update is called once per frame
    void Update()
    {
        if((PhotonNetwork.player.ID == 1 && this.tag=="Player1") ||
            (PhotonNetwork.player.ID == 2 && this.tag == "Player2")){
            this.hpSlider.value = this.HP;
        }
        if (HP < 0 && DiedFlag == false)
        {
            if(PhotonNetwork.player.ID == 1)
            {
                GameManager.P1_GP += 1;
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                GameManager.P2_GP += 1;
            }
            this.gameObject.SetActive(false);
            DiedFlag = true;
        }
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


    //HP送受信
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //データの送信
            stream.SendNext(this.HP);
        }
        else
        {
            //データの受信
            this.HP = (float)stream.ReceiveNext();
        }
    }


}
