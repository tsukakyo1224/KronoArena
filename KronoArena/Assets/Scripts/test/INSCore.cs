﻿using System; // 注意
using System.Collections;
using Photon; // 注意
using UnityEngine;
using UnityEngine.UI;　//注意

public class INSCore : PunBehaviour, IPunTurnManagerCallbacks// このコールバックを使用する際は1，2，3，4，5を実装しなければならない
{
    private PhotonView PhotonView;//追加する

    [SerializeField]
    private RectTransform TimerFillImage;//タイマーの赤い部分

    [SerializeField]
    private Text TurnText;//ターン数の表示テキスト

    [SerializeField]
    private Text TimeText;//残り時間の表示テキスト

    [SerializeField]
    private Text WaitingText;//待ってくださいのテキスト

    private bool IsShowingResults;//真偽値




    private PunTurnManager turnManager;

    public void Awake()// StartをAwakeにする。
    {
        this.turnManager = this.gameObject.AddComponent<PunTurnManager>();//PunTurnManagerをコンポーネントに追加
        this.turnManager.TurnManagerListener = this;//リスナーを？
        this.turnManager.TurnDuration = 5f;//ターンは5秒にする

        //PhotonView = GameObject.Find("Scripts").GetComponent<PhotonView>();//scriptsにphotonviewを付けておくのを忘れずに。

    }


    void Update()
    {

        if (this.TurnText != null)
        {
            this.TurnText.text = this.turnManager.Turn.ToString();//何ターン目かを表示してくれる
        }

        if (this.turnManager.Turn > 0 || this.TimeText != null && !IsShowingResults)//ターンが0以上、TimeTextがnullでない、結果が見えていない場合。
        {

            this.TimeText.text = this.turnManager.RemainingSecondsInTurn.ToString("F1") + " SECONDS";//小数点以下1桁の残り時間を表示。

            TimerFillImage.anchorMax = new Vector2(1f - this.turnManager.RemainingSecondsInTurn / this.turnManager.TurnDuration, 1f);//残り時間のバーの表示。

            //this.turnManager.RemainingSecondsInTurn -= Time.deltaTime;
            //turnManager.TurnDuration -= Time.deltaTime;
            //this.turnManager.RemainingSecondsInTurn = turnManager.TurnDuration;
            if (turnManager.TurnDuration < 0f)
            {
                turnManager.TurnDuration = 5f;
                this.turnManager.Turn.ToString();
                Debug.Log("a");
            }
        }

        if (this.turnManager.IsCompletedByAll) //両方のプレイヤーがターンを終了しているか
        {
            //後に処理を書く予定
        }
            

    }

    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)//1
    {
        Debug.Log("OnTurnFinished: " + photonPlayer + " turn: " + turn + " action: " + move);
    }

    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)//2
    {
        Debug.Log("OnPlayerMove: " + photonPlayer + " turn: " + turn + " action: " + move);
    }

    public void OnTurnBegins(int turn)//3 ターンが開始した場合
    {
        Debug.Log("OnTurnBegins() turn: " + turn);

        IsShowingResults = false;
    }

    public void OnTurnCompleted(int obj)//4ターン終了時に呼ばれるメソッド　（あなたのターン開始・終了みたいな文字を出す）
    {
        Debug.Log("OnTurnCompleted: " + obj);
        this.OnEndTurn();//エンドターンに必要な処理をします
        this.StartTurn();
    }

    public void OnTurnTimeEnds(int turn)//5　タイマーが終了した場合
    {
        this.StartTurn();
    }

    public void StartTurn()//ターン開始メソッド（シーン開始時にRPCから呼ばれる呼ばれるようにしてあります。）
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.turnManager.BeginTurn();//turnmanagerに新しいターンを始めさせる
            PhotonView.RPC("RPC_AutomaticSend", PhotonTargets.All);
        }
    }

    public void MakeTurn(int index)//手を決めるメソッド
    {
        this.turnManager.SendMove(index, true);//アクションを送るときに呼ぶ（アクション,ターンを終了するかどうか(true)）
    }

    public void OnClickButton()
    {
        int index = 9;
        this.MakeTurn(index);
        Debug.Log(PhotonNetwork.player.ID);

    }
    public void OnEndTurn()//エンドターンのメソッド
    {
        //ターンエンドで必要な処理を書く予定
    }

    [PunRPC]
    public void RPC_AutomaticSend()
    {
        if ((this.turnManager.Turn % 2) + 1 == PhotonNetwork.player.ID)
        {
            int index = 0;
            this.turnManager.SendMove(index, true);
            this.WaitingText.text = "wait for another player...";
        }
        else
        {
            this.WaitingText.text = "";
        }
    }
}