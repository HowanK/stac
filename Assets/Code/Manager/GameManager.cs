﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Type
{
    public enum TYPE
    {
        NONE,
        TOPAZ,
        RUBY,
        SAPPHIRE,
        DIAMOND
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isPlayerTurn = true;
    public List<GameObject> AllCards = new List<GameObject>();
    public int cost = 3;

    public int goldCount = 0;
    public int topazCount = 0;
    public int rubyCount = 0;
    public int sapphireCount = 0;
    public int diamondCount = 0;
    
    public ResultWindow ResultWindowPrefab;
    public string mapName;// 현재 맵의 이름 (나중에 사용할 예정)
    public bool isFirstStart = true;// 해당맵이 처음 시작되는것 인지 맵이 바뀔때 마다 true로 해주어야 합니다.

    public MonsterManager monsterOption;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }    
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        monsterOption = GetComponent<MonsterManager>();
    }

    public void OnGameEnd()
    {
        ResultWindow obj = Instantiate(ResultWindowPrefab);
        obj.TitleButton.onClick.AddListener(() =>
        {
            SceneLoader.LoadSceneWithFadeStatic("MapTree");
            // SceneLoader.LoadSceneWithFadeStatic("");
        });
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += AutoReset;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        SceneManager.sceneLoaded -= AutoReset;
    }

    void AutoReset(Scene scene, LoadSceneMode mode)
    {
        SaveGemCount();
        switch (scene.name)
        {
            case "Title":
                isFirstStart = true;
                LoadGemCount();
                MainUIMnager.Instance.SetText();
            break;
        }
    }

    public void LoadGemCount()
    {
        goldCount= PlayerPrefs.GetInt("goldCount");
        
        topazCount = PlayerPrefs.GetInt("topazCount");
        rubyCount = PlayerPrefs.GetInt("rubyCount");
        sapphireCount = PlayerPrefs.GetInt("sapphireCount");
        diamondCount = PlayerPrefs.GetInt("diamondCount");
    }

    public void SaveGemCount()
    {
        PlayerPrefs.SetInt("goldCount", goldCount);
        
        PlayerPrefs.SetInt("topazCount", topazCount);
        PlayerPrefs.SetInt("rubyCount", rubyCount);
        PlayerPrefs.SetInt("sapphireCount", sapphireCount);
        PlayerPrefs.SetInt("diamondCount", diamondCount);
    }
}
