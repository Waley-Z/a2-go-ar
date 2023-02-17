using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeButton : MonoBehaviour
{
    public GameManager.SceneType NewScene;

    public void OnSwitchButtonPressed()
    {
        GameManager.Instance.LoadScene(NewScene);
    }
}
