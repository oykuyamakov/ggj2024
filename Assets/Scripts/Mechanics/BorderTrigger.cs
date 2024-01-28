using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BorderTrigger : MonoBehaviour
{
    [SerializeField][EnumToggleButtons]
    private BorderType m_BorderType;
    
    public BorderType BorderType => m_BorderType;
    
    private bool m_IsTriggered = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ababi sikim");
        
        if (other.gameObject.GetComponent<CharacterMovement>())
        {
            m_IsTriggered = true;
            BathroomSceneManager.Instance.OnBorder(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CharacterMovement>())
        {
            m_IsTriggered = false;
        }
    }
}

public enum BorderType
{
    Left,
    Right,
    Middle
}