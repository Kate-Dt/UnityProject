using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorsLevelOne : MonoBehaviour {

    public float loadDelay = 2f;

    void OnTriggerEnter2D()
    {

        Invoke("LoadLevelOne", loadDelay);
       
    }

    void LoadLevelOne()
    {
        SceneManager.LoadScene(2);
    }

}
