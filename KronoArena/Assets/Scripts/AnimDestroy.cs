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
        DesTime += Time.deltaTime;
        if(GameObject.Find("TurnCol").GetComponent<TurnCol>().TurnNum == 1)
        {
            this.GetComponent<Animator>().enabled = true;
        }
        else
        {
            this.GetComponent<Animator>().enabled = false;
        }
        if(DesTime >= 1.0f)
        {
            this.GetComponent<Animator>().enabled = false;
        }
    }
}
