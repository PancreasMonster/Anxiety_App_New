﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectSpawner : MonoBehaviour
{

    public GameObject objectToSpawn;
    public GameObject[] characters;
   
    private PlacementIndicator _placementIndicator;
    private string characterName;

   public  GameObject obj2;
 
    
    // Start is called before the first frame update
    void Start()
    {
        _placementIndicator = FindObjectOfType<PlacementIndicator>();
       characterName=PlayerPrefs.GetString("Name", "No Name");
       if (characterName == "Man")
       {    
    objectToSpawn=characters[0];
    
    objectToSpawn.transform.localScale=new Vector3(0.6f, 0.6f, 0.6f);
       }
      

       else
       {
        
           objectToSpawn=characters[1];
           objectToSpawn.transform.localScale=new Vector3(0.07f, 0.07f, 0.07f);
           
       }
     
       objectToSpawn.transform.localScale=new Vector3(0.6f, 0.6f, 0.6f);
      
       //PlayerPrefs.GetString("Name", "No Name");
    }

    // Update is called once per frame
    void Update()
    {
        //Get number of touches on  screen, should be greater than 0.
        //Get the first touch and if this is the first frame it has been touched. So doesnt spawn in later frames only in the first frame it is present in.
        if (obj2==null && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            
          PlaceObject();
           
           
        }
        
        if (obj2!=null && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            
           ReplaceObject();
           
        }
    }

    public void PlaceObject()
    {
        obj2 = Instantiate(objectToSpawn, _placementIndicator.transform.position, _placementIndicator.transform.rotation);
        obj2.transform.Rotate(0,180f,0, Space.Self);
        
    }

    public void ReplaceObject()
    {
        Destroy(obj2);
        obj2 = Instantiate(objectToSpawn, _placementIndicator.transform.position, _placementIndicator.transform.rotation);
        obj2.transform.Rotate(0,180f,0, Space.Self);
    }
}
