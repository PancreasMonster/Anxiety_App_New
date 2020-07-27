using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    bool firstTime = true;
    public GameObject characterHolder;
    public List<GameObject> models = new List<GameObject>();
    public int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = PlayerPrefs.GetInt("HeroChosen");
        AssignHero(currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignHero(int i)
    {
        if (i == currentIndex && !firstTime)
            return;
        if (characterHolder.transform.GetChild(0) != null)
            Destroy(characterHolder.transform.GetChild(0).gameObject);
        GameObject _Model = Instantiate(models[i], transform.position, Quaternion.identity, characterHolder.transform);
        _Model.transform.localPosition = Vector3.zero;
        PlayerPrefs.SetInt("HeroChosen", i);
        currentIndex = i;
        firstTime = false;
    }
}
