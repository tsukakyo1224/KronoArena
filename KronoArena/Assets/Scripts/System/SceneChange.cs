using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SceneChangeMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void SceneChangeTitle()
    {
        GameObject.Find("PhotonManager").GetComponent<Network_01>().Leave();
        //SceneManager.LoadScene("Title");
    }
}
