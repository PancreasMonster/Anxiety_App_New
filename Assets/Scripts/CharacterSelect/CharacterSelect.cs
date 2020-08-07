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
    public int currentIndex;
    public DescriptionAssign descriptionAssign;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("HeroChosen", 0);
        AssignHero(currentIndex);        
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
        GameObject _Model = Instantiate(models[i], transform.position, models[i].transform.rotation, characterHolder.transform);
        _Model.transform.localPosition = Vector3.zero;
        PlayerPrefs.SetInt("HeroChosen", i);
        currentIndex = i;
        firstTime = false;
        descriptionAssign.AssignDescription(descriptionObjects[i]);
    }
}
