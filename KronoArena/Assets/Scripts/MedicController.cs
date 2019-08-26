using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicController : MonoBehaviour {

	private Animator animator;
	//　パーティクルシステム
	//public ParticleSystem ps;

	[SerializeField] private static GameObject explosion;

	public static Vector3 position;

	// Use this for initialization
	void Start () {

		animator = GetComponent <Animator> ();
		explosion = Resources.Load<GameObject> ("HolyBall");
		
	}
	
	// Update is called once per frame
	void Update () {
		position = this.transform.position;
		
	}

	public void Attack1(){

		animator.SetBool("attack", true);
		//var instantiateEffect = GameObject.Instantiate(explosion, transform.position + new Vector3(0f, 1f, 2f), Quaternion.identity) as GameObject;

	}

	public static void effect() {
		var instantiateEffect = GameObject.Instantiate(explosion,  position + new Vector3(0f, 0.5f, 0.6f), Quaternion.identity) as GameObject;
		//Debug.Log("a");
	}
}
