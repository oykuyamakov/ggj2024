using System.Collections;
using System.Collections.Generic;
using Roro.Scripts.GameManagement;
using UnityCommon.Modules;
using UnityCommon.Runtime.UI.Animations;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] 
    private Button m_StartButton;

    private UIAlphaAnim m_PanelAlphaAnim;
    
    private UITranslateAnim m_StartButtonTranslateAnim;
    
    private void Awake()
    {
        m_PanelAlphaAnim = GetComponentInChildren<UIAlphaAnim>();
        m_StartButtonTranslateAnim = GetComponentInChildren<UITranslateAnim>();
        m_StartButton.onClick.RemoveAllListeners();
        m_StartButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnEnable()
    {
        m_PanelAlphaAnim.FadeIn();

        Conditional.Wait(1).Do(() => m_StartButtonTranslateAnim.FadeIn());
        
        m_StartButton.enabled = true;
    }

    private void OnStartButtonClicked()
    {
        m_StartButton.enabled = false;
        SceneLoader.Instance.LoadFirstScene();
        m_PanelAlphaAnim.FadeOut();
        Conditional.Wait(1).Do(() =>
        {
            this.gameObject.SetActive(false);
        });

    }
}
