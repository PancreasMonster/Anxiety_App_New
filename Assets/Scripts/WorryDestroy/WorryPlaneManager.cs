using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorryPlaneManager : MonoBehaviour
{

    public static WorryPlaneManager instance;
    public GameObject worryPlane;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
