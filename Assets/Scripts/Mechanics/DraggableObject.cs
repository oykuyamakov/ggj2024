using System;
using UnityEngine;

namespace Mechanics
{
    public class DraggableObject : SelectableObject
    {
        [SerializeField]
        private bool m_ReturnToInitialPosition = true;
        
        [SerializeField]
        private bool m_Draggable = true;

        private Vector3 m_InitPos;

        private void Start()
        {
            m_InitPos = transform.position;
        }

        private void Update()
        {
            if (m_Selected)
            {
                transform.position = Input.mousePosition;
            }else if (m_ReturnToInitialPosition)
            {
                transform.position = m_InitPos;
            }
        }
    }
}
