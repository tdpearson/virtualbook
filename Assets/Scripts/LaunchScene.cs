using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour {

    public void LoadSceneButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
