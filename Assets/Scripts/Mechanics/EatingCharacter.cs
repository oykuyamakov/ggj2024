using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterImplementations;
using Roro.Scripts.GameManagement;

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
        //all the sprites are list, to avoid confusion, use First() to get the first sprite in the list if it is a single sprite
        spriteRenderer.sprite = m_CurrentCharacter.OpenMouseAnimationSprites.First();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
