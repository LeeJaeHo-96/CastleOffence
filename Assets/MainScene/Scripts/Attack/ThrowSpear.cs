using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowSpear : MonoBehaviour
{
    [SerializeField] GameObject spearPrefab;
    //â������ ���� ī�޶�
    [SerializeField] Camera spearCam;

    int cooldown;

    private void Awake()
    {
       Init();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (Camera.main.depth == 0)
        {
            if (cooldown <= 0)
            {
                if (context.performed)
                {
                    //����ĳ��Ʈ �� ī�޶� ���� �� �� ķ���� ���� ����
                    Ray ray = spearCam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        // Ŭ���� ��ġ�� â ����
                        Vector3 spawnPosition = hit.point + Vector3.up * 10;
                        GameObject spear = Instantiate(spearPrefab, spawnPosition, Quaternion.identity);

                        // �߷��� �̿��� �������� ����
                        Rigidbody rb = spear.GetComponent<Rigidbody>();
                        if (rb == null)
                        {
                            rb = spear.AddComponent<Rigidbody>();
                        }
                    }
                }
            }
        }
    }

    void Init()
    {
        int cooldown = 0;
    }
}
