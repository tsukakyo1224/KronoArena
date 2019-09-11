using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingLaser : MonoBehaviour
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

    public float _rotSpeed = 180.0f;

    ParticleSystem ps;
    ParticleSystem.Particle[] m_Particles;


    void Start()
    {
        ps = GetComponent<ParticleSystem>();


        dis = 100.0f;
        // 初期位置をposionに格納
        position = transform.position;
        // rigidbody取得
        rigid = this.GetComponent<Rigidbody>();

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

                float angle = 30.0f;    //攻撃範囲内の角度

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


        if (target != null)
        {

            m_Particles = new ParticleSystem.Particle[ps.main.maxParticles];
            int numParticlesAlive = ps.GetParticles(m_Particles);
            for (int i = 0; i < numParticlesAlive; i++)
            {
                var velocity = m_Particles[i].velocity;
                var position = m_Particles[i].position;
                // ターゲットへのベクトル
                var direction = ps.transform.InverseTransformPoint(target.TransformPoint(target.position)) - position;
                // ターゲットまでの角度
                float angleDiff = Vector3.Angle(velocity, direction);
                // 回転角
                float angleAdd = (_rotSpeed * Time.deltaTime);
                // ターゲットへ向けるクォータニオン
                Quaternion rotTarget = Quaternion.FromToRotation(velocity, direction);
                if (angleDiff <= angleAdd)
                {
                    // ターゲットが回転角以内なら完全にターゲットの方を向く
                    m_Particles[i].velocity = (rotTarget * velocity);
                }
                else
                {
                    // ターゲットが回転角の外なら、指定角度だけターゲットに向ける
                    float t = (angleAdd / angleDiff);
                    m_Particles[i].velocity = Quaternion.Slerp(Quaternion.identity, rotTarget, t) * velocity;
                }
            }
            ps.SetParticles(m_Particles, numParticlesAlive);


            /*
            var velocity = this.velocity;
            var position = this.position;

            var direction = this.transform.InverseTransformPoint(target.TransformPoint
                (target.position)) - position;

            float angleDiff = Vector3.Angle(velocity, direction);

            //回転角
            float angleAdd = (_rotspeed * Time.deltaTime);

            //ターゲットへ向けるクォータニオン
            Quaternion rotTarget = Quaternion.FromToRotation(velocity, direction);

            if (angleDiff <= angleAdd)
            {
                //ターゲットが回転角以内なら完全にターゲットの方を向く
                this.velocity = (rotTarget * velocity);
            }
            else
            {
                //ターゲットが回転角の外なら指定角度だけターゲットの方を向く
                float t = (angleAdd / angleDiff);
                this.velocity = Quaternion.Slerp(Quaternion.identity, rotTarget, t) * velocity;
            }
        }*/
            Debug.Log(this.velocity);
        }

        void FixedUpdate()
        {
            // 移動処理
            //rigid.MovePosition(transform.position + velocity * Time.deltaTime);
        }

    }
}