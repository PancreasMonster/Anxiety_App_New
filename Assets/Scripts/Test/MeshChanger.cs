using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    public GameObject g;
    public Mesh m;
    public ParticleSystem ps;
    SkinnedMeshRenderer smr;
    

    // Start is called before the first frame update
    void Start()
    {
        smr = g.GetComponent<SkinnedMeshRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Mesh baked = new Mesh();
        smr.BakeMesh(baked);
        var psRenderer = ps.GetComponent<ParticleSystemRenderer>();
        psRenderer.mesh = baked;
    }
}
