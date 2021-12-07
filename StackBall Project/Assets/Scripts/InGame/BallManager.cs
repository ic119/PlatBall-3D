using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Ball 변수 목록
    private Rigidbody ballRb;
    [SerializeField]
    private bool isClick;
    [SerializeField]
    private bool isBreakPlatform;
    [SerializeField]
    private float ballSpeed = 1500f;
    private float ballSpeedLimit = 5.0f;
    [SerializeField]
    private float ballBounceSpeed = 0f;
    #endregion

    #region Ball 상태 열거형 변수
    public enum BallState
    {
        Ready, Play, Death, Finish
    }
    #endregion

    #region 기타 변수 목록
    private App app;
    #endregion

    private int curBreakCount;
    // 현재 파괴한 platform의 수
    private int allPlatformCount;
    // 모든 platform의 수

    public BallState ballState = BallState.Ready;
    // Ball의 초기 상태를 준비(Ready)상태로 초기화

    private void Awake()
    {
        this.ballRb = this.gameObject.GetComponent<Rigidbody>();
        // 매 시작할 때마다 현재 파괴한 platform의 수를 0으로 초기화
        this.app = FindObjectOfType<App>();
        this.ballState = BallState.Play;
    }

    private void Start()
    {
        allPlatformCount = FindObjectsOfType<PlatformManager>().Length;
    }

    private void Update()
    {
        if (ballState == BallState.Play)
        {
            ClickManager();
        }

        if (this.ballState == BallState.Finish)
        {
            if(Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<StageManager>().NextStage();
            }
        }
    }


    private void FixedUpdate()
    {
        BallMove();
    }

    #region Ball 조작 관련 메서드
    private void BallMove()
    {
        if(ballState == BallState.Play)
        {
            if (Input.GetMouseButtonDown(0) && isClick == true)
            {
                this.ballRb.velocity = new Vector3(0, -ballSpeed * Time.deltaTime, 0);
                // 아래로 내려가야하므로 음수 y축 값을 부여
            }
        }

        if (this.ballRb.velocity.y > ballSpeedLimit)
        {
            this.ballRb.velocity = new Vector3(ballRb.velocity.x, ballSpeedLimit, ballRb.velocity.z);
        }
    }
    #endregion

    public void ClickManager()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.isClick = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.isClick = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isClick)
        {
            this.ballRb.velocity = new Vector3(0, ballBounceSpeed * Time.deltaTime, 0);
        }
        else
        {
            if(isBreakPlatform)
            {
                if(collision.gameObject.tag == "SafeCube" || collision.gameObject.tag == "DeathCube")
                {
                    collision.transform.parent.GetComponent<PlatformManager>().DeleteAllPlatform();
                }
            }
            else
            {
                if (collision.gameObject.tag == "SafeCube")
                {
                    collision.transform.parent.GetComponent<PlatformManager>().DeleteAllPlatform();
                }

                if (collision.gameObject.tag == "DeathCube")
                {
                    this.ballRb.isKinematic = true;
                    this.ballState = BallState.Death;
                }
            }
        }

        if (collision.gameObject.CompareTag("Finish") && this.ballState == BallState.Play)
        {
            this.ballState = BallState.Finish;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(!isClick || collision.gameObject.CompareTag("Finish"))
        {
            ballRb.velocity = new Vector3(0, ballBounceSpeed * Time.deltaTime, 0);
        }
    }
}
