using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void OnPlay()
    {
        StartCoroutine(ChangeScene("IntroScene01"));
    }

    public void OnCredits()
    {
        StartCoroutine(ChangeScene("CreditsScene01"));
    }

    public void OnBack()
    {
        StartCoroutine(ChangeScene("MenuScene01"));
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    IEnumerator ChangeScene(string scene)
    {

        yield return null;
        SceneManager.LoadScene(scene);
    }


}
