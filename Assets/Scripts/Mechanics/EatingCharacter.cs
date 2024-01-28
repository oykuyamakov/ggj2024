using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterImplementations;
using Roro.Scripts.GameManagement;
using UnityCommon.Modules;

public class EatingCharacter : MonoBehaviour
{
    private Character m_CurrentCharacter;

    private void Awake()
    {
        m_CurrentCharacter = GameManager.Instance.CurrentCharacter;
    }

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //all the sprites are list, to avoid confusion, use First() to get the first sprite in the list if it is a single sprite
        spriteRenderer.sprite = m_CurrentCharacter.OpenMouseAnimationSprites.First();

        //Conditional.Wait(15).Do(AnimateEat);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AnimateEat();
    }

    private Conditional con;
    public void AnimateEat()
    {
        con = Conditional.For(2).Do(() =>
        {
            if (!GetComponent<SpriteRenderer>())
            {
                con.Cancel();
                return;
            }
            int index = (int) (Time.time * 5) % m_CurrentCharacter.EatingAnimationSprites.Count;
            GetComponent<SpriteRenderer>().sprite = m_CurrentCharacter.EatingAnimationSprites[index];
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
