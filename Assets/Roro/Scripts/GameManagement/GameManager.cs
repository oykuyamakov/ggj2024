using System.Collections.Generic;
using CharacterImplementations;
using Events;
using MechanicEvents;
using Roro.Scripts.Utility;
using Sirenix.OdinInspector;
using TMPro;
using UnityCommon.Modules;
using UnityCommon.Runtime.UI;
using UnityCommon.Runtime.UI.Animations;
using UnityCommon.Singletons;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.UI;

namespace Roro.Scripts.GameManagement
{
    [DefaultExecutionOrder(ExecOrder.GameManager)]
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField]
        private int m_TargetFrameRate = 60;

        [SerializeField] 
        private BoolVariable m_GameIsRunning;

        [SerializeField] 
        private IntVariable m_LiveVar;

        [SerializeField] 
        private Character m_CurrentCharacter;

        #region UI
        
        //Next UI
        
        [SerializeField] 
        private GameObject m_NextUI;

        //Timer UI
        
        [SerializeField] 
        private Canvas m_TimerCanvas;
        
        [SerializeField] 
        private Image m_TimerImage;
        
        [SerializeField] 
        private UIAlphaAnim m_TimerAlphaAnim;
        
        [SerializeField] 
        private List<Sprite> m_TimerUIList;
        
        //Scene Info UI
        
        [SerializeField]
        private Canvas m_SceneInfoCanvas;
        
        [SerializeField]
        private UIAlphaAnim m_SceneInfoPanelAlphaAnim;
        
        [SerializeField]
        private List<Sprite> m_SubwaySceneInfos;
        [SerializeField]
        private List<Sprite> m_BathroomSceneInfos;
        [SerializeField] 
        private List<Sprite> m_EatingSceneInfos;
        [SerializeField] 
        private List<Sprite> m_WalkingSceneInfos;
        [SerializeField] 
        private List<Sprite> m_Subway2SceneInfos;
        [SerializeField] 
        private List<Sprite> m_WineSceneInfos;
        [SerializeField]
        private List<Sprite> m_StainSceneInfos;
        [SerializeField] 
        private List<Sprite> m_SexSceneInfos;
        
        // WIN LOOSE UI
        
        [SerializeField]
        private Canvas m_WinLooseCanvas;
        
        [SerializeField]
        private UIAlphaAnim m_WinLooseCanvasAlphaAnim;
        
        [SerializeField]
        private Image m_WinLooseImage;

        [SerializeField] 
        private Sprite m_WinScreen;

        [SerializeField] 
        private Sprite m_LooseScreen;

        [SerializeField] 
        private GameObject m_IntroUI;

        #endregion
        
        public Character CurrentCharacter => m_CurrentCharacter;
        public int CurrentSceneIndex => m_CurrentSceneIndex;
        
        public BoolVariable GameIsRunning => m_GameIsRunning;
        
        private float m_Timer = 0f;
        
        private bool m_OnSwitchToNextScene = false;
        
        private int m_CurrentSceneIndex = 0;

        private List<SceneName> m_ScenesByOrder = new List<SceneName>()
        {
            SceneName.CharacterSelectionScene,
            SceneName.FirstScene,
            SceneName.SubwayScene,
            SceneName.BathroomScene,
            SceneName.EatingScene,
            SceneName.WalkingScene,
            SceneName.SubwayScene,
            SceneName.WineScene,
            SceneName.StainScene,
            SceneName.SexScene
        };
        
        [Button]
        public void ToggleGame()
        {
            m_GameIsRunning.Value = !m_GameIsRunning.Value;
        }

        private void Awake()
        {
            if (!SetupInstance())
                return;

            Variable.Initialize();
            
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Application.backgroundLoadingPriority = ThreadPriority.Normal;

            Application.targetFrameRate = m_TargetFrameRate;
            
            ConditionalsModule.CreateSingletonInstance();
            
            m_GameIsRunning =  Variable.Get<BoolVariable>("GameIsRunning");
            
            m_GameIsRunning.Value = true;
            
            GEM.AddListener<MechanicResultEvent>(OnMechanicResultEvent);

            SetUpUIForStart();
        }

        private void SetUpUIForStart()
        {
            m_TimerCanvas.enabled = false;
            m_NextUI.SetActive(false);
            m_SceneInfoCanvas.enabled = false;
            //m_WinLooseCanvas.enabled = false;
            m_IntroUI.SetActive(true);
        }

        private void OnDestroy()
        {
            GEM.RemoveListener<MechanicResultEvent>(OnMechanicResultEvent);
        }

        private void OnMechanicResultEvent(MechanicResultEvent evt)
        {
            if (evt.result)
            {
                OnSuccessfulMechanic();
            }
            else
            {
                OnFailedMechanic();
            }
        }
        
        private void OnSuccessfulMechanic()
        {
            m_OnSwitchToNextScene = true;
            
            EnableNextCanvas();
        }

        private void OnFailedMechanic()
        {
            m_OnSwitchToNextScene = true;
            
            m_LiveVar.Value--;

            if (m_LiveVar.Value <= 0)
            {
                OnGameOver();
                return;
            }
            
            EnableNextCanvas();
        }

        public void EnableNextCanvas()
        {
            m_NextUI.SetActive(true);
        }
        
        private void OnGameOver()
        {
            //Loose UI
        }

        private void EnableSceneInfoCanvas()
        {
            m_SceneInfoCanvas.enabled = true;
            m_SceneInfoPanelAlphaAnim.FadeIn();
            
            Conditional.Wait(5).Do(() =>
            {
                m_SceneInfoPanelAlphaAnim.FadeOut();
                
                Conditional.Wait(1).Do(() =>
                {
                    m_SceneInfoCanvas.enabled = false;
                    
                    m_Timer = 0;

                    if (m_CurrentSceneIndex > 1)
                    {
                        ToggleTimeUI(true);
                        m_OnSwitchToNextScene = false;
                    }
                });
            });
        }
        
        public void NextScene()
        {
            ToggleTimeUI(false);
            
            m_CurrentSceneIndex++;

            if (m_CurrentSceneIndex >= m_ScenesByOrder.Count)
            {
                //Win
                return;
            }
            
            FadeInOut.Instance.DoTransition(() =>
            {
                StartCoroutine(SceneLoader.Instance.LoadScene(m_ScenesByOrder[m_CurrentSceneIndex]));
                
                EnableSceneInfoCanvas();
               
            }, 3f, Color.black);
            
        }
        private void Update()
        {
            if (!m_OnSwitchToNextScene)
            {
                m_Timer += Time.deltaTime;
                UpdateTimerUI();
            }
            else
            {
                m_Timer = 0;
            }
            
            if (m_Timer >= 60f && !m_OnSwitchToNextScene)
            {
                m_OnSwitchToNextScene = true;
                OnFailedMechanic();
            }
        }

        private void UpdateTimerUI()
        {
            switch (m_Timer)
            {
                case 0:
                    return;
                case < 15f:
                    m_TimerImage.sprite = m_TimerUIList[0];
                    break;
                case < 30f:
                    m_TimerImage.sprite = m_TimerUIList[1];
                    break;
                case < 45f:
                    m_TimerImage.sprite = m_TimerUIList[2];
                    break;
                case < 60:
                    m_TimerImage.sprite = m_TimerUIList[3];
                    break;
                case < 75:
                    m_TimerImage.sprite = m_TimerUIList[4];
                    break;
            }
        }
        
        private void ToggleTimeUI(bool visible)
        {
            if (visible)
            {
                m_TimerImage.enabled = true;
                m_TimerCanvas.enabled = true;
                m_TimerAlphaAnim.FadeIn();
            }
            else
            {
                m_TimerImage.enabled = false;
                m_TimerCanvas.enabled = false;
                m_TimerAlphaAnim.FadeOut();
            }
        }

        public void ChangeCurrentCharacter(Character character)
        {
            m_CurrentCharacter = character;
            
            NextScene();
        }

        
    }
}