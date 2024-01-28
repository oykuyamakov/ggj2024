using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterImplementations;
using GameManager;

public class EatingCharacter : MonoBehaviour
{
    private Character m_CurrentCharacter;

    private void Awake()
    {
        Character m_CurrentCharacter = GameManager.Instance.CurrentCharacter;
    }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = m_CurrentCharacter.OpenMouseAnimationSprites;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
