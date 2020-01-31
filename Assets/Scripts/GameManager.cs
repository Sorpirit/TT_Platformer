using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject Player;

    public GameObject LatestChekPoint
    {
        get => latestChekPoint;
        set{
            latestChekPoint = value;
            TimerAnimator.SetTrigger("Lerp");
            if(curentLerp < lerpsTimers.Length){
                lerpsTimers[curentLerp].text = SetTime();
                curentLerp++;
            } 
        }
    }
    

    public ParticleSystem playerSpawnEffect;

    [SerializeField] private Image image;
    [SerializeField] private float disolveSpeed;

    [SerializeField] private TMP_Text mainTimer;
    [SerializeField] private Animator TimerAnimator;
    [SerializeField] private TMP_Text[] lerpsTimers;
    private int curentLerp;

    [SerializeField] private GameObject latestChekPoint;
    private Material camMat;
    private float disolveAmount;
    private bool playerDead;
    private float TimerFromStart;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(this);
        }

        //camMat = image.material;
    }

    private void Update() {
        TimerFromStart += Time.deltaTime;
        mainTimer.text = SetTime();
    }

    public void OnPlayerDead(){
        if(!playerDead)StartCoroutine(DisolveAnim());
    }

    IEnumerator DisolveAnim(){
        playerDead = true;
        disolveAmount = 0;

        while(disolveAmount < 1){
            disolveAmount += Time.deltaTime * disolveSpeed;
            //camMat.SetFloat("_disolveAmount",disolveAmount);
            yield return null;
        }

        Player.transform.position = latestChekPoint.transform.position;

        while(disolveAmount > 0){
            disolveAmount -= Time.deltaTime * disolveSpeed;
            //camMat.SetFloat("_disolveAmount",disolveAmount);
            yield return null;
        }

        
        playerDead =  false;
        playerSpawnEffect.transform.position = latestChekPoint.transform.position;
        playerSpawnEffect.Play();
    }

    private string SetTime(){
        int miliseconds = (int) Mathf.Floor((TimerFromStart-Mathf.Floor(TimerFromStart)) * 100f);
        int seconds = (int) Mathf.Floor(TimerFromStart);
        int minutes = seconds/60;
        seconds -= 60 * minutes;

        string miliPart = miliseconds < 10 ? "0" + miliseconds : miliseconds + "";
        string secPart = seconds < 10 ? "0" + seconds : seconds + "";
        string minPart = minutes < 10 ? "0" + minutes : minutes + "";

        return minPart + ":" + secPart + ":" +  miliPart;
    }
}
