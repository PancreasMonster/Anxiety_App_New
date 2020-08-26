using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnTest : MonoBehaviour
{
    public List<GameObject> character = new List<GameObject>();
    public GameObject breathingGameObject;
    public Transform arCam;
    public bool replacePossible = true;
    private GameObject spawnedObject, spawnedBreathGO;
    private Vector2 touchPosition;
    public List<Color> colors = new List<Color>();
    public List<Vector3> size = new List<Vector3>();

    

    // Start is called before the first frame update
    void Awake()
    {
       
    }

    void Start()
    {

    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
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
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(character[0], transform.position, transform.rotation);
                spawnedBreathGO = Instantiate(breathingGameObject, transform.position, transform.rotation);
                spawnedObject.GetComponent<TestRotation>().target = arCam;
                //spawnedBreathGO.GetComponent<RectTransform>().localPosition = new Vector3 (0, 0, .5f);
                // spawnedBreathGO.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0,-180,0);
                spawnedObject.transform.localScale = size[PlayerPrefs.GetInt("HeroChosen")];
                spawnedBreathGO.GetComponentInChildren<BreathingCursor>().anim = spawnedObject.GetComponent<Animator>();
                spawnedBreathGO.GetComponentInChildren<BreathingCursor>().character = spawnedObject;
                spawnedBreathGO.GetComponentInChildren<BreathingCursor>().col = colors[PlayerPrefs.GetInt("HeroChosen")];
                spawnedBreathGO.GetComponentInChildren<BreathingCursor>().charging = spawnedObject.transform.Find("PowerUpParticles").GetComponent<ParticleSystemHandler>();
                spawnedObject.transform.Find("ParticleSystemLanding").GetComponent<ParticleSystemHandler>().Landing(colors[PlayerPrefs.GetInt("HeroChosen")]);
                replacePossible = false;

            }
            else
            {
                //spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}
