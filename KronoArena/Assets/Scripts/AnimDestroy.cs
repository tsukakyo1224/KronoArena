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
        if(DesTime >= 1.0f)
        {
            this.GetComponent<Animator>().enabled = false;
        }
    }
}
