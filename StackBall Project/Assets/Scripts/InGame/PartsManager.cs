using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour
{
    #region 변수 목록
    private Rigidbody partsRb;
    private MeshRenderer partsRenderer;
    public static PlatformManager platformManager;
    private Collider partsCollider;
    private float partsMovementSpeed = 1.5f;
    #endregion

    private void Awake()
    {
        this.partsRb = this.gameObject.GetComponent<Rigidbody>();
        this.partsRenderer = this.gameObject.GetComponent<MeshRenderer>();
        this.partsCollider = this.gameObject.GetComponent<Collider>();
        platformManager = transform.parent.GetComponent<PlatformManager>();
    }

    #region Parts가 깨질 때의 효과 처리
    public void Breaking()
    {
        this.partsRb.isKinematic = false;
        this.partsCollider.enabled = false;
        Vector3 powerStarPoint = transform.parent.position;
        float parentPosX = transform.parent.position.x;
        float posX = this.partsRenderer.bounds.center.x;

        Vector3 subDir = (parentPosX - posX < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * partsMovementSpeed + subDir).normalized;

        float randomPower = Random.Range(30, 45);
        float randomTorque = Random.Range(120, 190);

        // 조각이 날아가는 효과
        this.partsRb.AddForceAtPosition(dir * randomPower, powerStarPoint, ForceMode.Impulse);
        this.partsRb.AddTorque(Vector3.left * randomTorque);
        this.partsRb.velocity = Vector3.down;
    }
    #endregion

    public void DeletingParts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).SetParent(null);
            i--;
        }
    }
}
