using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_DateChar", menuName = "DateChar", order = 0)]
public class DateChar : ScriptableObject
{
    [SerializeField] private Sprite m_NormalSprite;
    [SerializeField] private Sprite m_SittingSprite;
    
    public Sprite NormalSprite => m_NormalSprite;
    public Sprite SittingSprite => m_SittingSprite;
    

}
