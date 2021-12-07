using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    #region 변수목록
    [SerializeField]
    private PartsManager[] platformParts = null;
    #endregion

    public void DeleteAllPlatform()
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (PartsManager parts in platformParts)
        {
            parts.Breaking();
        }
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
