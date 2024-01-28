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
    private Image m_CharImage;
    
    [SerializeField] 
    private Character m_Char;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_CharImage.sprite = m_Char.SelectedAnimationSprites.First();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_CharImage.sprite = m_Char.IdleAnimationSprites.First();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        m_CharImage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        
        GameManager.Instance.ChangeCurrentCharacter(m_Char);
    }
}
