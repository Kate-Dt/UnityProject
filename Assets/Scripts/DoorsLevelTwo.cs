using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorsLevelTwo : MonoBehaviour
{

    public float loadDelay = 2f;

    void OnTriggerEnter2D()
    {

        Invoke("LoadLevelOne", loadDelay);

    }

    void LoadLevelOne()
    {
        SceneManager.LoadScene(3);
    }

}
