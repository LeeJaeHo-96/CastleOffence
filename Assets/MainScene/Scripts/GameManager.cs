using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    [SerializeField] public int gold;

    [SerializeField] TMP_Text haveGold;
    [SerializeField] TMP_Text curTimer;
    [SerializeField] TMP_Text result;

    public int timer;

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
            result.text = "������ �ı����� ���ϰ� �����Ͽ����ϴ�.. �й�..";
            result.gameObject.SetActive(true);
            //Todo: ���� �й�
        }
    }

    void SingletonInit()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(instance);

        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        gold = 0;
        timer = 60;
    }
}