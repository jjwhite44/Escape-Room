using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public void StartNew()
    {
        SceneManager.LoadScene(1); //the number in the () is the index of the scene, which can be configured in the build settings
    }
}
