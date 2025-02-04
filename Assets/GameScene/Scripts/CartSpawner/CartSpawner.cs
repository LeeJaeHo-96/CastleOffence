using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CartSpawner : MonoBehaviour
{
    [Inject]
    GameManager gameManager;

    [Inject]
    DiContainer container;

    [SerializeField] Vector3 cartSpawner;

    [SerializeField] GameObject cartLevel1;
    [SerializeField] GameObject cartLevel2;
    [SerializeField] GameObject cartLevel3;

    [SerializeField] Slider cartMakeBar;

    [SerializeField] Button makedButton;

    [SerializeField] GameObject cart;
    [SerializeField] TMP_Text cartCostText;
    Coroutine cartOnCo;

    //īƮ ���� üũ��
    public GameObject makedCart;
    public void Awake()
    {
        Init();
    }

    /// <summary>
    /// ��ư�� _ īƮ ���׷��̵�
    /// </summary>
    public void CartUpgrade()
    {
        if (cart == cartLevel1)
        {
          //  if (GameManager.instance.gold >= 500)
          //  {
          //      GameManager.instance.gold -= 500;
          //      cart = cartLevel2;
          //      cartCostText.text = "������ ��ȭ�մϴ�.\n ��� : 1000";
          //  }

            if (gameManager.gold >= 500)
            {
                gameManager.gold -= 500;
                cart = cartLevel2;
                cartCostText.text = "������ ��ȭ�մϴ�.\n ��� : 1000";
            }
        }
        else if (cart == cartLevel2)
        {
           // if (GameManager.instance.gold >= 1000)
           // {
           //     GameManager.instance.gold -= 1000;
           //     cart = cartLevel3;
           //     cartCostText.text = "������ ��ȭ�մϴ�.\n ��� : 1500";
           // }

            if (gameManager.gold >= 1000)
            {
                gameManager.gold -= 1000;
                cart = cartLevel3;
                cartCostText.text = "������ ��ȭ�մϴ�.\n ��� : 1500";
            }
        }
    }

    /// <summary>
    /// īƮ �������ִ� �ڷ�ƾ
    /// </summary>
    public IEnumerator MakeCart()
    {
        if (makedCart != null)
        {
            //Todo : �ؽ�Ʈ ����
            Debug.Log("�̹� ������ �ֽ��ϴ�.");
            StopCoroutine("MakeCart");
        }

        yield return new WaitForSeconds(3f);


        cartMakeBar.gameObject.SetActive(false);

        //���� ����
        if (makedCart == null)
        {
            //makedCart = Instantiate(cart, cartSpawner, Quaternion.identity);
            makedCart = container.InstantiatePrefab(cart, cartSpawner, Quaternion.identity, null);
            Rigidbody rigid = makedCart.GetComponent<Rigidbody>();

            rigid.velocity = Vector3.down * 10f;
            rigid.AddForce(rigid.velocity, ForceMode.Impulse);

            if (cartOnCo == null)
            {
                StartCoroutine(CartOnRoutine(makedCart));
            }
        }
    }

    /// <summary>
    /// ���� ���� �� ä���ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    public IEnumerator CartMakeBarRoutine()
    {
        //�������϶� ���� ������
        if (cartMakeBar.gameObject.activeSelf)
            yield break;

        if (makedCart != null)
        {
            //Todo : �ؽ�Ʈ ����
            Debug.Log("�̹� ������ �ֽ��ϴ�.");
            StopCoroutine("CartMakeBarRoutine");
        }

        if ( makedCart == null)
        {
            cartMakeBar.value = 0;
            cartMakeBar.gameObject.SetActive(true);
            while (cartMakeBar.value < 1)
            {
                cartMakeBar.value += 0.03f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    /// <summary>
    /// īƮ�� �����ϸ� ��ũ��Ʈ ���ִ� �ڷ�ƾ
    /// </summary>
    /// <param name="makedCart">������ īƮ</param>
    /// <returns></returns>
    IEnumerator CartOnRoutine(GameObject makedCart)
    {
        yield return new WaitForSeconds(1f);
        if (makedCart.GetComponent<CartLevel1>() != null)
            makedCart.GetComponent<CartLevel1>().enabled = true;

        if (makedCart.GetComponent<CartLevel2>() != null)
            makedCart.GetComponent<CartLevel2>().enabled = true;

        if (makedCart.GetComponent<CartLevel3>() != null)
            makedCart.GetComponent<CartLevel3>().enabled = true;

        cartOnCo = null;

    }

    void Init()
    {
        cartSpawner = transform.position;

        makedButton.onClick.AddListener
        (() =>
            {
                StartCoroutine(MakeCart());
                StartCoroutine(CartMakeBarRoutine());
            }
        );
    }
}
