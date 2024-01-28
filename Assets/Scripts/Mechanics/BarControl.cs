using System;
using Based.Utility;
using Events;
using MechanicEvents;
using Roro.Scripts.GameManagement;
using Roro.Scripts.Sounds.Core;
using Roro.Scripts.Sounds.Data;
using Roro.Scripts.UI.UITemplates.UITemplateImplementations;
using UnityCommon.Modules;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mechanics
{
    public class BarControl : MonoBehaviour
    {
        [SerializeField] 
        private SliderTemplate m_SliderTemplate;
        
        [SerializeField] 
        private Sound m_PopSound;
        
        [SerializeField]
        private Image m_BarImage;

        [SerializeField] 
        private Image m_DateImage;

        [SerializeField] 
        private float m_BarDecreaseSpeed = 2;

        private float m_CurrentValue;
        private float m_AimedValue;

        private bool m_IsBarActive;
        private void Start()
        {
            Conditional.Wait(4).Do(() =>
            {
                m_IsBarActive = true;
            });
            
            m_CurrentValue = 80;
            m_SliderTemplate.Set(m_CurrentValue,100);

            m_DateImage.sprite = GameManager.Instance.DateChar.NormalSprite;
        }

        private void Update()
        {
            
            if (!m_IsBarActive)
            {
                return;
            }
            
            if (m_CurrentValue > 99 || m_CurrentValue < 1)
            {
                m_IsBarActive = false;
                
                m_BarImage.color = Color.black;
                
                using var evt = MechanicResultEvent.Get(false);
                evt.SendGlobal();
            }
            

            m_CurrentValue -= 0.1f * m_BarDecreaseSpeed * Time.deltaTime;
            m_SliderTemplate.AnimatedSet(m_CurrentValue,0.1f,100);
            
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                m_CurrentValue += 10f;
                
                SoundManager.Instance.PlayOneShot(m_PopSound);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                m_CurrentValue -= 10f;
                
                SoundManager.Instance.PlayOneShot(m_PopSound);
            }
        }
    }
}
