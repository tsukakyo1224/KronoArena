using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCol : MonoBehaviour
{

    private GameObject NowLoading;
    private Image Panel;

    public bool preparation = false;
    private bool NLFlag = false;    //NowLoadingが消えるまでのフラグ
    private bool FadeInFlag = false; //フェードインするまでのフラグ
    public bool StartCameraFlag = false;   //フェードインしてカメラのアニメータをオンにするまでのフラグ
    public bool StartFlag = false;  //ゲームスタートフラグ
    public bool TimeStartFlag = false;
    private float t;


    // Start is called before the first frame update
    void Start()
    {
        NowLoading = GameObject.Find("NowLoading");
        Panel = GameObject.Find("Panel").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //NowLoading
        if (preparation == true && PhotonNetwork.playerList.Length == 2)
        {
            Destroy(NowLoading);
            NLFlag = true;
            preparation = false;
        }
        //フェードイン
        if (NLFlag)
        {
            Panel.GetComponent<FadeController>().isFadeIn = true;
            NLFlag = false;
        }

        if (StartCameraFlag)
        {
            GameObject.Find("Camera").GetComponent<Animator>().enabled = true;
            StartCameraFlag = false;
        }
        if(StartFlag == true)
        {
            t += Time.deltaTime;
            if(t >= 1.0f)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().UIAnim();
            }
            if(t >= 2.0f)
            {
                TimeStartFlag = true;
                Debug.Log("Start");
                StartFlag = false;
            }
        }
    }
}
