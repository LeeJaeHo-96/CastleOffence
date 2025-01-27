using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : BaseUI
{
    Button gameButton;
    Button keyButton;
    Button exitButton;
    private void Awake()
    {
        Bind();
        Init();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GameButton()
    {
        Debug.Log("���ӽ���");
    }

    void KeyButton()
    {
        Debug.Log("����Ű ����");
    }

    void ExitButton()
    {
        Debug.Log("��������");
    }

    void Init()
    {
        gameButton = GetUI<Button>("gameButton");
        keyButton = GetUI<Button>("keyButton");
        exitButton = GetUI<Button>("exitButton");

        gameButton.onClick.AddListener(GameButton);
        keyButton.onClick.AddListener(KeyButton);
        exitButton.onClick.AddListener(ExitButton);

    }
}
