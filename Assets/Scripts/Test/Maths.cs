using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maths : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int j = 1;
        int size = 15;
        for (int i = 1; i <= size; i++)
        {
             j = 1;

            do
            {
                j++;
            }
            while (i % j != 0 && j <= size);

            if (j == i)
            {
                Debug.Log(i + " is a prime number");
            } else
            {
                Debug.Log(i + " is not a prime number");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
