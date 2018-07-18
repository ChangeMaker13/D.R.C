﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float m_SceneTimer;

    public PlayerData m_Player;
    private Canvas m_Canvas;        //the place system draws user interfaces

    public GameObject m_GameOver;       //gameover picture
    public Text m_BulletNum;
    public Text m_Hp;

	// Use this for initialization
    void Awake()
    {
        m_Canvas = FindObjectOfType<Canvas>();
    }

	void Start () {
        m_SceneTimer = 0f;
        StartCoroutine("ShowPlayerData");
	}

    void FixedUdpate()
    {
        
    }

    //Get player's bullet remained and HP.
    IEnumerator ShowPlayerData()
    {
        while(true)
        {
            m_BulletNum.text = m_Player.m_WeaponInhand.m_AmmoBulletNum.ToString() + "/" + m_Player.m_WeaponInhand.m_MaxBulletNum.ToString();
            m_Hp.text = m_Player.m_Hp.ToString() + "/" + m_Player.m_MaxHp;
            yield return null;
        }
    }

    //Activate "Game over"UI when player die.
    public void ActiveUI(string _str)
    {
        m_GameOver.SetActive(true);
    }

    //change scene
    public void NextScene(string _Str)
    {
          SceneManager.LoadScene(_Str);
    }

    //change scene with delay
    public IEnumerator NextScene(string _Str, float _delay)
    {
        while (true)
        {
            if (m_SceneTimer < _delay)
            {
                m_SceneTimer += Time.deltaTime;
            }
            else
            {
                m_SceneTimer = 0f;
                SceneManager.LoadScene(_Str);
                StopCoroutine("NextScene");
            }

            yield return null;
        }
    }

}