using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowingCamera.cameraflag == true)
        {
            Player1 = GameObject.Find("Player1(Clone)");
            animator1 = Player1.GetComponent<Animator>();
            Player2 = GameObject.Find("Player2(Clone)");
            animator2 = Player2.GetComponent<Animator>();
            Player3 = GameObject.Find("Player3(Clone)");
            animator3 = Player3.GetComponent<Animator>();
        }
    }

    public void TurnChange()
    {
        GameObject.Find("TurnCol").GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        TurnCol.ChangeTurn();
        Debug.Log("button");
        //TimerScript.TotalTime = 5.0f;
    }

    //攻撃ボタン1
    public void Attack1()
    {
        if (ChangeChara.nowChara == 0)
        {
            animator1.SetBool("Jab", true);
            GameManager.CharaAttackTime1.SetActive(true);
        }
        else if (ChangeChara.nowChara == 1)
        {
            animator2.SetBool("Jab", true);
            GameManager.CharaAttackTime2.SetActive(true);
        }
        else if (ChangeChara.nowChara == 2)
        {
            animator3.SetBool("Jab", true);
            GameManager.CharaAttackTime3.SetActive(true);
        }
        Debug.Log(ChangeChara.nowChara + " Attack");
    }

    //スペシャル攻撃1
    public void Special1()
    {
        if (ChangeChara.nowChara == 0)
        {
           animator1.SetBool("Hikick", true);
           //CharaData1.AttackFlag = true;
        }
        else if (ChangeChara.nowChara == 1)
        {
            animator2.SetBool("Hikick", true);
        }
        else if (ChangeChara.nowChara == 2)
        {
            animator3.SetBool("Hikick", true);
        }
        Debug.Log(ChangeChara.nowChara + " Special1");
    }
    //スペシャル攻撃2
    public void Special2()
    {
        if (ChangeChara.nowChara == 0)
        {
            animator1.SetBool("Spinkick", true);
            //CharaData1.AttackFlag = true;
        }
        else if (ChangeChara.nowChara == 1)
        {
            animator2.SetBool("Spinkick", true);
        }
        else if (ChangeChara.nowChara == 2)
        {
            animator3.SetBool("Spinkick", true);
        }
        Debug.Log(ChangeChara.nowChara + " Special2");
    }
}
