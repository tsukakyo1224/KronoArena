using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TitleButton : MonoBehaviour {

	public GameObject Title;
	public GameObject TitleMOVIE;


	float alfa;
	float red, green, blue;

	// Use this for initialization
	void Start () {
		

		red = TitleMOVIE.GetComponent<RawImage>().color.r;
		green = TitleMOVIE.GetComponent<RawImage>().color.g;
		blue = TitleMOVIE.GetComponent<RawImage>().color.b;
		alfa = TitleMOVIE.GetComponent<RawImage> ().color.a;

		//Title.SetActive (true);
		//TitleMOVIE.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

	}

	//ボタン関連
	public void StartButton(){

		//Title.SetActive (false);
		Destroy(Title, 0.4f);
		TitleMOVIE.GetComponent<VideoPlayer> ().Play();


	}

}
