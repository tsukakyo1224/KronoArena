 
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian_Effect_Animation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ガーディアン
        if (stateInfo.IsName("attack01") || stateInfo.IsName("attack02") || stateInfo.IsName("attack03"))
        {

            GameObject Gu;
            if (TurnCol.P1_Turn == true)
            {
                Gu = GameObject.Find("P1_Chara3");
                Gu.GetComponent<Guardian_Data>().Damage();
                //Debug.Log(Kn);
            }

            else if (TurnCol.P2_Turn == true)
            {
                Gu = GameObject.Find("P2_Chara3");
                Gu.GetComponent<Guardian_Data>().Damage();
                //Debug.Log(Kn);
            }

        }
        /*
        if (stateInfo.IsName("skill01_1"))
        {
            GameObject Gu;
            if (TurnCol.P1_Turn == true)
            {
                Gu = GameObject.Find("P1_Chara3");
                Gu.GetComponent<Guardian_Data>().BuffSet1();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Gu = GameObject.Find("P2_Chara3");
                Gu.GetComponent<Guardian_Data>().BuffSet1();
            }

        }

        if (stateInfo.IsName("skill01_2"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara3");
                Me.GetComponent<Guardian_Data>().Buff1();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara3");
                Me.GetComponent<Guardian_Data>().Buff1();
            }
        }
        if (stateInfo.IsName("skill02_1"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara3");
                Me.GetComponent<Guardian_Data>().BigShieldSet();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara3");
                Me.GetComponent<Guardian_Data>().BigShieldSet();
            }
        }

        if (stateInfo.IsName("skill02_2"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara3");
                Me.GetComponent<Guardian_Data>().BigShield();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara3");
                Me.GetComponent<Guardian_Data>().BigShield();
            }
        }*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    /*override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("skill01_1"))
        {
            GameObject MeHeel = GameObject.Find("Guardian_BuffSet1(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill01_2"))
        {
            GameObject MeHeel = GameObject.Find("Guardian_Buff1(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill02_1"))
        {
            GameObject MeHeel = GameObject.Find("Guardian_BigShieldSet(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill02_2"))
        {
            GameObject MeHeel = GameObject.Find("Guardian_BigShield(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
    }*/

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
