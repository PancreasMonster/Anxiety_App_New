using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public float speed;
    float spinAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spinAmount += speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler (0, 0, spinAmount);
    }
}
