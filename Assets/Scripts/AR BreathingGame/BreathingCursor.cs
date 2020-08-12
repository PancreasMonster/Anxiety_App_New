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
    public float animationSpeed = 3;
    public float animationSpeedDescreaseAmount = .5f;
    public float animationSpeedChangeAmount = .25f;
    public List<float> speedDevisor = new List<float>();
    public Button breatheButton;

    public GameObject WellDoneCanvas;

    float i;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GetComponent<RectTransform>();
        anim.SetFloat("AnimationSpeed", animationSpeed);
        i = speedDevisor[PlayerPrefs.GetInt("HeroChosen")];
        animationSpeed /= i;
        animationSpeedDescreaseAmount /= i;

        
    }

    // Update is called once per frame
    void Update()
    {
        CursorMovement();
        //Breath();
    }


    float time;
    void CursorMovement ()
    {
        if (progressBarFillAmount < 1)
        {
            time += Time.deltaTime * speedMultiplier;
            cursor.localPosition = new Vector2(Mathf.Sin(time) * sineRange, cursor.localPosition.y); //controls the movement of the cursor
            cursorValue = Mathf.Sin(time) * 100; // sine wave that moves between -100 and 100, used to find the location of the cursor relative to the centre of the breathbar when the user taps the screen
        }
    }

    public void Breath()
    {
        
        if (canBreathe && progressBarFillAmount < 1)
        {
            if(Mathf.Abs(cursorValue) < 20)
            {
                progressBarFillAmount += ((float)15/100);
                StartCoroutine(FillProgressBar());
            }
            else if (Mathf.Abs(cursorValue) < 50)
            {
                progressBarFillAmount += ((float)10 / 100);
                StartCoroutine(FillProgressBar());
            }
            else if (Mathf.Abs(cursorValue) > 50)
            {
                progressBarFillAmount += ((float)5 / 100);
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
            animationSpeed -= animationSpeedDescreaseAmount;
            anim.SetFloat("AnimationSpeed", animationSpeed);
            speedMultiplier -= .2f;
        }
        while (progressBar.fillAmount < progressBarFillAmount)
        {
            progressBar.fillAmount += startFillAmount * Time.deltaTime /  progressBarFillAmountTime;

            yield return null;
        }

        if(progressBar.fillAmount == 1)
        {
            StartCoroutine(BreathCompleted());
        }

        breatheButton.interactable = true;
        canBreathe = true;
    }

    IEnumerator BreathCompleted ()
    {
        anim.SetBool("ThumbsUp", true);
        Instantiate(WellDoneCanvas, WellDoneCanvas.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(6f);
        ScenesManager.instance.LoadGame((int)ScenesHolder.BREATHING_SCENE, (int)ScenesHolder.WORRY_SCENE);
    }
}
