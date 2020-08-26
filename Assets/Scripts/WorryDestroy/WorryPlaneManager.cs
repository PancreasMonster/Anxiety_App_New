using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorryPlaneManager : MonoBehaviour
{

    public static WorryPlaneManager instance;
    public GameObject worryPlane;
    [SerializeField] private Material mat;

    private float dissolveAmount;
    private bool isDissolving;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_DissolveAmount", dissolveAmount);
    }

    // Update is called once per frame
    void Update()
    {

        if (isDissolving)
        {
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
