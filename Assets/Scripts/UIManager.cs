using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public IEnumerator showSpawnText()
    {
        GameObject spawnText = GameObject.Find("SpawnText");
        spawnText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        spawnText.SetActive(false);
    }
}
