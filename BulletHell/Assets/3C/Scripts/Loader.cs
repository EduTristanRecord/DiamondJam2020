using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this);
        LoadGameScene();
        Application.targetFrameRate = 60;
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
}