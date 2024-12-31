using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadSceneDilay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        audioSource.PlayOneShot(success, 0.6f);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadSceneDilay);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash, 0.1f);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadSceneDilay);
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
