using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : BaseUI
{
    List<Button> buttons = new List<Button>();
    List<UnityAction> actions = new List<UnityAction>();

    GameObject popup;
    TMP_Text popUpText;
    Button yes;
    Button no;
    //�⺻�� 0
    //�ٽ��ϱ��� 1
    //�����ϱ��� 2
    int check;

    [SerializeField] GameObject optionCanvas;

    GameObject lastButton;
    Color highlightButton;

    
    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        GameManager.PlayerInput.SwitchCurrentActionMap(ActionMap.UI);
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        
    }

    private void Update()
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

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
    void ContinueButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.PlayerInput.SwitchCurrentActionMap(ActionMap.Cam);
    }

    void NewButton()
    {
        popUpText.text = "������ ���� �����Ͻðڽ��ϱ�?";
        popup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(yes.gameObject);
        check = 1;
    }

    void OptionButton()
    {
        optionCanvas.SetActive(true);
        gameObject.SetActive(false);
    }

    void ExitButton()
    {
        popUpText.text = "����ȭ������ ���ư��ðڽ��ϱ�?";
        popup.SetActive(true);
        EventSystem.current.SetSelectedGameObject(yes.gameObject);
        check = 2;
    }

    void PopUpYes()
    {
        if (check == 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(Scene.GameScene);
            GameManager.PlayerInput.SwitchCurrentActionMap(ActionMap.Cam);
        }
        else if (check == 2)
        {
            SceneManager.LoadScene(Scene.MainScene);
        }
    }

    void PopUpNo()
    {
        check = 0;
        popup.SetActive(false);
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }
    void Init()
    {
        buttons.Add(GetUI<Button>("ContinueButton"));
        buttons.Add(GetUI<Button>("NewButton"));
        buttons.Add(GetUI<Button>("OptionButton"));
        buttons.Add(GetUI<Button>("ExitButton"));

        yes = GetUI<Button>("Yes");
        no = GetUI<Button>("No");
        popUpText = GetUI<TMP_Text>("PopUpText");
        popup = GetUI("PopUp");

        actions.Add(ContinueButton);
        actions.Add(NewButton);
        actions.Add(OptionButton);
        actions.Add(ExitButton);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.AddListener(actions[i]);
        }

        yes.onClick.AddListener(PopUpYes);
        no.onClick.AddListener(PopUpNo);

    }

}
