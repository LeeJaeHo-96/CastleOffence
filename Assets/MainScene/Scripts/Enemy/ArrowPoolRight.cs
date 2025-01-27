using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolRight : MonoBehaviour
{
    [SerializeField] GameObject arrowPoint;
    public GameObject arrowPrefab;
    public int poolSize = 10;

    public List<GameObject> arrowPool;

    private void Start()
    {
        // ȭ�� Ǯ ����
        arrowPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.identity);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    /// <summary>
    /// ȭ�� �������� �޼���
    /// </summary>
    /// <returns></returns>
    public GameObject GetArrow()
    {
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true);
                return arrow;
            }

        }

        // ��� ȭ���� ��� ���̸� ���� ����
        GameObject newArrow = Instantiate(arrowPrefab, arrowPoint.transform.position, Quaternion.identity);
        newArrow.SetActive(true);
        arrowPool.Add(newArrow);
        return newArrow;
    }

    /// <summary>
    /// ȭ�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="arrow"></param>
    public void ReturnBullet(GameObject arrow)
    {
        arrow.SetActive(false);
    }
}
