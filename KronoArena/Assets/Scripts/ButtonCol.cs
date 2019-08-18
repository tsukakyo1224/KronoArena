using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCol : MonoBehaviour
{

    private Animator animator1;
    private Animator animator2;
    private Animator animator3;
    private GameObject Player1;
    private GameObject Player2;
    private GameObject Player3;

    //選択した攻撃までの時間
    public static float Time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.cameraflag == true)
        {
            if(PhotonNetwork.player.ID == 1)
            {
                Player1 = GameObject.Find("P1_Chara1");
                animator1 = Player1.GetComponent<Animator>();
                Player2 = GameObject.Find("P1_Chara2");
                animator2 = Player2.GetComponent<Animator>();
                Player3 = GameObject.Find("P1_Chara3");
                animator3 = Player3.GetComponent<Animator>();
            }
            else
            {
                Player1 = GameObject.Find("P2_Chara1");
                animator1 = Player1.GetComponent<Animator>();
                Player2 = GameObject.Find("P2_Chara2");
                animator2 = Player2.GetComponent<Animator>();
                Player3 = GameObject.Find("P2_Chara3");
                animator3 = Player3.GetComponent<Animator>();
            }
        }
    }

    public void TurnChange()
    {
        GameObject.Find("TurnCol").GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        TurnCol.ChangeTurn();
        Debug.Log("button");
    }

    //攻撃ボタン1
    public void Attack1()
    {
        if (ChangeChara.nowChara == 0)
        {
            animator1.SetBool("Attack", true);
            Knight_Data.AttackFlag = true;
            //剣コライダーをオンに
            if (PhotonNetwork.player.ID == 1)
            {
                //Player1.GetComponent<Knight_Data>(). = true;
            }
            //Knight_Data.Sword.enabled = true;

        }
        else if (ChangeChara.nowChara == 1)
        {
            animator2.SetBool("Attack", true);
        }
        else if (ChangeChara.nowChara == 2)
        {
            animator3.SetBool("Attack", true);
        }
        Debug.Log(ChangeChara.nowChara + " Attack");
        //Knight_Data.AttackFlag = false;
    }

    //スキル1
    public void Special1()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            if (Knight_Data.SkillFlag2 == false)
            {
                Knight_Data.SkillFlag1 = true;
                animator1.SetBool("Skill1", true);
            }
        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Medic_Data.SkillFlag2 == false)
            {
                Medic_Data.SkillFlag1 = true;
                animator2.SetBool("Skill1", true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Guardian_Data.SkillFlag2 == false)
            {
                Guardian_Data.SkillFlag1 = true;
                animator3.SetBool("Skill1", true);
            }
        }
        Debug.Log(ChangeChara.nowChara + " Special1");
    }

    //スキル2
    public void Special2()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            if (Knight_Data.SkillFlag1 == false)
            {
                Knight_Data.SkillFlag2 = true;
                animator1.SetBool("Skill2", true);
            }
        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Medic_Data.SkillFlag1 == false)
            {
                Medic_Data.SkillFlag2 = true;
                animator2.SetBool("Skill2", true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Guardian_Data.SkillFlag1 == false)
            {
                Guardian_Data.SkillFlag2 = true;
                animator3.SetBool("Skill2", true);
            }
        }
        Debug.Log(ChangeChara.nowChara + " Special2");
    }


}
