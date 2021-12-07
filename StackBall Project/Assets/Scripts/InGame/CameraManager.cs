using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region 카메라 변수 목록
    [SerializeField]
    private Vector3 cameraPos;
    [SerializeField]
    private Transform ballPos;
    [SerializeField]
    private Transform finishPlatform;

    private float cameraDistance = 5.0f;
    #endregion

    private void Awake()
    {
        this.ballPos = FindObjectOfType<BallManager>().transform;
    }

    private void Update()
    {
        TakeBall();
    }

    #region Ball을 비추는 메서드
    public void TakeBall()
    {
        if(this.finishPlatform == null)
        {
            this.finishPlatform = GameObject.Find("FinishPlatform(Clone)").transform;
            // 게임시작 시 생성되는 Finish플랫폼 클론의 위치를 저장;
        }

        if(this.gameObject.transform.position.y > this.ballPos.transform.position.y && this.gameObject.transform.position.y > this.finishPlatform.transform.position.y)
        {
            this.cameraPos = new Vector3(this.gameObject.transform.position.x, this.ballPos.transform.position.y, this.gameObject.transform.position.z);
        }
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.cameraPos.y, -this.cameraDistance);
    }
    #endregion
}

