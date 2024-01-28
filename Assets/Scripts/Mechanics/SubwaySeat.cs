using System;
using System.Linq;
using Roro.Scripts.GameManagement;
using UnityCommon.Modules;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Mechanics
{
    public class SubwaySeat : SelectableObject
    {
        [SerializeField] 
        private bool m_IsOccupied;
        
        [SerializeField]
        private Image m_SeatIndicatorImage;
        
        [SerializeField] 
        private Image m_SitImage;

        [SerializeField] 
        private Sprite m_OccupierPersonSprite;
        
        private Sprite m_OurPersonSprite;
        
        public bool IsOccupied => m_IsOccupied;
        public bool IsActive;

        private bool m_Acquired;

        private bool m_OnTheWayToOccupied;
        
        private Conditional m_SeatOccupyCondition;
        
        private SubwaySceneManager m_SubwaySceneManager;
        
        public void Initialize(SubwaySceneManager subwaySceneManager)
        {
            m_OurPersonSprite = GameManager.Instance.CurrentCharacter.SittingAnimationSprites.First();
            
            m_SeatOccupyCondition = null;
            m_Acquired = false;
            m_IsOccupied = false;
            m_SeatIndicatorImage.color = Color.clear;
            m_SeatIndicatorImage.enabled = true;

            m_SitImage.enabled = false;
            
            IsActive = true;
            m_SubwaySceneManager = subwaySceneManager;
            
        }
        private void Update()
        {
            if (!IsActive)
            {
                return;
            }
            
            if (!m_IsOccupied && m_PointerIn && !m_Acquired && !m_OnTheWayToOccupied)
            {
                m_OnTheWayToOccupied = true;
                m_SeatIndicatorImage.color = Color.grey;
                m_SeatOccupyCondition = Conditional.Wait(0.5f).Do(OccupySeat);
            }
            if (!m_IsOccupied && m_Selected && !m_Acquired)
            {
                GetSeat();
            }
        }

        private void OccupySeat()
        {
            if(m_Acquired) 
                return;
            
            Debug.Log("Seat Lost");
            
            m_IsOccupied = true;
            m_SubwaySceneManager.OnSeatLost();
            
            m_SeatIndicatorImage.color = Color.clear;

            m_SitImage.sprite = m_OccupierPersonSprite;
            m_SitImage.enabled = true;
        }

        public void GetSeat()
        {
            m_Acquired = true;
            m_SeatOccupyCondition?.Cancel();
            m_SubwaySceneManager.OnSeatAcquired();
            
            m_SeatIndicatorImage.color = Color.clear;
            
            m_SitImage.sprite = m_OurPersonSprite;
            m_SitImage.enabled = true;
            
        }
    }
}
