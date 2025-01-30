using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Option : BaseUI
{
    List<Button> buttons = new List<Button>();

    [SerializeField] GameObject pause;
    GameObject lastButton;
    Color highlightButton;
    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void OnDisable()
    {
        pause.SetActive(true);
    }

    void Update()
    {
        NullClick();
        ButtonHighlight();
    }

    void NullClick()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastButton.gameObject);
        }
    }

    void ButtonHighlight()
    {
        //�÷��� ��ư 1����
        //���� 0.1�� �ٲ�
        //2 3
        //�÷��� ���õ� ��ư ����
        //�÷� ���� 1�� �ٲ�
        //���õ� ��ư�� �÷� ����

        for (int i = 0; i < buttons.Count; i++)
        {
            highlightButton = buttons[i].GetComponent<Image>().color;
            highlightButton.a = 0.1f;
            buttons[i].GetComponent<Image>().color = highlightButton;
        }

        highlightButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        highlightButton.a = 1f;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = highlightButton;
    }

    void CloseButton()
    {
        gameObject.SetActive(false);
        //�׼Ǹ� ��������
    }

    void Init()
    {
        buttons.Add(GetUI<Button>("Sound"));
        buttons.Add(GetUI<Button>("Light"));
        buttons.Add(GetUI<Button>("Close"));

        buttons[2].onClick.AddListener(CloseButton);
    }
}
