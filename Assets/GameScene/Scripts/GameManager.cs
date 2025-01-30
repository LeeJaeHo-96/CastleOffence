using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public PlayerInput PlayerInput;

    [SerializeField] public int gold;

    [SerializeField] TMP_Text haveGold;
    [SerializeField] TMP_Text curTimer;
    [SerializeField] TMP_Text result;

    public int timer;

    [SerializeField] GameObject cam;
    [SerializeField] GameObject pause;

    private void Awake()
    {
        SingletonInit();
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
        Debug.Log(PlayerInput.currentActionMap);

    }

    IEnumerator TimerRoutine()
    {
        while (true)
        {
            timer--;
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateInfo()
    {
        haveGold.text = $"��� : {gold}";
        curTimer.text = $"���� �ð� : {timer}";

        if (timer == 0)
        {
            StopAllCoroutines();
            result.text = "������ �ı����� ���ϰ� �����Ͽ����ϴ�.. �й�.. \n 'E' ��ư�� ���� ����ȭ������ ���ư�����..";
            result.gameObject.SetActive(true);
            //Todo: ���� �й�
        }
    }

    void infoView()
    {

    }

    void SingletonInit()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if ((result.gameObject.activeSelf))
        {
            if (context.performed)
            {
                SceneManager.LoadScene(Scene.MainScene);
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pause.SetActive(!pause.activeSelf);
        }
    }

    void Init()
    {
        gold = 0;
        timer = 100;

        PlayerInput = cam.GetComponent<PlayerInput>();
    }
}
