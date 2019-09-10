using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroy : MonoBehaviour
{
    private float DesTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DesTime += Time.deltaTime;
        if(GameObject.Find("TurnCol").GetComponent<TurnCol>().TurnNum == 1)
        {
            if (PhotonNetwork.player.ID == 1 && this.name == "Sand_MyUp")
            {
                this.GetComponent<Animator>().enabled = true;

            }
            else if (PhotonNetwork.player.ID == 2 && this.name == "Sand_YouUp")
            {
                this.GetComponent<Animator>().enabled = true;
            }
        }
    }
}
