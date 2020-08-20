using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingToHoverTest : MonoBehaviour
{

    public Animator anim;
    public GameObject character;
    public float riseAmount;
    public ParticleSystemHandler PSH;
    public Color col;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rise());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Rise()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("Hover", true);
        float startAmount = 0;
        Vector3 origPos = character.transform.position;
        while (startAmount < riseAmount)
        {
            float t = Time.deltaTime * 2.5f;
            startAmount += t;
            origPos = new Vector3(origPos.x, origPos.y + t, origPos.z);
            character.transform.position = origPos;
            yield return null;
        }
        PSH.powerUpParticles(col);
    }
}
