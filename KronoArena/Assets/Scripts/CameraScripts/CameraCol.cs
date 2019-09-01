using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCol : MonoBehaviour
{
    public GameObject Parent;

    Quaternion angle;
    Vector3 V3_angle;
    float tmp;

    // Start is called before the first frame update
    void Start()
    {
        Parent = GameObject.Find("CameraParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Network_01.gameplayflag == true)
        {
            if (PhotonNetwork.player.ID == 1)
            {
                if (TurnCol.P1_Turn == true)
                {
                    //VIVEコントローラーの角度をハンマーに代入
                    angle = this.transform.rotation;
                    //Quaternionをオイラー角Vecter3に変換
                    V3_angle = angle.eulerAngles;
                    V3_angle.z = 0f;
                    //戻す
                    angle = Quaternion.Euler(V3_angle);
                    this.transform.rotation = angle;
                }
                else
                {
                    //VIVEコントローラーの角度をハンマーに代入
                    angle = this.transform.rotation;
                    //Quaternionをオイラー角Vecter3に変換
                    V3_angle = angle.eulerAngles;
                    V3_angle.z = 180f;
                    //戻す
                    angle = Quaternion.Euler(V3_angle);
                    this.transform.rotation = angle;
                }
            }
            else if (PhotonNetwork.player.ID == 2)
            {
                if (TurnCol.P2_Turn == true)
                {
                    //VIVEコントローラーの角度をハンマーに代入
                    angle = this.transform.rotation;
                    //Quaternionをオイラー角Vecter3に変換
                    V3_angle = angle.eulerAngles;
                    V3_angle.z = 0f;
                    //戻す
                    angle = Quaternion.Euler(V3_angle);
                    this.transform.rotation = angle;
                }
                else
                {
                    //VIVEコントローラーの角度をハンマーに代入
                    angle = this.transform.rotation;
                    //Quaternionをオイラー角Vecter3に変換
                    V3_angle = angle.eulerAngles;
                    V3_angle.z = 180f;
                    //戻す
                    angle = Quaternion.Euler(V3_angle);
                    this.transform.rotation = angle;
                }
            }
        }
    }
}
