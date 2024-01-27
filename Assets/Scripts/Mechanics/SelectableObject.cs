using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Mechanics
{
    public class SelectableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
    {
        protected bool m_PointerIn;
        protected bool m_Selected;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Pointer Enterp");
            m_PointerIn = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Pointer Exit");
            m_PointerIn = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pointer Click");
            m_Selected = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer Up");
            m_Selected = false;
        }
    }
}
