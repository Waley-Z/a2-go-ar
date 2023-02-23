using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchModeButton : MonoBehaviour
{
    public GameManager.SceneType NewScene;

    public void OnSwitchButtonPressed()
    {
        GameManager.Instance.LoadScene(NewScene);
    }
}
