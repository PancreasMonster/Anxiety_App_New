using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneLoader : MonoBehaviour
{

    
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
        Debug.Log("Loaded");
    }
}