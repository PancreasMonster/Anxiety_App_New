using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDestroyPlacement : MonoBehaviour
{
    public List <GameObject> heroes = new List<GameObject>();
    public List<Color> colors = new List<Color>();
    GameObject hero;
    Animator anim;
    public Transform t;

    // Start is called before the first frame update
    void Start()
    {
        hero = Instantiate(heroes[PlayerPrefs.GetInt("HeroChosen")], transform.position, transform.rotation, transform);
        hero.transform.localPosition = new Vector3(0, 2.15f, 0);
        anim = hero.GetComponent<Animator>();
        StartCoroutine(WorryDestruction());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WorryDestruction()
    {
        hero.transform.Find("PowerUpParticles").GetComponent<ParticleSystemHandler>().powerUpParticles(colors[PlayerPrefs.GetInt("HeroChosen")]);
        yield return new WaitForSeconds(1.5f);        
        hero.transform.Find("ChargeUpParticles").GetComponent<ParticleSystemHandler>().powerUpParticles(colors[PlayerPrefs.GetInt("HeroChosen")]);
        yield return new WaitForSeconds(.33f);
        anim.SetBool("Fire", true);
        yield return new WaitForSeconds(2f);
        hero.GetComponent<LaserFire>().FireLasers();
       
    }
}
