﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Made By : 안규원
// Comment : 메인 화면의 UI들을 관리 (애니메이션 및 기능들)할 스크립트입니다.

public class MainUIMnager : MonoBehaviour
{
    private static MainUIMnager instance;
    public static MainUIMnager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogWarning(typeof(MainUIMnager).FullName + " Instance is null.");
                return null;
            }
            return instance;
        }
    }

    public float SlideObjectSpeed = 5.0f;
    private bool isSettingButtonsActive = true;
    // Volume
    public RectTransform VolumeController;
    private bool isVolumeControl = false;
    private Coroutine VolumeCoroutine = null;

    // Vibration
    public RectTransform VibrationController;
    private bool isVibrationControl = false;
    private Coroutine VibrationCoroutine = null;

    // Upgrade
    public GameObject UpgradePrefab;

    // Shop
    public GameObject ShopPrefab;

    // Text
    public Text GoldText;
    public Text TopazText;
    public Text RubyText;
    public Text SapphireText;
    public Text DiamondText;

    public WindowUI window;

    public Player m_Player;

    private void Awake()
    {
        instance = this;
        GameObject Images = GameObject.Find("Images");
        if (m_Player)
            m_Player.hp = m_Player.maxHp;
        if (SceneLoader.GetNowSceneName() == "MapTree")
            isSettingButtonsActive = false;

        // GameObject GoldImg = Images?.transform?.GetChild(0)?.gameObject;
        // GameObject MaterialImg = Images?.transform?.GetChild(0)?.gameObject;

            // GoldText = GoldImg?.transform?.GetChild(0)?.GetComponent<Text>();

            // RubyText = MaterialImg?.transform?.GetChild(0)?.GetComponent<Text>();
            // SapphireText = MaterialImg?.transform?.GetChild(1)?.GetComponent<Text>();
            // TopazText = MaterialImg?.transform?.GetChild(2)?.GetComponent<Text>();
            // DiamondText = MaterialImg?.transform?.GetChild(3)?.GetComponent<Text>();
    }

    public void VolumeControl()
    {
        isVolumeControl = !isVolumeControl;

        if(VolumeCoroutine != null)
        {
            StopCoroutine(VolumeCoroutine);
            VolumeCoroutine = null;
        }
        VolumeCoroutine = StartCoroutine(CO_SlideObject(VolumeController, isVolumeControl ? new Vector3(555, 130) : new Vector3(1310, 130)));
    }

    public void VibrationControl()
    {
        isVibrationControl = !isVibrationControl;

        if (VibrationCoroutine != null)
        {
            StopCoroutine(VibrationCoroutine);
            VibrationCoroutine = null;
        }
        VibrationCoroutine = StartCoroutine(CO_SlideObject(VibrationController, isVibrationControl ? new Vector3(555, -130) : new Vector3(1310, -130)));
    }

    IEnumerator CO_SlideObject(RectTransform obj, Vector3 pos)
    {
        while(Vector3.Distance(obj.localPosition, pos) > 50.0f)
        {
            obj.localPosition += (pos - obj.localPosition).normalized * SlideObjectSpeed * Time.deltaTime;
            yield return null;
        }
        obj.localPosition = pos;
    }

    public void SetVolume(float value)
    {
        SoundManager.Instance.SetVolume(value);
    }

    public void SetVibration(float value)
    {
        Vibration.Instance.SetVibrationFactor(value);
    }

    public void SetSettingButtonsToggle()
    {
        SetSettingButtonsActive(!isSettingButtonsActive);
    }

    public void SetSettingButtonsActive(bool active)
    {
        isSettingButtonsActive = active;
        Transform SettingButton = GameObject.Find("SettingBtn").transform;
        SettingButton.parent.GetChild(1).gameObject.SetActive(active);
        SettingButton.parent.GetChild(2).gameObject.SetActive(active);
        SettingButton.parent.parent.GetChild(0).gameObject.SetActive(active);
    }

    public void OpenUpgradePrefab()
    {
        Instantiate(UpgradePrefab);
    }

    public void OpenShopPrefab()
    {
        Instantiate(ShopPrefab);
    }

    public void ChangeScene(int index)
    {
        SceneLoader.LoadSceneWithFadeStatic(index);
    }

    public void ChangeScene(string name)
    {
        SceneLoader.LoadSceneWithFadeStatic(name);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
        Application.Quit();
#endif
    }

    public void SetText()
    {
        GoldText.text = GameManager.instance.goldCount.ToString();

        TopazText.text = GameManager.instance.topazCount.ToString();
        RubyText.text = GameManager.instance.rubyCount.ToString();
        SapphireText.text = GameManager.instance.sapphireCount.ToString();
        DiamondText.text = GameManager.instance.diamondCount.ToString();
    }
}
