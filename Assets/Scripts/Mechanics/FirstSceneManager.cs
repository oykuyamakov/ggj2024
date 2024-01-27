using System;
using System.Collections;
using System.Collections.Generic;
using Roro.Scripts.GameManagement;
using UnityCommon.Modules;
using UnityEngine;
using UnityEngine.Video;

public class FirstSceneManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject m_VideoPanel;
    [SerializeField] 
    private VideoPlayer m_VideoPlayer;


    private void Awake()
    {
        if (m_VideoPanel != null)
        {
           m_VideoPlayer.Play();
        }

        Conditional.Wait(3).Do(GameManager.Instance.EnableNextCanvas);
    }
}
