using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour {

    public bool AttackFlag;

	// Use this for initialization
	void Start () {
        AttackFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //オブジェクトがぶつかった時
	void OnParticleCollision(GameObject obj) {
		if (obj.tag != this.tag)
        {
            Guardian();
            if (AttackFlag == false)
            {
                obj.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Magic_Attack / ((1 + obj.GetComponent<Status>().Magic_Defense) / 10));
                Debug.Log(obj + "に" + (int)(this.GetComponent<Status>().Magic_Attack /
                    ((1 + obj.GetComponent<Status>().Defense) / 10)) + "ダメージ");
            }
            AttackFlag = false;
        }
		Destroy (this.gameObject);
	}

	public void Delete(){
		Destroy (this.gameObject);
	}

    //周りにガーディアンがいて、ガーディアンが身代わりをしていたらガーディアンに攻撃
    void Guardian()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if (PhotonNetwork.player.ID == 2)
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);
            //対象キャラとの距離表示
            if (obj.GetComponent<Status>().Name == "Guardian" && dist < 2.0)
            {
                if (obj.GetComponent<Guardian_Data>().GuardFlag == true)
                {
                    obj.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Attack / ((1 + obj.GetComponent<Status>().Defense) / 10));
                    AttackFlag = true;
                }
            }
        }
    }

}
