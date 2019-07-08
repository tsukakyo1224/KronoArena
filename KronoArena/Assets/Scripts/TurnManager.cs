using System.Collections;
using System.Collections.Generic;
using Photon; // 注意
using UnityEngine;
using UnityEngine.UI;　//注意
public class INSCore : PunBehaviour, IPunTurnManagerCallbacks
{
    private PunTurnManager turnManager;

    public void Awake()// StartをAwakeにする。
    {
        this.turnManager = this.gameObject.AddComponent<PunTurnManager>();//PunTurnManagerをコンポーネントに追加
        this.turnManager.TurnManagerListener = this;//リスナー？
        this.turnManager.TurnDuration = 30f;//ターンは5秒にする
        //PhotonView = GameObject.Find("scripts").GetComponent<PhotonView>();//scriptsにphotonviewを付けておくのを忘れずに。
    }

    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)//1
    {
        //今のところ使っていない機能です。使い方が分かったら教えてください。
    }
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)//2
    {
        //今のところ使っていない機能です。使い方が分かったら教えてください。
    }
    public void OnTurnBegins(int turn)//3 
    {
        //ターンが開始した場合に呼びたい処理を書きます。
    }
    public void OnTurnCompleted(int obj)//4
    {
        //ターンのムーブ（後述）が全プレイヤー終了した場合に呼びたい処理を書きます。
        this.StartTurn();//ここで次のターンを開始しています（後述）。
    }
    public void OnTurnTimeEnds(int turn)//5　タイマーが終了した場合
    {
        //タイマーが終了した場合に呼びたい処理を書きます。
        this.StartTurn();//ここで次のターンを開始しています（後述）。
    }

    public void StartTurn()//ターン開始メソッド（シーン開始時にRPCから呼ばれる呼ばれるようにしてあります。）
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.turnManager.BeginTurn();//turnmanagerに新しいターンを始めさせる
        }
    }
}