using System;
using UnityCommon.Modules;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Mechanics
{
    public class SubwaySeat : SelectableObject
    {
        [SerializeField] 
        private bool m_IsOccupied;
        
        [SerializeField]
        private Image m_SeatImage;

        public bool IsOccupied => m_IsOccupied;
        public bool IsActive;

        private bool m_Acquired;

        private bool m_OnTheWayToOccupied;
        
        private Conditional m_SeatOccupyCondition;
        
        private SubwaySceneManager m_SubwaySceneManager;
        
        public void Initialize(SubwaySceneManager subwaySceneManager)
        {
            m_SeatOccupyCondition = null;
            m_Acquired = false;
            m_IsOccupied = false;
            m_SeatImage.color = Color.white;
            m_SeatImage.enabled = true;
            IsActive = true;
            m_SubwaySceneManager = subwaySceneManager;
            
        }

        public void SetOccupied(bool isOccupied)
        {
            m_IsOccupied = isOccupied;
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
                Debug.Log("Seat Intention");
                m_SeatOccupyCondition = Conditional.Wait(0.5f).Do(OccupySeat);
            }
            if (!m_IsOccupied && m_Selected && !m_Acquired)
            {
                m_Acquired = true;
                m_SeatOccupyCondition?.Cancel();
                m_SeatImage.color = Color.blue;
                m_SubwaySceneManager.OnSeatAcquired();
            }
        }

        private void OccupySeat()
        {
            if(m_Acquired) 
                return;
            
            Debug.Log("Seat Lost");
            
            m_IsOccupied = true;
            m_SeatImage.color = Color.red;
            m_SubwaySceneManager.OnSeatLost();
        }
    }
}
