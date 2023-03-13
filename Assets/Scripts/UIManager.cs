using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public GameObject spawnText;

    private void Start()
    {
        spawnText = transform.GetChild(0).transform.GetChild(0).gameObject;
    }

    public IEnumerator showSpawnText()
    {
        spawnText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        spawnText.SetActive(false);
    }
}
