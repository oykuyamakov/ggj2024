using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }
        
        #region Singleton

        public SceneName CurrentScene;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                return;
            }

            DontDestroyOnLoad(gameObject);

            StartCoroutine(StartGame());
        }

        #endregion
        
        private SceneName m_LastScene = SceneName.Null;


        private IEnumerator StartGame()
        {
            StartCoroutine(LoadScene(SceneName.SharedScene));

            yield return new WaitForSeconds(2);
            
            //LoadFirstScene();
        }
        
        public void LoadFirstScene()
        {
            m_LastScene = SceneName.IntroScene;

            StartCoroutine(LoadScene(SceneName.CharacterSelectionScene));
        }
        
        public IEnumerator LoadScene(SceneName sceneName)
        {
            //var sceneSounds = FindObjectsOfType<SceneSound>();

            if (sceneName != SceneName.SharedScene)
            {
                var multiplier = sceneName == SceneName.FirstScene ? 5 : 1;
                
                // foreach (var sceneSo in sceneSounds)
                // {
                //     multiplier = sceneSo.AutoStart ? 1 : 5;
                //     Debug.Log(sceneSo.name+ "SoundScene founded, will be MUTED");
                //
                //     while (sceneSo.Source.volume > 0)
                //     {
                //         sceneSo.Source.volume -= 0.1f * Time.deltaTime * multiplier;
                //     
                //         yield return null;
                //     }
                // }
            }
            
            var op1 = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
            
            UnLoadPrevScene();

            while (!op1.isDone)
            {
                yield return null;
            }
            
            Debug.Log(sceneName + "LOADED");
            

            if (sceneName != SceneName.SharedScene)
            {
                m_LastScene = sceneName;
            }
            
            CurrentScene = sceneName;
            
            if(sceneName == SceneName.SharedScene)
                yield break;
            
            //Debug.Log("now will search for scene sounds");

            //var newSceneSounds = FindObjectsOfType<SceneSound>();
            
            // Debug.Log(newSceneSounds.Length + "SoundScene found");
            //
            // foreach (var nSs in newSceneSounds)
            // {
            //     Debug.Log(nSs.name+ "SoundScene founded, will be played");
            //
            //     nSs.StartMusic(true);
            // }
            
        }

        private void UnLoadPrevScene()
        {
            Debug.Log(m_LastScene.ToString());
            if (m_LastScene != SceneName.Null)
            {
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(m_LastScene.ToString());
            }
        } 
        private IEnumerator UnLoadScene(SceneName sceneName)
        {
            yield return new WaitForSeconds(1);
            
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName.ToString());
        }

    }

    public enum SceneName
    {
        IntroScene,
        SharedScene,
        LoadingScene,
        SexScene,
        SubwayScene,
        StainScene,
        BathroomScene,
        WalkingScene,
        Null,
        FirstScene,
        EatingScene,
        WineScene,
        CharacterSelectionScene
    }