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

        private bool m_Acquired;

        private Conditional m_SeatOccupyCondition;

        public void SetOccupied(bool isOccupied)
        {
            m_IsOccupied = isOccupied;
        }

        private void Update()
        {
            if (!m_IsOccupied && m_PointerIn && !m_Acquired)
            {
                m_SeatOccupyCondition = Conditional.Wait(0.5f).Do(OccupySeat);
            }
            if (!m_IsOccupied && m_Selected)
            {
                m_Acquired = true;
                m_SeatOccupyCondition?.Cancel();
                m_SeatImage.color = Color.blue;
            }
        }

        private void OccupySeat()
        {
            if(m_Acquired) 
                return;
            
            m_IsOccupied = true;
            m_SeatImage.color = Color.red;
        }
    }
}
