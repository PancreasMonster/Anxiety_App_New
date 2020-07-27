using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoader : MonoBehaviour
{

    
    public void LoadScene(int level)
    {
        ScenesManager.instance.LoadGame((int)ScenesHolder.WORRY_SCENE, (int)ScenesHolder.BREATHING_SCENE);
    }
}