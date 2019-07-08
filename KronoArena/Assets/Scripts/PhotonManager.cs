using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour
{

    private GameObject cube;
    //ゲーム開始フラグ
    public static bool startflag = false;

    //Photon接続
    public void ConnectPhoton()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    //ロビー入室時に呼ばれるコールバックメソッドを定義
    void OnJoinedLobby()
    {
        Debug.Log("PhotonManager OnJoinedLobby");
        //ボタンを押せるようにする
        GameObject.Find("CreateRoomB").GetComponent<Button>().interactable = true;
    }

    //ルーム作成
    [System.Obsolete]
    public void CreateRoom()
    {
        string userName = "ユーザ1";
        string userId = "user1";
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        //カスタムプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName); //ユーザ名
        customProp.Add("userId", userId); //ユーザID
        PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.customRoomProperties = customProp;
        //ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
        roomOptions.customRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.maxPlayers = 2; //部屋の最大人数
        roomOptions.isOpen = true; //入室許可する
        roomOptions.isVisible = true; //ロビーから見えるようにする
        //userIdが名前のルームがなければ作って入室、あれば普通に入室する。
        PhotonNetwork.JoinOrCreateRoom(userId, roomOptions, null);
    }

    //ルーム一覧が取れると
    [System.Obsolete]
    void OnReceivedRoomListUpdate()
    {
        //ルーム一覧を取る
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (rooms.Length == 0)
        {
            Debug.Log("ルームが一つもありません");
        }
        else
        {
            //ルームが1件以上ある時ループでRoomInfo情報をログ出力
            for (int i = 0; i < rooms.Length; i++)
            {
                Debug.Log("RoomName:" + rooms[i].name);
                Debug.Log("userName:" + rooms[i].customProperties["userName"]);
                Debug.Log("userId:" + rooms[i].customProperties["userId"]);
                GameObject.Find("StatusText").GetComponent<Text>().text = rooms[i].name;
            }
        }
    }

    //ルーム入室
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("user1");
    }

    //ルーム入室した時に呼ばれるコールバックメソッド
    void OnJoinedRoom()
    {
        Debug.Log("PhotonManager OnJoinedRoom");
        GameObject.Find("StatusText").GetComponent<Text>().text = "OnJoinedRoom";

        Vector3 initPos = new Vector3(6.34f, 3f, 7.17f);
        cube = PhotonNetwork.Instantiate("Cube", initPos, Quaternion.Euler(Vector3.zero), 0);

        startflag = true;
    }


    //作成したCubeの変数1を変更する
    public void ChangeVal()
    {
        cube.GetComponent<CubeScript>().hensu1 = 10;
        Debug.Log("change1");
    }

    //作成したCubeの変数2を変更する
    public void ChangeVal2()
    {
        cube.GetComponent<CubeScript>().hensu2 = 5.0f;
        Debug.Log("change2");
    }
}
