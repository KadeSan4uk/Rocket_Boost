using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadSceneDilay = 1.5f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool IsControlable = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsControlable) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You are Starting Platform.");
                break;
            case "Finish":
                StartSuccessSequense();
                break;
            case "Trash":
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequense()
    {
        IsControlable = false;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX, 0.6f);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadSceneDilay);
        IsControlable = true;
    }

    void StartCrashSequence()
    {
        IsControlable = false;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX, 0.1f);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadSceneDilay);
        IsControlable = true;
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
