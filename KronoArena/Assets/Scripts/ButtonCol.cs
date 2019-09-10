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

    private GameObject Chara1Area;
    private GameObject Chara2Area;
    private GameObject Chara3Area;

    //選択した攻撃までの時間
    public static float Time;

    public static bool CharaTrigger;

    // Start is called before the first frame update
    void Start()
    {
        CharaTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.CameraFlag == true)
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
            Chara1Area = Player1.transform.GetChild(4).gameObject;
            Chara2Area = Player2.transform.GetChild(4).gameObject;
            Chara3Area = Player3.transform.GetChild(4).gameObject;
            Chara1Area.SetActive(false);
            Chara2Area.SetActive(false);
            Chara3Area.SetActive(false);
        }
    }

    //ターンチェンジ(自キャラにランダムでバフ)
    public void TurnChange()
    {
        GameObject.Find("TurnCol").GetComponent<TurnCol>().ChangeTurn();
        Debug.Log("button");
    }

    //攻撃ボタン1
    public void Attack1()
    {
        if (ChangeChara.nowChara == 0)
        {
            if (Knight_Data.SkillFlag1 == false && Knight_Data.SkillFlag2 == false)
            {
                animator1.SetBool("Attack", true);
            }
        }
        else if (ChangeChara.nowChara == 1)
        {
            if (Medic_Data.SkillFlag1 == false && Medic_Data.SkillFlag2 == false)
            {
                animator2.SetBool("Attack", true);
            }
        }
        else if (ChangeChara.nowChara == 2)
        {
            if (Guardian_Data.SkillFlag1 == false && Guardian_Data.SkillFlag2 == false)
            {
                animator3.SetBool("Attack", true);
            }
        }
    }

    //スキル1
    public void Special1()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            if (Knight_Data.SkillFlag1 == false && Knight_Data.SkillFlag2 == false)
            {
                Knight_Data.SkillFlag1 = true;
                //animator1.SetBool("Skill1", true);
            }
        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Medic_Data.SkillFlag1 == false && Medic_Data.SkillFlag2 == false)
            {
                Medic_Data.SkillFlag1 = true;
                //animator2.SetBool("Skill1", true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Guardian_Data.SkillFlag1 == false && Guardian_Data.SkillFlag2 == false)
            {
                Guardian_Data.SkillFlag1 = true;
                //animator3.SetBool("Skill1", true);
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
            if (Knight_Data.SkillFlag1 == false && Knight_Data.SkillFlag2 == false)
            {
                Knight_Data.SkillFlag2 = true;
                animator1.SetBool("Skill2", true);
            }
        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Medic_Data.SkillFlag1 == false && Medic_Data.SkillFlag2 == false)
            {
                Medic_Data.SkillFlag2 = true;
                animator2.SetBool("Skill2", true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Guardian_Data.SkillFlag1 == false && Guardian_Data.SkillFlag2 == false)
            {
                Guardian_Data.SkillFlag2 = true;
                animator3.SetBool("Skill2", true);
            }
        }
        Debug.Log(ChangeChara.nowChara + " Special2");
    }

    //スキル1範囲表示
    public void Skill1AreaOn()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            if (Player1.GetComponent<Status>().SkillArea1 == true)
            {
                Chara1Area.SetActive(true);
            }

        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Player2.GetComponent<Status>().SkillArea1 == true)
            {
                Chara2Area.SetActive(true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Player3.GetComponent<Status>().SkillArea1 == true)
            {
                Chara3Area.SetActive(true);
            }
        }
    }

    //スキル2範囲表示
    public void Skill2AreaOn()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            if (Player1.GetComponent<Status>().SkillArea2 == true)
            {
                Chara1Area.SetActive(true);
            }

        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            if (Player2.GetComponent<Status>().SkillArea2 == true)
            {
                Chara2Area.SetActive(true);
            }
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            if (Player3.GetComponent<Status>().SkillArea2 == true)
            {
                Chara3Area.SetActive(true);
            }
        }
    }

    public void AreaOff()
    {
        //ナイトの場合
        if (ChangeChara.nowChara == 0)
        {
            Chara1Area.SetActive(false);
        }
        //メディックの場合
        else if (ChangeChara.nowChara == 1)
        {
            Chara2Area.SetActive(false);
        }
        //ガーディアンの場合
        else if (ChangeChara.nowChara == 2)
        {
            Chara3Area.SetActive(false);
        }
    }

    public void CharaColOn()
    {
        CharaTrigger = true;
    }

    public void CharaColOff()
    {
        CharaTrigger = false;
    }
}
