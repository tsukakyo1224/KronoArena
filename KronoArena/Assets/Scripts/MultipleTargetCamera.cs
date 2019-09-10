using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{

    private bool CameraFlag;

    public Camera cam;
    public List<Transform> targets = new List<Transform>();
    public Vector3 offset;
    public float smoothTime = 0.5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter = 50;
    private Vector3 velocity;
    private Vector3 pos = new Vector3();
    private Vector3 pos2 = new Vector3();
    private Vector3 center = new Vector3();
    private int i = 0;
    private int maxcount = 6;
    private int count = 0;

    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        CameraFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.player.ID == 1)
        {
            offset = new Vector3(-5.0f, 8.0f, 20.0f);
        }
        else if (PhotonNetwork.player.ID == 2)
        {
            offset = new Vector3(-5.0f, 8.0f, -20.0f);
        }
        if(CameraFlag == true)
        {
            if(PhotonNetwork.player.ID == 1)
            {
                i = ChangeChara.nowChara;
            }
            else if(PhotonNetwork.player.ID == 2)
            {
                i = ChangeChara.nowChara + 3;
            }


            pos2 = targets[i].position;
            pos = new Vector3(0, 0, 0);
            for(int i = 0; i<maxcount; i++)
            {
                if (targets[i] != null)
                {
                    pos += targets[i].position;
                    count++;
                }
            }
            maxcount = count;
            count = 0;
            pos += pos2;
            center = pos / maxcount;
            this.transform.LookAt(center);          //CenterPointのポジションを中心に配置
            //Debug.Log(center);
            //cameraPos.LookAt(this.transform);           //CameraPositionを中心の方向に向かせる
        }

    }
    private void Reset()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (targets.Count == 0 || PhotonNetwork.playerList.Length != 2) return;
        Move();
        Zoom();
    }
    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
    private void Move()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }
    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < maxcount; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    [PunRPC]
    public void AddChara()
    {
        targets.Add(GameObject.Find("P1_Chara1").transform);
        targets.Add(GameObject.Find("P1_Chara2").transform);
        targets.Add(GameObject.Find("P1_Chara3").transform);
        targets.Add(GameObject.Find("P2_Chara1").transform);
        targets.Add(GameObject.Find("P2_Chara2").transform);
        targets.Add(GameObject.Find("P2_Chara3").transform);
        CameraFlag = true;
        GameObject.Find("GameManager").GetComponent<StartCol>().preparation = true;
    }
    /*
    [PunRPC]
    public void AddChara()
    {
        targets.Add(GameObject.Find("P1_Chara1").transform);
        targets.Add(GameObject.Find("P1_Chara2").transform);
        targets.Add(GameObject.Find("P1_Chara3").transform);
        CameraFlag = true;
    }*/

    public void AddCharaOn()
    {
        this.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player.ID);
        photonView.RPC("AddChara", PhotonTargets.All);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //stream.SendNext(this.transform.position);

        }
        else
        {
            //this.transform.position = (Vector3)stream.ReceiveNext();　//現在のポジションを受信
        }
    }

}
