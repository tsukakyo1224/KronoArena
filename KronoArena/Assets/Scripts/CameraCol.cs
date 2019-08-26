using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Network_01.gameplayflag == true)
        {
            if (PhotonNetwork.player.ID == 1)
            {
                this.transform.position = new Vector3(0.0f, 5.0f, 10.0f);
            }
            else if (PhotonNetwork.player.ID == 2)
            {
                this.transform.position = new Vector3(0.0f, 3.0f, -7.0f);
                //this.transform.rotation = Quaternion.Euler(this.transform.position.x, this.transform.position.y-180.0f, 180.0f);
            }
        }
    }
}
