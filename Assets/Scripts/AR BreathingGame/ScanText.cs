using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScanText : MonoBehaviour
{
    public string origText;
    string text;
    TMPro.TextMeshProUGUI UI;
    int i = 0;
    float t = 0;
    float g = 1;

    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<TextMeshProUGUI>();
        UI.text = origText;
        text = origText;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t > g)
        {
            g += 1;
            text += ".";
            UI.text = text;
            if(g == 4)
            {
                t = 0;
                g = 1;
                text = origText;
            }
        }
    }
}
