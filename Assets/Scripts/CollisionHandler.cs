using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float LoadDelay;
    [SerializeField] private AudioClip CrashSfx;
    [SerializeField] private AudioClip SuccesSfx;
    [SerializeField] private ParticleSystem SuccesParticles;
    [SerializeField] private ParticleSystem CrashParticles;
    
    private AudioSource audioSource;

    private bool isControllable = true;
    private bool isCollidable = true;

    [SerializeField] private TMP_Text deathCounterTxt;
    [SerializeField] private int deathCounter;

    private int scenePlus = 1;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        RespondToDebugKeys();
        deathCounterTxt.text = PlayerPrefs.GetInt("DeathCount").ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped Into A Friend");
                break;
            case "Finish":
                Debug.Log("Congrats");
                StartNextLevelSequence();
                
                break;
            case "Fuel":
                Debug.Log("Gluk Gluq Glk");
                break;
            default:
                Debug.Log("Bomboclat");
                StartCrashSequence();
                
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (other.gameObject.tag)
        {
            case "Ammo":
                StartCrashSequence();
                Debug.Log("Bomboclat");
                break;
            default:
                
    

                break;
        }
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
    private void StartCrashSequence()
    {
        
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        CrashParticles.Play();
        audioSource.PlayOneShot(CrashSfx);
        int Dcount = PlayerPrefs.GetInt("DeathCount") + 1;
        PlayerPrefs.SetInt("DeathCount", Dcount);
        Invoke("ReloadScene" ,LoadDelay);
    }
    private void StartNextLevelSequence()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        SuccesParticles.Play();
        audioSource.PlayOneShot(SuccesSfx);
        PlayerPrefs.SetInt("DeathCount", 0);
        Invoke("LoadNextScene" ,LoadDelay);
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        Debug.Log(nextScene);
        if (SceneManager.sceneCountInBuildSettings == nextScene)
        {
            nextScene = 1;
        }
        SceneManager.LoadScene(nextScene);
    }
}


