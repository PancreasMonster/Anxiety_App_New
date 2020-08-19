using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    bool firstTime = true;
    public GameObject characterHolder;
    public List<GameObject> models = new List<GameObject>();
    public List<Button> buttons = new List<Button>();
    public List<CharacterDescriptionScriptableObject> descriptionObjects = new List<CharacterDescriptionScriptableObject>();
    public List<Vector3> initialPositions = new List<Vector3>();
    public List<Vector3> scales = new List<Vector3>();
    public int currentIndex;
    public DescriptionAssign descriptionAssign;
    public Camera cam;
    public Transform cameraPlacement;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("HeroChosen", 0);
        AssignHero(currentIndex);
        //ScreenshotHandler.instance.AssignCamera(cam);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignSelectedGameObject()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }

    public void AssignHero(int i)
    {
        if (i == currentIndex && !firstTime)
            return;
        if (characterHolder.transform.GetChild(0) != null)
            Destroy(characterHolder.transform.GetChild(0).gameObject);
        GameObject _Model = Instantiate(models[i], transform.position, Quaternion.identity, characterHolder.transform);
        _Model.transform.localRotation = Quaternion.Euler(0, 180, 0);
        _Model.transform.localPosition = initialPositions[i];
        _Model.transform.localScale = scales[i];
        PlayerPrefs.SetInt("HeroChosen", i);
        currentIndex = i;
        firstTime = false;
        descriptionAssign.AssignDescription(descriptionObjects[i]);
    }

    public void ScreenshotTake()
    {
        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
    }

    public void MoveCamera()
    {
        cam.orthographic = false;
        cam.transform.position = cameraPlacement.position;
        cam.transform.rotation = cameraPlacement.rotation;
    }

}
