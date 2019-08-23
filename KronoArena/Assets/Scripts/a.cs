using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : StateMachineBehaviour {


    //private PhotonView photonView;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		//メディック
		if (stateInfo.IsName ("attack01") || stateInfo.IsName ("attack02") || stateInfo.IsName ("attack03")) {

            GameObject Me;

            if (PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == true)
            {
                Debug.Log("プレイヤー1から");
                Me = GameObject.Find("P1_Chara2");
                Me.GetComponent<Medic_Data>().effect();
            }

            else if (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == true)
            {
                Debug.Log("プレイヤー2から");
                Me = GameObject.Find("P2_Chara2");
                Me.GetComponent<Medic_Data>().effect();
            }
            //photonView = PhotonView.Get(GameObject.Find("P1_Chara2"));

            //GameObject Me = GameObject.Find("P1_Chara2");

            //if (!photonView.isMine)

			//Medic_Data.effect();

		}
	
	}




	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    

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
