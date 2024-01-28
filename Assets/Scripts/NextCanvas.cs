using System;
using System.Collections;
using System.Collections.Generic;
using Roro.Scripts.GameManagement;
using UnityCommon.Modules;
using UnityCommon.Runtime.UI.Animations;
using UnityEngine;
using UnityEngine.UI;

public class NextCanvas : MonoBehaviour
{
    [SerializeField] 
    private Button m_NextButton;

    private UIAlphaAnim m_translateAnim;
    
    private void Awake()
    {
        m_translateAnim = GetComponentInChildren<UIAlphaAnim>();
        m_NextButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void OnEnable()
    {
        m_translateAnim.FadeIn();
        
        m_NextButton.enabled = true;
    }

    private void OnNextButtonClicked()
    {
        m_NextButton.enabled = false;
        GameManager.Instance.NextScene();
        m_translateAnim.FadeOut();
        Conditional.Wait(1).Do(() =>
        {
            this.gameObject.SetActive(false);
        });

    }
}
