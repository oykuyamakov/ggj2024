using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CharacterImplementations;
using Roro.Scripts.GameManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DateSelectionImage : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{  
    [SerializeField] 
    private Image m_DateImage;
    
    [SerializeField] 
    private DateChar m_Date;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(GameManager.Instance.DateChar)
            return;

        //m_CharImage.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        m_DateImage.sprite = m_Date.NormalSprite;
        
        m_DateImage.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(GameManager.Instance.DateChar)
            return;
        
        m_DateImage.transform.localScale = new Vector3(1f, 1f, 1f);


        m_DateImage.sprite = m_Date.NormalSprite;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.Instance.DateChar)
            return;
        
        
        m_DateImage.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        
        GameManager.Instance.ChangeCurrentDate(m_Date);
    }
}
