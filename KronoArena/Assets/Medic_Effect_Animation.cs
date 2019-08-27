using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic_Effect_Animation : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //メディック
        if (stateInfo.IsName("attack01") || stateInfo.IsName("attack02") || stateInfo.IsName("attack03"))
        {

            GameObject Me;

            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().effect();
            }
            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().effect();
            }

        }

        if (stateInfo.IsName("heel01"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().HeelAreaEffect();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().HeelAreaEffect();
            }

        }
        /*
        if (stateInfo.IsName("heel02"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().HeelShowerEffect();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().HeelShowerEffect();
            }
        }*/
        if (stateInfo.IsName("skill01"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().Medic_BuffSetEffect();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().Medic_BuffSetEffect();
            }
        }
        /*
        if (stateInfo.IsName("skill02"))
        {
            GameObject Me;
            if (TurnCol.P1_Turn == true)
            {
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().Medic_BuffEffect();
            }

            else if (TurnCol.P2_Turn == true)
            {
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().Medic_BuffEffect();
            }
        }*/

    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (stateInfo.IsName("heel01"))
        {
            GameObject MeHeel = GameObject.Find("Heel(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }

        if (stateInfo.IsName("heel02"))
        {
            GameObject MeHeel = GameObject.Find("HeelShower(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill01"))
        {
            GameObject MeHeel = GameObject.Find("Medic_BuffSet(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }
        if (stateInfo.IsName("skill02"))
        {
            GameObject MeHeel = GameObject.Find("Medic_Buff(Clone)");
            //Debug.Log (MeHeel);
            MeHeel.GetComponent<DestoryEffect>().Delete();
        }*/
    }
    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
