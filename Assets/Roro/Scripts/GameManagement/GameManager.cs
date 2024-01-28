using System;
using System.Collections.Generic;
using CharacterImplementations;
using Events;
using MechanicEvents;
using Roro.Scripts.Serialization;
using Roro.Scripts.Sounds.Core;
using Roro.Scripts.Utility;
using Sirenix.OdinInspector;
using TMPro;
using UnityCommon.Modules;
using UnityCommon.Runtime.UI;
using UnityCommon.Singletons;
using UnityCommon.Variables;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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
        private GameObject m_NextCanvas;

        [SerializeField] 
        private Character m_CurrentCharacter;
        
        [SerializeField]
        private TextMeshProUGUI m_TimerText;
        
        private float m_Timer = 0f;
        
        private bool m_OnSwitchToNextScene = false;
        
        public Character CurrentCharacter => m_CurrentCharacter;
        
        public int CurrentSceneIndex => m_CurrentSceneIndex;
        
        private int m_CurrentSceneIndex = 0;

        private List<SceneName> m_ScenesByOrder = new List<SceneName>()
        {
            SceneName.FirstScene,
            SceneName.SubwayScene,
            SceneName.BathroomScene,
            SceneName.EatScene,
            SceneName.WalkingScene,
            SceneName.SubwayScene,
            SceneName.WineScene,
            SceneName.StainScene,
            SceneName.SexScene
        };
        
        public BoolVariable GameIsRunning => m_GameIsRunning;

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
            
        }

        private void OnDestroy()
        {
            GEM.RemoveListener<MechanicResultEvent>(OnMechanicResultEvent);
        }

        private void OnMechanicResultEvent(MechanicResultEvent evt)
        {
            Debug.Log(evt.result + " Game Manager");
            if (evt.result)
            {
                OnSuccessfulMechanic();
            }
            else
            {
                OnFailedMechanic();
            }
        }

        public void EnableNextCanvas()
        {
            m_NextCanvas.SetActive(true);
        }
        
        private void OnGameOver()
        {
            
        }

        private void OnSuccessfulMechanic()
        {
            NextScene();
        }

        private void OnFailedMechanic()
        {
            m_LiveVar.Value--;

            if (m_LiveVar.Value <= 0)
            {
                OnGameOver();
                return;
            }
            
            NextScene();
        }
        

        public void NextScene()
        {
            m_CurrentSceneIndex++;
            
            if(m_CurrentSceneIndex >= m_ScenesByOrder.Count)
                m_CurrentSceneIndex = 0;
            
            FadeInOut.Instance.DoTransition(() =>
            {
                StartCoroutine(SceneLoader.Instance.LoadScene(m_ScenesByOrder[m_CurrentSceneIndex]));

                Conditional.Wait(2).Do(() =>
                {
                    m_Timer = 0;
                    m_OnSwitchToNextScene = false;
                });
            }, 1f, Color.black);
            
        }
        private void Update()
        {
            if (!m_OnSwitchToNextScene)
            {
                m_Timer += Time.deltaTime;
            
                m_TimerText.text = m_Timer.ToString("F2");
            }

            if (m_Timer >= 60f)
            {
                m_OnSwitchToNextScene = true;
                EnableNextCanvas();
            }
        }
        
    }
}