using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingCursor : MonoBehaviour
{
    RectTransform cursor;
    public float sineRange = 300;
    public float speedMultiplier = 1;

    public bool canBreathe = true;
    public float cursorValue;

    public Image progressBar;
    public float progressBarFillAmount;
    public float progressBarFillAmountTime = 2;

    public Animator anim;
    public GameObject character;
    public ParticleSystemHandler charging;
    public List<float> animationSpeeds = new List<float>();
    public List<float> animationSpeedDescreaseAmounts = new List<float>();
    public float animationSpeedChangeAmount = .25f;

    public Button breatheButton;
    public RectTransform breathButtonRect;

    public GameObject WellDoneCanvas;
    public RectTransform ps;
    float progressBarHeight;

    int i;
    bool breathingEnded = false;
    public Color col;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GetComponent<RectTransform>();
        i = PlayerPrefs.GetInt("HeroChosen");      
        anim.SetFloat("AnimationSpeed", animationSpeeds[i]);
        progressBarHeight = progressBar.rectTransform.rect.height;
        //breathButtonRect = breatheButton.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CursorMovement();
        //Breath();

        if (progressBar.fillAmount == 1 && !breathingEnded)
        {
            StartCoroutine(Rise());
            breathingEnded = true;
        }

        //ps.localPosition = new Vector2(500, (900 * progressBar.fillAmount * 2) - (900));
    }


    float time;
    void CursorMovement ()
    {
        if (progressBarFillAmount < 1)
        {
            time += Time.deltaTime * speedMultiplier;
            cursor.localPosition = new Vector2(Mathf.Sin(time) * sineRange, cursor.localPosition.y); //controls the movement of the cursor
            cursorValue = Mathf.Sin(time) * 100; // sine wave that moves between -100 and 100, used to find the location of the cursor relative to the centre of the breathbar when the user taps the screen
            breathButtonRect.localScale = new Vector3 (1 - Mathf.Abs((Mathf.Sin(time) * .5f)), 1 - Mathf.Abs((Mathf.Sin(time) * .5f)), 1);
        }
    }

    public void Breath()
    {
        
        if (canBreathe && progressBarFillAmount < 1)
        {
            if(Mathf.Abs(cursorValue) < 20)
            {
                progressBarFillAmount += ((float)25/100);
                StartCoroutine(FillProgressBar());
            }
            else if (Mathf.Abs(cursorValue) < 50)
            {
                progressBarFillAmount += ((float)25 / 100);
                StartCoroutine(FillProgressBar());
            }
            else if (Mathf.Abs(cursorValue) > 50)
            {
                progressBarFillAmount += ((float)25 / 100);
                StartCoroutine(FillProgressBar());
            }
            
        }
 
    }


    IEnumerator FillProgressBar ()
    {
        canBreathe = false;
        breatheButton.interactable = false;
        float startFillAmount = progressBarFillAmount - progressBar.fillAmount;

        if(progressBarFillAmount >= animationSpeedChangeAmount)
        {
            animationSpeedChangeAmount += .25f;
            animationSpeeds[i] -= animationSpeedDescreaseAmounts[i];
            anim.SetFloat("AnimationSpeed", animationSpeeds[i]);
            speedMultiplier -= .2f;
        }
        while (progressBar.fillAmount < progressBarFillAmount)
        {
            progressBar.fillAmount += startFillAmount * Time.deltaTime /  progressBarFillAmountTime;

            yield return null;
        }

        

        breatheButton.interactable = true;
        canBreathe = true;
    }

    IEnumerator Rise()
    {
        Instantiate(WellDoneCanvas, WellDoneCanvas.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        anim.SetBool("Hover", true);
        float startAmount = 0;
        Vector3 origPos = character.transform.position;
        while (startAmount < .9f)
        {
            float t = Time.deltaTime * 2.5f;
            startAmount += t;
            origPos = new Vector3(origPos.x, origPos.y + t, origPos.z);
            character.transform.position = origPos;
            yield return null;
        }
        yield return new WaitForSeconds(8f);
        ScenesManager.instance.LoadGame((int)ScenesHolder.BREATHING_SCENE, (int)ScenesHolder.WORRY_DESTRUCTION);
        charging.powerUpParticles(col);
    }
}
