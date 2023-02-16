using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARObjectSpawner : MonoBehaviour
{
    public Transform cursor;
    public GameObject gameobject_to_spawn;
    public GameObject gameobject_to_spawn_2;

    public AudioClip spawn_sound_effect;

    public void SpawnGameobjectAtCursor()
    {
        GameObject new_object = Instantiate(gameobject_to_spawn);
        new_object.transform.SetPositionAndRotation(cursor.position, cursor.rotation);

        AudioSource.PlayClipAtPoint(spawn_sound_effect, Camera.main.transform.position);
    }

    public void SpawnGameobjectAtCursor2()
    {
        GameObject new_object = Instantiate(gameobject_to_spawn_2);
        new_object.transform.SetPositionAndRotation(cursor.position, cursor.rotation);

        AudioSource.PlayClipAtPoint(spawn_sound_effect, Camera.main.transform.position);
    }

    public void SwitchModes()
    {
        SceneManager.LoadScene("exploration_scene");
    }
}



