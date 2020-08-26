using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorryPlacer : MonoBehaviour
{

    Transform planeTrans;
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
        planeTrans = WorryPlaneManager.instance.worryPlane.transform;
        planeTrans.position = transform.position;
        planeTrans.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!planeTrans)
            return;

        planeTrans.position = transform.position;
        planeTrans.rotation = transform.rotation;
    }
}
