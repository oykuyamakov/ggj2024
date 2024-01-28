using System.Collections;
using System.Linq;
using CharacterImplementations;
using Roro.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharSelectImage : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    [SerializeField]
    private Canvas m_DateSelectionCanvas;
    
    [SerializeField] 
    private Image m_CharImage;
    
    [SerializeField] 
    private Character m_Char;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.Instance.CharSelected)
            return;

        //m_CharImage.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        
        m_CharImage.sprite = m_Char.SelectedAnimationSprites.First();
        
        m_CharImage.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(GameManager.Instance.CharSelected)
            return;
        
        m_CharImage.sprite = m_Char.IdleAnimationSprites.First();
        
        m_CharImage.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.CharSelected)
            return;
        
        
        m_CharImage.transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        
        GameManager.Instance.ChangeCurrentCharacter(m_Char);

        m_DateSelectionCanvas.enabled = true;
    }
}
