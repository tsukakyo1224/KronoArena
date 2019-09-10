using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCol : MonoBehaviour
{

    private GameObject NowLoading;
    private Image Panel;
    private AudioSource CameraBGM;

    public bool preparation = false;
    private bool NLFlag = false;    //NowLoadingが消えるまでのフラグ
    private bool FadeInFlag = false; //フェードインするまでのフラグ
    public bool StartCameraFlag = false;   //フェードインしてカメラのアニメータをオンにするまでのフラグ
    public bool StartFlag = false;  //ゲームスタートフラグ
    public bool TimeStartFlag = false;  //時間が動き出すフラグ
    private float t;


    // Start is called before the first frame update
    void Start()
    {
        NowLoading = GameObject.Find("NowLoading");
        Panel = GameObject.Find("Panel").GetComponent<Image>();
        AudioSource[] audioSources = GameObject.Find("Camera").GetComponents<AudioSource>();
        CameraBGM = audioSources[0];
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
            //DelayMethodを3.5秒後に呼び出す
            Invoke("CameraBGMOn", 1.0f);

        }
        if(StartFlag == true)
        {
            t += Time.deltaTime;
            if (t >= 1.0f)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().UIAnim();
            }
            //戦闘開始音を流す
            if(t >= 2.5f)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().BattleStartPlay();
                StartFlag = false;
            }
        }
    }

    public void CameraBGMOn()
    {
        CameraBGM.PlayOneShot(CameraBGM.clip);
    }
}
