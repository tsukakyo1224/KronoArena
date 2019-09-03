using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCol_test : MonoBehaviour
{
    private Vector3 tapPoint;
    private GameObject player;
    private Vector3 velocity = Vector3.zero;
    public float playerSpeed;
    private Animator animator;
    //private Animation anim;


    private PhotonView photonView;
    private PhotonTransformView photonTransformView;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        photonTransformView = GetComponent<PhotonTransformView>();
        player = this.gameObject;
        playerSpeed = ControlOnOffChara.walkSpeed;
        animator = GetComponent<Animator>();
        //anim = GetComponent<Animation>();

        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        //走り続ける防止
        if ((PhotonNetwork.player.ID == 1 && TurnCol.P1_Turn == false) ||
            (PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == false))
        {
            animator.SetBool("Run", false);
        }

        if (photonView.isMine && this.GetComponent<Status>().ActionFlag ==false)
        {
            if(PhotonNetwork.player.ID==1 && TurnCol.P1_Turn == false)
            {
                return;
            }
            if(PhotonNetwork.player.ID == 2 && TurnCol.P2_Turn == false)
            {
                return;
            }

            //AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo(0);
            Debug.Log(clipinfo[0].clip.name);

            if(clipinfo[0].clip.name == "attack01" || clipinfo[0].clip.name == "attack02" || clipinfo[0].clip.name == "attack03")
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                //押下した位置を取得
                tapPoint = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                if (PhotonNetwork.player.ID == 1)
                {
                    Vector3 currentTapPoint = Input.mousePosition;
                    //指をずらしたときのベクトルを取得
                    Vector3 mag = currentTapPoint - tapPoint;
                    //指を動かしたときのみキャラを操作する
                    if (tapPoint != currentTapPoint)
                    {
                        //動かした指の位置から角度を計算
                        float rad = Mathf.Atan2(currentTapPoint.y - tapPoint.y, currentTapPoint.x - tapPoint.x);
                        float rot = (rad * 180 / Mathf.PI) + 90;
                        //キャラクターの向きを決定
                        player.transform.rotation = Quaternion.Euler(0f, rot * -1, 0f);
                        player.transform.Translate(0f, 0f, playerSpeed);
                        //一定以上画面をドラッグしたときは移動速度を上げる
                        if (mag.magnitude > 5f)
                        {
                            velocity = transform.forward * playerSpeed;
                            animator.SetBool("Run", mag.magnitude > 1.0f);
                            playerSpeed = this.gameObject.GetComponent<Status>().Speed;
                        }
                        else if (mag.magnitude <= 5f)
                        {
                            animator.SetBool("Run", mag.magnitude > 1.0f);
                            ControlOnOffChara.walkSpeed = 5.0f;
                        }
                        characterController.Move(velocity * Time.deltaTime);
                    }
                }
                //プレイヤー2は操作を逆に
                else if (PhotonNetwork.player.ID == 2)
                {
                    Vector3 currentTapPoint = Input.mousePosition;
                    //指をずらしたときのベクトルを取得
                    Vector3 mag = currentTapPoint - tapPoint;
                    //指を動かしたときのみキャラを操作する
                    if (tapPoint != currentTapPoint)
                    {
                        //動かした指の位置から角度を計算
                        float rad = Mathf.Atan2(currentTapPoint.y - tapPoint.y, currentTapPoint.x - tapPoint.x);
                        float rot = (rad * 180 / Mathf.PI) + 270;
                        //キャラクターの向きを決定
                        player.transform.rotation = Quaternion.Euler(0f, rot * -1, 0f);
                        player.transform.Translate(0f, 0f, playerSpeed);
                        //一定以上画面をドラッグしたときは移動速度を上げる
                        if (mag.magnitude > 5f)
                        {
                            velocity = transform.forward * playerSpeed;
                            animator.SetBool("Run", mag.magnitude > 1.0f);
                            playerSpeed = this.gameObject.GetComponent<Status>().Speed;
                        }
                        else if (mag.magnitude <= 5f)
                        {
                            animator.SetBool("Run", mag.magnitude > 1.0f);
                            ControlOnOffChara.walkSpeed = 5.0f;
                        }
                        characterController.Move(velocity * Time.deltaTime);
                    }
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("Run", false);
            }
            //現在の移動速度
            velocity = characterController.velocity;
            //移動速度を指定
            photonTransformView.SetSynchronizedValues(speed: velocity, turnSpeed: 0);
            //if (!(anim.IsPlaying("attack01") || anim.IsPlaying("attack02") || anim.IsPlaying("attack03")))
            //if (!anim.IsPlaying("attack01"))
            //{

            //}
        }
    }


    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.transform.position);

        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();　//現在のポジションを受信
        }
    }
}
