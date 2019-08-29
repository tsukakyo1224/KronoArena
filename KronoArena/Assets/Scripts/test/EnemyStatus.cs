using UnityEngine;
using System.Collections;
 
public class EnemyStatus : MonoBehaviour {
 
    //　敵のMaxHP
    [SerializeField]
    private int maxHp = 100;
    //　敵のHP
    private int hp;
    //　敵の攻撃力
    [SerializeField]
    private int attackPower = 1;
    //private Enemy enemy;
    //　敵のHPバー処理スクリプト
    private HPStatusUI hpStatusUI;
 
    void Start() {
        //enemy = GetComponent<Enemy> ();
        hp = maxHp;
        hpStatusUI = GetComponentInChildren <HPStatusUI> ();
    }
 
    public void SetHp(int hp) {
        this.hp = hp;
 
        //　HP表示用UIのアップデート
        //hpStatusUI.UpdateHPValue ();
 
        if (hp <= 0) {
            //　HP表示用UIを非表示にする
            //hpStatusUI.SetDisable ();
            //enemy.Dead ();
        }
    }
 
    public int GetHp() {
        return hp;
    }
 
    public int GetMaxHp() {
        return maxHp;
    }
}