using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textChange : MonoBehaviour
{

    public Text changingText;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.GetString("Name", "No Name");
        changingText.text=PlayerPrefs.GetString("Name", "No Name");
        //Debug.Log(changingText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
