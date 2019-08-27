using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_Effect_Animation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ナイト
        if (stateInfo.IsName("attack01") || stateInfo.IsName("attack02") || stateInfo.IsName("attack03"))
        {
            if(PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true)
            {
                GameObject Kn = GameObject.Find("P1_Chara1");
                Kn.GetComponent<Knight_Data>().Damage();
            }
            else if(PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true)
            {
                GameObject Kn = GameObject.Find("P2_Chara1");
                Kn.GetComponent<Knight_Data>().Damage();
            }



        }

        if (stateInfo.IsName("rollwait"))
        {
            GameObject Kn;
            if (TurnCol.P1_Turn == true)
            {
                Kn = GameObject.Find("P1_Chara1");
                Kn.GetComponent<Knight_Data>().RollSet();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Kn = GameObject.Find("P2_Chara1");
                Kn.GetComponent<Knight_Data>().RollSet();
            }

        }
        if (stateInfo.IsName("rollattack"))
        {
            GameObject Kn;
            if (TurnCol.P1_Turn == true)
            {
                Kn = GameObject.Find("P1_Chara1");
                Kn.GetComponent<Knight_Data>().Roll();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Kn = GameObject.Find("P2_Chara1");
                Kn.GetComponent<Knight_Data>().Roll();
            }
        }
        if (stateInfo.IsName("skillwait"))
        {
            GameObject Kn;
            if (TurnCol.P1_Turn == true)
            {
                Kn = GameObject.Find("P1_Chara1");
                Kn.GetComponent<Knight_Data>().BuffSet();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Kn = GameObject.Find("P2_Chara1");
                Kn.GetComponent<Knight_Data>().BuffSet();
            }
        }
        if (stateInfo.IsName("skill"))
        {
            GameObject Kn;
            if (TurnCol.P1_Turn == true)
            {
                Kn = GameObject.Find("P1_Chara1");
                Kn.GetComponent<Knight_Data>().Buff();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Kn = GameObject.Find("P2_Chara1");
                Kn.GetComponent<Knight_Data>().Buff();
            }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("rollwait"))
        {
            GameObject MeHeel = GameObject.Find("Knight_RollSet(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("rollattack"))
        {
            GameObject MeHeel = GameObject.Find("Knight_Roll(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete2();
        }
        if (stateInfo.IsName("skillwait"))
        {
            GameObject MeHeel = GameObject.Find("Knight_BuffSet(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill"))
        {
            GameObject MeHeel = GameObject.Find("Knight_Buff(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
