using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCol_test : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 tapPoint;
    private GameObject player;
    private Vector3 velocity = Vector3.zero;
    public float playerSpeed;
    private Animator animator;

    private PhotonView photonView;
    private PhotonTransformView photonTransformView;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        player = this.gameObject;
        playerSpeed = ControlOnOffChara.walkSpeed;
        animator = GetComponent<Animator>();

        photonTransformView = GetComponent<PhotonTransformView>();
        photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //押下した位置を取得
                tapPoint = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
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
                        //Debug.Log(mag.magnitude);
                        animator.SetBool("Run", mag.magnitude > 1.0f);
                        playerSpeed = 5.0f;
                    }
                    else
                    if (mag.magnitude <= 5f)
                    {
                        //Debug.Log(mag.magnitude);
                        animator.SetBool("Run", mag.magnitude > 1.0f);
                        ControlOnOffChara.walkSpeed = 5.0f;
                    }
                    characterController.Move(velocity * Time.deltaTime);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("Run", false);
            }
        }

    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);

        }
        else
        {

        }
    }
}
