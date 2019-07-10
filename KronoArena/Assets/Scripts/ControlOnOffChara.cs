using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOnOffChara : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    public static float walkSpeed = 5.0f;
    private Rigidbody _rigidbody;
    //　現在キャラクターを操作出来るかどうか
    private bool isControl;
    //HPバー
    Slider HPBar;

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        _rigidbody = this.GetComponent<Rigidbody>();
        if(this.gameObject.name == "Player1")
        {
            //HPバー
            HPBar = GameObject.Find("Player1HP").GetComponent<Slider>();
        }
        else if(this.gameObject.name == "Player2")
        {
            //HPバー
            HPBar = GameObject.Find("Player2HP").GetComponent<Slider>();
        }
        else if (this.gameObject.name == "Player3")
        {
            //HPバー
            HPBar = GameObject.Find("Player3HP").GetComponent<Slider>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.playerList.Length == 1 && TurnCol.P1_Turn == true)
        {
            //Aを押すとjab
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetBool("Jab", true);

            }

            //Sを押すとHikick
            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("Hikick", true);
            }

            //Dを押すとSpinkick
            if (Input.GetKeyDown(KeyCode.D))
            {
                animator.SetBool("Spinkick", true);
            }

            if ((_rigidbody != null) && characterController.isGrounded)
            {
                velocity = Vector3.zero;

                //if (isControl)
                //{

                var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

                if (input.magnitude > 0f)
                {
                    //animator.SetFloat("Speed", input.magnitude);
                    transform.LookAt(transform.position + input);
                    velocity = transform.forward * walkSpeed;
                }
                else
                {
                    //animator.SetFloat("Speed", 0f);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    velocity.y += 5f;
                }
                animator.SetBool("Run", velocity.magnitude > 1.0f);
                //Debug.Log(velocity.magnitude);
                //}
            }
        }


        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void ChangeControl(bool controlFlag)
    {
        isControl = controlFlag;
    }


    //攻撃判定
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.CompareTag("Enemy")){
            //HP減少
            //HPBar.value -= 1;
            if (hit.gameObject.name == "YourChara1")
            {
                HPStatusUI.hpSlider.value -= 1;
            }
            if (hit.gameObject.name == "YourChara2")
            {
                HPStatusUI.hpSlider.value -= 1;
            }

            if (hit.gameObject.name == "YourChara3")
            {
                HPStatusUI.hpSlider.value -= 1;
            }
            //HPStatusUI.hpSlider.value -= 1;
        }
    }

}
