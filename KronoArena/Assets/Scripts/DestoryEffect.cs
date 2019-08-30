using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour {

    public bool AttackFlag;

    public bool AttackObject;


	// Use this for initialization
	void Start () {
        AttackFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        //エフェクトを消す
        //メディック
        if (this.gameObject.name == "HolyBall(Clone)")
        {
            Destroy(this.gameObject, 5f);
        }
        if (this.gameObject.name == "Heel(Clone)")
        {
            Destroy(this.gameObject, 20f);
        }
        if (this.gameObject.name == "HeelShower(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        if (this.gameObject.name == "Medic_Buff(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        if (this.gameObject.name == "Medic_BuffSet(Clone)")
        {
            Destroy(this.gameObject, 10f);
        }
        //ナイト
        if (this.gameObject.name == "Knight_RollSet(Clone)")
        {
            Destroy(this.gameObject, 20f);
        }
        if (this.gameObject.name == "Knight_Roll(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        if (this.gameObject.name == "Knight_BuffSet(Clone)")
        {
            Destroy(this.gameObject, 10f);
        }
        if (this.gameObject.name == "Knight_Buff(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        //ガーディアン
        if (this.gameObject.name == "Guardian_BuffSet1(Clone)")
        {
            Destroy(this.gameObject, 15f);
        }
        if (this.gameObject.name == "Guardian_Buff1(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        if (this.gameObject.name == "Guardian_BigShieldSet(Clone)")
        {
            Destroy(this.gameObject, 20f);
        }
        if (this.gameObject.name == "Guardian_BigShield(Clone)")
        {
            Destroy(this.gameObject, 3f);
        }
        if (this.gameObject.name == "Guardian_Absorption(Clone)")
        {
            Destroy(this.gameObject, 10.0f);
        }

    }

    //オブジェクトがぶつかった時
    void OnParticleCollision(GameObject obj) {
		if (obj.tag != this.tag && AttackObject == true)
        {
            Guardian();
            if (AttackFlag == false)
            {
                obj.GetComponent<Status>().HP -=
                    (int)(this.GetComponent<Status>().Magic_Attack / ((1 + obj.GetComponent<Status>().Magic_Defense) / 10));
                Debug.Log(obj + "に" + (int)(this.GetComponent<Status>().Magic_Attack /
                    ((1 + obj.GetComponent<Status>().Defense) / 10)) + "ダメージ");
            }
            GameObject.Find("GameManager").GetComponent<GameManager>().AudioPlay();

            AttackFlag = false;
        }
        Destroy(this.gameObject);
    }


    //エフェクトを消す
	public void Delete(){
		Destroy (this.gameObject);
	}

    //エフェクトを消す
    public void Delete2()
    {
        Destroy(this.gameObject, 3.0f);
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
