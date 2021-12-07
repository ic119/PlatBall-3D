using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    #region 플랫폼 관련 변수목록
    [SerializeField]
    private GameObject[] allPlatformArr;
    [SerializeField]
    private GameObject[] selectedPlatformArr = new GameObject[4];
    [SerializeField]
    private GameObject finishPlatformPrefab;
    #endregion

    #region Stage관련 변수목록
    private GameObject inGamePlatform;
    private GameObject finishPlatform;
    public int stageCount = 1;
    // stage의 수를 의미
    private int platformAddCount = 7;
    // stage의 증가에 따라 추가될 플랫폼의 수를 의미

    private float i = 0;
    private float rotateSpeed = 10.0f;
    #endregion

    #region 기타 변수목록
    private App app;
    #endregion
    private void Awake()
    {
        StageController();
        this.app = FindObjectOfType<App>();
    }

    #region Stage 관련 메서드
    public void StageController()
    {
        this.stageCount = PlayerPrefs.GetInt("Stage", 1);
        // 스테이지를 플레이어데이터에 저장
        if (this.stageCount > 9)
            // 스테이지 카운터가 9보다 클 경우
        {
            platformAddCount = 0;
            // 더해질 플랫폼 카운트의 수를 0으로 초기화
        }

        PlatformSetting();

        #region 플랫폼 인스턴스 처리
        for (i = 0; i > -stageCount - platformAddCount; i -= 0.5f)
        {
            if (this.stageCount <= 10)
            // Stage가 10이하일 경우
            {
                this.inGamePlatform = Instantiate(this.selectedPlatformArr[Random.Range(0, 4)]);
                // 생성한 Platform 프리팹의 0~4번쨰를 랜덤하게 생성한다.
            }
            if (this.stageCount > 10 && this.stageCount <= 20)
            // Stage가 10보다 크고 20이하일 경우
            {
                this.inGamePlatform = Instantiate(this.selectedPlatformArr[Random.Range(1, 4)]);
            }
            if (this.stageCount > 20 && this.stageCount <= 30)
            // Stage가 21보다 크고 30이하일 경우
            {
                this.inGamePlatform = Instantiate(this.selectedPlatformArr[Random.Range(2, 4)]);
            }
            if (this.stageCount > 30 && this.stageCount <= 40)
            // Stage가 31보다 크고 40이하일 경우
            {
                this.inGamePlatform = Instantiate(this.selectedPlatformArr[Random.Range(3, 4)]);
            }

            inGamePlatform.transform.position = new Vector3(0, i, 0);
            inGamePlatform.transform.eulerAngles = new Vector3(0, i * rotateSpeed, 0);
            // 생성된 Platform의 위치와 회전을 담당

            if (Mathf.Abs(i) >= stageCount * 0.3f && Mathf.Abs(i) <= stageCount * 0.6f)
            {
                inGamePlatform.transform.eulerAngles += Vector3.up * 180;
            }

            inGamePlatform.transform.parent = FindObjectOfType<PartsManager>().transform;
        }

        finishPlatform = Instantiate(finishPlatformPrefab);
        finishPlatform.transform.position = new Vector3(0, i - 0.5f, 0);
        // 생성되는 Platform의 y축을 의미하는 i에 -0.5를 한다.
        // 하단으로 생성되므로 음수를 계산해줘야한다.
        #endregion
    }
    #endregion

    #region 플랫폼 세팅 규칙관련
    public void PlatformSetting()
        // 플랫폼을 세팅하는 규칙 처리
    {
        int randomSetNum = Random.Range(0, 5);
        switch(randomSetNum)
        // 랜덤변수인 randomSetNum에 따라서 Platform 오브젝트 중 해당 case와 i의 맞는 Platform을 생성하고, 3개씩 더 생성되도록 한다.
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    selectedPlatformArr[i] = allPlatformArr[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    selectedPlatformArr[i] = allPlatformArr[i + 3];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    selectedPlatformArr[i] = allPlatformArr[i + 6];
                }
                break;
            case 3 :
                for (int i = 0; i < 4; i++)
                {
                    selectedPlatformArr[i] = allPlatformArr[i + 9];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    selectedPlatformArr[i] = allPlatformArr[i + 12];
                }
                break;
        }
    }
    #endregion

    #region Stage 증가 후 유저 데이터 저장 처리
    public void NextStage()
    {
        PlayerPrefs.SetInt("Stage", PlayerPrefs.GetInt("Stage") + 1);
        this.app.ChangeScene(App.eSceneState.InGame);
    }
    #endregion

    
}
