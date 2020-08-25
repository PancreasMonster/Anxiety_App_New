using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDestroyPlacement : MonoBehaviour
{
    public List <GameObject> heroes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject hero = Instantiate(heroes[PlayerPrefs.GetInt("HeroChosen")], transform.position, transform.rotation, transform);
        hero.transform.localPosition = new Vector3(0, 2.15f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
