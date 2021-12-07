using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    public enum eSceneState
    {
        App, Start, InGame
    }


    public eSceneState sceneState;

    private void Start()
    {
        var app = FindObjectOfType<App>();
        if (app.GetInstanceID() != this.GetInstanceID())
        {
            Destroy(app.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        this.ChangeScene(eSceneState.Start);
    }

    public void ChangeScene(eSceneState sceneState)
    {
        switch(sceneState)
        {
            case eSceneState.App:
                {
                    SceneManager.LoadScene(eSceneState.App.ToString());
                    break;
                }

            case eSceneState.Start:
                {
                    SceneManager.LoadScene(eSceneState.Start.ToString());
                }
                  break;

            case eSceneState.InGame:
                {
                    SceneManager.LoadScene(eSceneState.InGame.ToString());
                }
                break;
        }
    }
}
