using System;
using System.Collections;
using System.Collections.Generic;
using Roro.Scripts.GameManagement;
using Roro.Scripts.Sounds.Core;
using Roro.Scripts.Sounds.Data;
using UnityCommon.Modules;
using UnityCommon.Runtime.UI;
using UnityCommon.Singletons;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BathroomSceneManager : SingletonBehaviour<BathroomSceneManager>
{
    [SerializeField]
    private SpriteRenderer m_BackgroundImage;

    [SerializeField] 
    private CharacterMovement m_CharacterMovement;
    
    [SerializeField] 
    private Sound m_PeeSoud;

    
    private void Awake()
    {
        if (!SetupInstance(false))
            return;
    }

    [SerializeField]
    private List<Sprite> m_BackgroundSprites;
    
    private int m_CurrentBackgroundIndex = 0;

    public void OnBorder(BorderTrigger borderTrigger)
    {
        Debug.Log("Inside" + borderTrigger.BorderType + m_CurrentBackgroundIndex);

        if (borderTrigger.BorderType == BorderType.Right)
        {
            if(m_CurrentBackgroundIndex == 2)
                return;
            m_CurrentBackgroundIndex++;
        }

        if (borderTrigger.BorderType == BorderType.Left)
        {
            if(m_CurrentBackgroundIndex == 0 || m_CurrentBackgroundIndex > 2)
                return;
            m_CurrentBackgroundIndex--;
        }
        
        if(borderTrigger.BorderType == BorderType.Middle)
        {
            if (m_CurrentBackgroundIndex != 2 || m_CharacterMovement.transform.localScale.y < 0.7f)
            {
                return;
            }

            m_CurrentBackgroundIndex++;
        }
        
        Debug.Log("Changing BG");
        
        Adjust();
       
    }

    private void Adjust()
    {
        if (m_CurrentBackgroundIndex > 3)
        {
            m_CharacterMovement.CanMove = false;
            
            SoundManager.Instance.PlayOneShot(m_PeeSoud);
            
            Conditional.Wait(2).Do(() =>
            {
                GameManager.instance.EnableNextCanvas();
            });
            
            return;
        }
        
        FadeInOut.Instance.DoTransition(() =>
        {
            m_CharacterMovement.ResetPos();
            
            m_BackgroundImage.sprite = m_BackgroundSprites[m_CurrentBackgroundIndex];
               
        }, 1f, Color.black);
       
    }
    
}
