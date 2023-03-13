using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public GameObject spawnText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator showSpawnText()
    {
        spawnText.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        spawnText.SetActive(false);
    }
}
