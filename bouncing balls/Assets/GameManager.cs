using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public  int borderHitCount = 0;
    public  bool gameStart = false;
    public  int score = 0;
    public Bloom bloom; 
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        gameStart = true;
        GetComponent<PostProcessVolume>().profile.TryGetSettings(out bloom);
    }
    

    // on run funtion

    public void OnBallGrab()
    {
        score += 1;
    }
    public void OnBorderHit()
    {

        if (borderHitCount < 3)
        {
            bloom.intensity.value = 50f;
            StartCoroutine(FlashEffect(bloom));
        }
        else
        {
            bloom.intensity.value = 50f;

            StartCoroutine(FlashEffect(bloom));
        }

    }

    IEnumerator FlashEffect(Bloom bloom)
    {

        while (bloom.intensity.value>6f)
        {
            bloom.intensity.value  = Mathf.Lerp(bloom.intensity.value,5f,Time.deltaTime);
            
            yield return null;
        }
        bloom.intensity.value = 5f;
    }
}
