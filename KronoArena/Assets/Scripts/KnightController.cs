using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {

		animator = GetComponent <Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.UpArrow)) {
			animator.SetFloat ("upKey", 1);
		} else {
			animator.SetFloat ("upKey", 0);
		}

		if (Input.GetKey (KeyCode.J)) {
			animator.SetFloat ("attackKey", 1);
		} else {
			animator.SetFloat ("attackKey", 0);
		}

		if (Input.GetKey (KeyCode.K)) {
			animator.SetFloat ("rollKey", 1);
		} else {
			animator.SetFloat ("rollKey", 0);
		}

		if (Input.GetKey (KeyCode.L)) {
			animator.SetFloat ("skillKey", 1);
		} else {
			animator.SetFloat ("skillKey", 0);
		}

	}
}
