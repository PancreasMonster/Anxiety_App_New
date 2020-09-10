using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;
    public GameObject loadingScreen, cam;
    public Image progressBar;
   // public Image background;
    public AudioSource gameMusic;
    public List<Sprite> backgroundSprites = new List<Sprite>();
    public float minTimeToLoad;
    public float minMusicVolume, maxMusicVolume;

    public void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)ScenesHolder.WORRY_SCENE, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(int unloadScene, int loadScene) 
    {

        StartCoroutine(FadeOutMusic(1));

        /* Sprite chosenSprite = null;
             if (PlayerPrefs.GetInt("LevelToLoad") == 0)
                 chosenSprite = backgroundSprites[0];
             else if (PlayerPrefs.GetInt("LevelToLoad") == 2)
                 chosenSprite = backgroundSprites[1];
             if (PlayerPrefs.GetInt("LevelToLoad") == 5)
                 chosenSprite = backgroundSprites[2];

             background.sprite = chosenSprite; */


        loadingScreen.SetActive(true);
       // cam.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(unloadScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress(loadScene));
        
    }



    float totalSceneProgress;
    float time;
    float timeProgress;
    IEnumerator GetSceneLoadProgress (int loadScene)
    {
        totalSceneProgress = 0;
        timeProgress = 0;
        while (time < minTimeToLoad)
        {     
            time += Time.deltaTime;
            timeProgress = time / minTimeToLoad;
            totalSceneProgress = (totalSceneProgress + timeProgress / scenesLoading.Count + 1);

            yield return null;
        }

        for(int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = (totalSceneProgress + timeProgress / scenesLoading.Count + 1);

                progressBar.fillAmount = totalSceneProgress;

                yield return null;
            }
        }

        loadingScreen.SetActive(false);
       // cam.SetActive(false);
        scenesLoading.Clear();
        if (loadScene == (int)ScenesHolder.BREATHING_SCENE)
        {
            GameObject.Find("AR Session Origin").GetComponent<ARCharacterSpawner>().StartUI();
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(loadScene));
    }

    public IEnumerator FadeOutMusic(float fadeTime)
    {
        float startVolume = gameMusic.volume;

        while (gameMusic.volume > minMusicVolume)
        {
            gameMusic.volume -= (startVolume - minMusicVolume) * Time.deltaTime / fadeTime;

            yield return null;
        }

        gameMusic.volume = minMusicVolume;
    }

    public void FadeIn ()
    {
        StartCoroutine(FadeInMusic(5));
    }

    public IEnumerator FadeInMusic(float fadeTime)
    {
        float startVolume = gameMusic.volume;       
        while (gameMusic.volume < maxMusicVolume)
        {
            gameMusic.volume += (maxMusicVolume - startVolume) * Time.deltaTime / fadeTime;

            yield return null;
        }

        gameMusic.volume = maxMusicVolume;
    }

    public void changeMusic (AudioClip audClip)
    {
        gameMusic.clip = audClip;
    }
}
