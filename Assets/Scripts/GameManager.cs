using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;
public class GameManager : Manager<GameManager>
{
    public enum SceneType
    {
        Exploration,
        Interaction
    }

    public GameScene[] SceneArray;
    public Image BlackScreen;
    public static Vector2d targetPos; // AR cursor position

    [System.Serializable]
    public class GameScene
    {
        public SceneType type;
        public string name;
    }

    // every time before OnSceneLoaded
    new void Awake()
    {
        base.Awake();
        //Application.targetFrameRate = 60;
        //QualitySettings.vSyncCount = 1;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        StartSceneMusic(getSceneType(SceneManager.GetActiveScene().name));
    }

    public static void updateTargetPos(Vector2d pos)
    {
        targetPos = pos;
    }

    public static SceneType getSceneType(string name)
    {
        foreach (GameScene scene in Instance.SceneArray)
        {
            if (scene.name == name)
            {
                return scene.type;
            }
        }
        Debug.LogError($"Scene not found: {name}");
        return SceneType.Exploration;
    }

    private static string getSceneName(SceneType sceneType)
    {
        foreach (GameScene scene in Instance.SceneArray)
        {
            if (scene.type == sceneType)
            {
                return scene.name;
            }
        }
        Debug.LogError("Scene not found");
        return null;
    }

    public void LoadScene(SceneType sceneType)
    {
        StartCoroutine(SceneTransition(sceneType, 2));
    }

    private IEnumerator SceneTransition(SceneType sceneType, float duration)
    {
        SoundManager.Instance.pauseBGM(duration / 2);
        yield return StartFade(duration / 2, 1);
        Time.timeScale = 1f;
        SceneManager.LoadScene(getSceneName(sceneType));
        yield return StartFade(duration / 2, 0);
        StartSceneMusic(sceneType);
    }

    private void StartSceneMusic(SceneType sceneType)
    {
        switch (sceneType)
        {
            case SceneType.Exploration:
                SoundManager.StartBGM(SoundManager.Sound.MusicExploration);
                break;
            case SceneType.Interaction:
                SoundManager.StartBGM(SoundManager.Sound.MusicInteraction);
                break;
            default:
                break;
        }
    }

    public static IEnumerator StartFade(float duration, float targetAlpha)
    {
        Instance.BlackScreen.gameObject.SetActive(true);

        float currentTime = 0;
        float start = Instance.BlackScreen.color.a;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(start, targetAlpha, currentTime / duration);
            Instance.BlackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    
        Instance.BlackScreen.gameObject.SetActive(targetAlpha > 0);
        yield break;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded: {scene.name} @ {Time.time}s");
        SoundManager.Instance.lowPassBGM(false);
    }

    public void Restart()
    {
        LoadScene(getSceneType(SceneManager.GetActiveScene().name));
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
