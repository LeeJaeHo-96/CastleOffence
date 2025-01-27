using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRight : MonoBehaviour
{
    public int archerHP;

    Animation Animation;
    Animator animator;

    Coroutine shootCo;

    [SerializeField] ArrowPoolRight arrowPool;

    [SerializeField] GameObject archerPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        archerHP = 3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            shootCo = StartCoroutine(ShootRoutine(other.gameObject));
            animator.SetBool("Attack", true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tag.Player))
        {
            Debug.Log("�����");
            if (other.gameObject.GetComponent<CartMover>().cartHP <= 0)
            {
                Debug.Log("�׾���");
                animator.SetBool("Attack", false);
                StopCoroutine(shootCo);
            }
        }
    }

    IEnumerator ShootRoutine(GameObject cart)
    {
        while (true)
        {
            GameObject arrow = arrowPool.GetArrow();
            arrow.transform.position = archerPoint.transform.position;
            yield return new WaitForSeconds(4f);
            
        }
    }

    void Update()
    {
        Die();
    }

    void Die()
    {
        if (archerHP <= 0)
        {
            animator.SetBool("Die", true);
            gameObject.SetActive(false);
        }
    }
}
