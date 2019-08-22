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
    [SerializeField] public float Heel;
    [SerializeField] public Sprite AttackIcon;
    [SerializeField] public Sprite SkillIcon1;
    [SerializeField] public Sprite SkillIcon2;

    public Slider hpSlider;

    public float MaxHP;

    public bool DiedFlag;

    public bool ActionFlag;

    PhotonView photonView;
    PhotonView this_photonView;

    // Start is called before the first frame update
    void Start()
    {
        MaxHP = HP;
        DiedFlag = false;
        StutusPut();


        photonView = GetComponent<PhotonView>();
        this_photonView = PhotonView.Get(this);

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
            if (this_photonView.isMine)
            {
                photonView.RPC("CharaDied", PhotonTargets.All);
            }
            //GameObject.Find("GameManager").GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
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
            stream.SendNext(this.Attack);
            stream.SendNext(this.Magic_Attack);
            stream.SendNext(this.Defense);
            stream.SendNext(this.Magic_Defense);
            stream.SendNext(this.Speed);
            stream.SendNext(this.Heel);
        }
        else
        {
            //データの受信
            this.HP = (float)stream.ReceiveNext();
            this.Attack = (float)stream.ReceiveNext();
            this.Magic_Attack = (float)stream.ReceiveNext();
            this.Defense = (float)stream.ReceiveNext();
            this.Magic_Attack = (float)stream.ReceiveNext();
            this.Speed = (float)stream.ReceiveNext();
            this.Heel = (float)stream.ReceiveNext();
        }
    }


    [PunRPC]
    void CharaDied()
    {

        if (this.tag == "Player1")
        {
            GameManager.P1_GP += 1;
            Debug.Log(this.name + "がやられた。(" + "倒した数" + GameManager.P1_GP + "体)");
        }
        if (this.tag == "Player2")
        {
            GameManager.P2_GP += 1;
            Debug.Log(this.name + "がやられた。(" + "倒した数" + GameManager.P2_GP + "体)");
        }

    }

}
