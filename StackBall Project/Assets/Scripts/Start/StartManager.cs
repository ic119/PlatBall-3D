using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    #region 변수목록
    private App app;
    #endregion

    private void Awake()
    {
        this.app = FindObjectOfType<App>();
    }

    private void Update()
    {
        TouchedScreen();
    }

    public void TouchedScreen()
    {
        if (Input.GetMouseButton(0))
        {
            this.app.ChangeScene(App.eSceneState.InGame);
        }
    }
}
