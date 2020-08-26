using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    public Transform leftHand, rightHand;
    public Transform target;
    public List<GameObject> lasers = new List<GameObject>();
    public GameObject laserPrefab;
    public float speed;
    public Color col;
    bool fired = false;
    bool lasersHit = false;
    public GameObject WellDoneCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(!target)
        target = WorryPlaneManager.instance.worryPlane.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fired)
            return;

        if (lasers.Count > 0)
        {
            foreach (GameObject l in lasers)
            {
                l.transform.LookAt(target);
                l.transform.Translate(l.transform.forward * speed * Time.deltaTime);

                if (Vector3.Distance(l.transform.position, target.position) < .5f)
                {
                    GameObject d = l;
                    lasers.Remove(d);
                    Destroy(d);
                }
            }
        }

        if(lasers.Count == 0 && !lasersHit)
        {
            lasersHit = true;
            WorryPlaneManager.instance.Dissolve();
            StartCoroutine(EndScene());
        }
    }

    public void FireLasers()
    {
        GameObject leftLaser = Instantiate(laserPrefab, leftHand.position, Quaternion.identity);
        leftLaser.GetComponent<ParticleSystemHandler>().powerUpParticles(col);
        lasers.Add(leftLaser);
        GameObject rightLaser = Instantiate(laserPrefab, rightHand.position, Quaternion.identity);
        lasers.Add(rightLaser);
        rightLaser.GetComponent<ParticleSystemHandler>().powerUpParticles(col);
        fired = true;
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2.5f);
        Instantiate(WellDoneCanvas, WellDoneCanvas.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        ScenesManager.instance.LoadGame((int)ScenesHolder.WORRY_DESTRUCTION, (int)ScenesHolder.WORRY_SCENE);
    }
}
