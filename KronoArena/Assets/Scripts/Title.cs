﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour {

	float alfa;
	float speed = 0.03f;
	float red, green, blue;
	bool UPDOWN = false;

	void Start () {
		red = GetComponent<Image>().color.r;
		green = GetComponent<Image>().color.g;
		blue = GetComponent<Image>().color.b;
	}

	void Update () {
		GetComponent<Image>().color = new Color(red, green, blue, alfa);
		//alfa += speed;

		if (UPDOWN == false) {
			alfa += speed;
			if (alfa >= 1) {
				UPDOWN = true;
			}
		} else if (UPDOWN == true) {
			alfa -= speed;
			if (alfa <= 0) {
				UPDOWN = false;
			}
		}
			
	}
}
