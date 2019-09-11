using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmedic : MonoBehaviour
{
    //Rigidbodyを入れる変数
    Rigidbody rigid;
    //速度
    Vector3 velocity;
    //発射するときの初期位置
    Vector3 position;
    // 加速度
    public Vector3 acceleration;
    // ターゲットをセットする
    public Transform target;
    // 着弾時間
    float period = 2f;

    //一番近いキャラの情報を見つけるやつ
    public float dis;


    void Start()
    {

        // 初期位置をposionに格納
        position = transform.position;
        // rigidbody取得
        rigid = this.GetComponent<Rigidbody>();

        dis = 100.0f;
        // 初期位置をposionに格納
        position = transform.position;

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player2");
        if (this.tag == "Player2")
        {
            targets = GameObject.FindGameObjectsWithTag("Player1");
        }
        foreach (GameObject obj in targets)
        {
            // 対象となるGameObjectとの距離を調べ、近くだったら何らかの処理をする
            float dist = Vector3.Distance(obj.transform.position, transform.position);

            if (obj.tag != this.tag)
            {
                Vector3 eyeDir = this.transform.forward; // プレイヤーの視線ベクトル。
                Vector3 playerPos = this.transform.position; // プレイヤーの位置
                Vector3 enemyPos = obj.transform.position; // 敵の位置

                float angle = 120.0f;    //攻撃範囲内の角度

                // プレイヤーと敵を結ぶ線と視線の角度差がangle以内なら当たり
                if (Vector3.Angle((enemyPos - playerPos).normalized, eyeDir) <= angle)
                {
                    if (dis > dist)
                    {
                        dis = dist;
                        target = obj.transform;
                    }
                }
            }
        }

    }


    void Update()
    {

        acceleration = Vector3.zero;

        //ターゲットと自分自身の差
        var diff = target.position - transform.position;

        //加速度を求めてるらしい
        acceleration += (diff - velocity * period) * 2f
                        / (period * period);


        //加速度が一定以上だと追尾を弱くする
        if (acceleration.magnitude > 100f)
        {
            acceleration = acceleration.normalized * 100f;
        }

        // 着弾時間を徐々に減らしていく
        period -= Time.deltaTime;

        // 速度の計算
        velocity += acceleration * Time.deltaTime;

    }

    void FixedUpdate()
    {
        // 移動処理
        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    void OnCollisionEnter()
    {
        // 何かに当たったら自分自身を削除
        Destroy(this.gameObject);

    }
}
