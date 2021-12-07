using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInGame : MonoBehaviour
{
    #region UI Object 변수목록
    public GameObject uiStage;
    public GameObject uiGameOver;
    public GameObject uiFinish;
    #endregion

    #region UIStage 변수목록
    public TextMeshProUGUI currentStageNum;
    #endregion

    #region UIGameOver 변수목록
    public TextMeshProUGUI gameOverStageNum;
    public Button main_Btn_GAMEOVER;
    public Button restart_Btn_GAMEOVER;
    #endregion

    #region UIFinish 변수목록
    public TextMeshProUGUI finishStageNum;
    public Button mainBtn_FINISH;
    public Button nextStage_Btn_FINISH;
    private App app;
    #endregion

    #region 기타 변수목록
    private BallManager ballManager;
    #endregion

    private void Awake()
    {
        this.ballManager = FindObjectOfType<BallManager>();
        this.app = FindObjectOfType<App>();
    }

    private void Start()
    {
        this.currentStageNum.text = FindObjectOfType<StageManager>().stageCount.ToString();

    }

    private void Update()
    {
        UIManager();
    }

    #region UI 초기화 메서드
    public void UIInit()
    {
        this.uiStage.SetActive(false);
        this.uiGameOver.SetActive(false);
        this.uiFinish.SetActive(false);
    }
    #endregion

    #region UI 반응처리 메서드
    public void UIManager()
    {
        if (this.ballManager.ballState == BallManager.BallState.Ready)
        {
            UIInit();
            this.uiStage.SetActive(true);
        }

        if (this.ballManager.ballState == BallManager.BallState.Play)
        {
            UIInit();
            this.uiStage.SetActive(true);
        }

        #region 게임오버 시 UI처리
        if (this.ballManager.ballState == BallManager.BallState.Death)
        {
            UIInit();
            this.uiGameOver.SetActive(true);
            this.gameOverStageNum.text = FindObjectOfType<StageManager>().stageCount.ToString();
        }
        #endregion

        #region 게임승리 시 UI처리
        if (this.ballManager.ballState == BallManager.BallState.Finish)
        {
            UIInit();
            this.uiFinish.SetActive(true);
            this.finishStageNum.text = FindObjectOfType<StageManager>().stageCount.ToString();
        }
        #endregion
    }
    #endregion

    public void OnButtonClick_Main()
    {
        this.app.ChangeScene(App.eSceneState.Start);
    }

    public void OnButtonClick_NextStage()
    {
        this.app.ChangeScene(App.eSceneState.InGame);
    }
    
    public void OnButtonClick_ReStart()
    {
        this.app.ChangeScene(App.eSceneState.InGame);
    }
}
