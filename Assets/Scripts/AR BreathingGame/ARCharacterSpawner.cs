﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


[RequireComponent(typeof(ARRaycastManager))]
public class ARCharacterSpawner : MonoBehaviour
{
    public List<GameObject> character = new List<GameObject>();
    public GameObject breathingGameObject;
    public Transform arCam;
    public bool replacePossible = true;
    private GameObject spawnedObject, spawnedBreathGO;
    private ARRaycastManager arRaycastManager;
    private Vector2 touchPosition;
    public Button replaceButton;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Start()
    {
        replaceButton.onClick.AddListener(Replace);
        StartCoroutine(BreathCompleted());
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(character[PlayerPrefs.GetInt("HeroChosen")], hitPose.position, hitPose.rotation);
                spawnedBreathGO = Instantiate(breathingGameObject, hitPose.position, hitPose.rotation, spawnedObject.transform);
                spawnedObject.GetComponent<TestRotation>().target = arCam;
                spawnedBreathGO.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, .5f);
                spawnedBreathGO.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,-180,0);
                spawnedBreathGO.GetComponentInChildren<BreathingCursor>().anim = spawnedObject.GetComponent<Animator>();
                replacePossible = false;
                replaceButton.interactable = true;
                
            }
            else if(replacePossible)
            {
                spawnedObject.transform.position = hitPose.position;
                replacePossible = false;
            }
        }
    }

    public void Replace()
    {
        replacePossible = true;
    }

    IEnumerator BreathCompleted()
    {
        yield return new WaitForSeconds(10f);
        ScenesManager.instance.LoadGame((int)ScenesHolder.BREATHING_SCENE, (int)ScenesHolder.WORRY_SCENE);
    }
}
