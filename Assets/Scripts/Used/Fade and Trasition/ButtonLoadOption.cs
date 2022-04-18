using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadOption : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneLoader.Instance.LoadNewScene(sceneName);
    }
}
