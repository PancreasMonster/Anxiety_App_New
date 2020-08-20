using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemHandler : MonoBehaviour
{

    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void powerUpParticles(Color col)
    {
        foreach(ParticleSystem ps in particleSystems)
        {
            var particleSys = ps.main;
            particleSys.startColor = col;
            ps.Play();
        }
    }

    public void Landing(Color col)
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            var particleSys = ps.main;
            particleSys.startColor = col;
            ps.Play();
        }
        Destroy(this.gameObject, 4);
    }



}
