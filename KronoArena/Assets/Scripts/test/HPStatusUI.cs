using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPStatusUI : MonoBehaviour
{

    //　親オブジェクトステータス
    private GameObject ParentChara;
    //　親ステータス
    //private GameObject ParentStatus;
    //　HP表示用スライダー
    public Slider hpSlider;

    void Start()
    {
        //　HP用Sliderを子要素から取得
        hpSlider = transform.Find("HPBar").GetComponent<Slider>();
        //マックスHPを取得
        hpSlider.maxValue = ParentChara.GetComponent<Status>().hpSlider.maxValue;
        //親オブジェクト取得
        ParentChara = transform.root.gameObject;
        if((PhotonNetwork.player.ID == 1 && ParentChara.tag == "Player1") ||
            (PhotonNetwork.player.ID == 2 && ParentChara.tag == "Player2"))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;
        hpSlider.maxValue = ParentChara.GetComponent<Status>().hpSlider.maxValue;
        hpSlider.value = ParentChara.GetComponent<Status>().hpSlider.value;
    }
}
