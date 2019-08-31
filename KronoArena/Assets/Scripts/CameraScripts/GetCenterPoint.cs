using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCenterPoint : MonoBehaviour
{
    /*
    public List<Transform> transList = new List<Transform>();   //カメラ範囲内におさめたいオブジェクトのリスト
    private Transform cameraPos;
    public Camera usingCamera;
    private Vector3 pos = new Vector3();
    private Vector3 center = new Vector3();
    private float radius;
    private float margin = 1.0f;        //半径を少し余分にとるための値
    private float distance;
    private float cameraHeight = 1.5f;  //カメラが地面にめり込まないようにカメラを浮かせる高さ

    private bool CameraFlag;

    void Start()
    {
        cameraPos = GameObject.Find("CameraPosition").GetComponent<Transform>();
        CameraFlag = false;
    }

    void Update()
    {
        //if(PhotonNetwork.playerList.Length == 2 && CameraFlag == false)
        if(GameManager.cameraflag == true)
        {
            transList.Add(GameObject.Find("P1_Chara1").transform);
            transList.Add(GameObject.Find("P1_Chara2").transform);
            transList.Add(GameObject.Find("P1_Chara3").transform);
            transList.Add(GameObject.Find("P2_Chara1").transform);
            transList.Add(GameObject.Find("P2_Chara2").transform);
            transList.Add(GameObject.Find("P2_Chara3").transform);
            CameraFlag = true;
        }
        pos = new Vector3(0, 0, 0);
        radius = 0.0f;
        foreach (Transform trans in transList)
        {     //オブジェクトのポジションの平均値を算出
            pos += trans.position;
        }
        center = pos / transList.Count;
        this.transform.position = center;           //CenterPointのポジションを中心に配置
        foreach (Transform trans in transList)
        {     //中心から最も遠いオブジェクトとの距離を算出
            radius = Mathf.Max(radius, Vector3.Distance(center, trans.position));
        }
        distance = (radius + margin) / Mathf.Sin(usingCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);   //カメラの距離を算出
        cameraPos.localPosition = new Vector3(0, cameraHeight, -distance);    //CameraPositionをカメラの距離をもとに配置
        cameraPos.LookAt(this.transform);           //CameraPositionを中心の方向に向かせる
    }*/

    public List<Transform> transList = new List<Transform>();   //カメラ範囲内におさめたいオブジェクトのリスト
    private Transform cameraPos;
    public Camera usingCamera;
    private Vector3 pos = new Vector3();
    private Vector3 pos2 = new Vector3();
    private Vector3 center = new Vector3();
    private int i = 0;
    // Use this for initialization
    void Start()
    {
        //cameraPos = GameObject.Find("CameraPosition").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.cameraflag == true)
        {
            transList.Add(GameObject.Find("P1_Chara1").transform);
            transList.Add(GameObject.Find("P1_Chara2").transform);
            transList.Add(GameObject.Find("P1_Chara3").transform);
            transList.Add(GameObject.Find("P2_Chara1").transform);
            transList.Add(GameObject.Find("P2_Chara2").transform);
            transList.Add(GameObject.Find("P2_Chara3").transform);
            //CameraFlag = true;
        }


        i = ChangeChara.nowChara;

        pos2 = transList[i].position;
        pos = new Vector3(0, 0, 0);
        pos = transList[0].position + transList[1].position + transList[2].position + pos2;
        center = pos / 3;
        this.transform.LookAt(center);          //CenterPointのポジションを中心に配置
        //cameraPos.LookAt(this.transform);           //CameraPositionを中心の方向に向かせる
    }
}
