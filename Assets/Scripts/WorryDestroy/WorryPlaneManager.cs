using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorryPlaneManager : MonoBehaviour
{

    public static WorryPlaneManager instance;
    public GameObject worryPlane;
    public ParticleSystemHandler PSH;
    [SerializeField] private Material mat;

    private float dissolveAmount;
    private bool isDissolving = false;
    private bool psPlay = false;
    public List<Color> colors = new List<Color>();
    public bool input = false;
    private AudioSource aud;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_DissolveAmount", dissolveAmount);
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.T) && input)
        {
            isDissolving = true;
        }

        if (isDissolving)
        {
            if(!psPlay)
            {
                psPlay = true;
                PSH.powerUpParticles(colors[PlayerPrefs.GetInt("HeroChosen")]);
                aud.Play();
            }

            if (dissolveAmount < 1)
            {
                dissolveAmount = Mathf.Lerp(dissolveAmount, 1, Time.deltaTime);
                mat.SetFloat("_DissolveAmount", dissolveAmount);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Dissolve ()
    {
        isDissolving = true;
    }
}
