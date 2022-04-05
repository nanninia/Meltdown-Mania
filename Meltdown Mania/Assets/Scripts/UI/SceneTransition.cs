using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Help from: https://www.youtube.com/watch?v=05OfmBIf5os

public class SceneTransition : MonoBehaviour
{
    public void TransitionScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}