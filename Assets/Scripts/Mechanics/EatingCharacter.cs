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

    [SerializeField] 
    private SpriteRenderer m_SpriteRenderer;

    private void Awake()
    {
        m_CurrentCharacter = GameManager.Instance.CurrentCharacter;
        
        Debug.Log("wtf is wrong amk");
        
        //all the sprites are list, to avoid confusion, use First() to get the first sprite in the list if it is a single sprite
        m_SpriteRenderer.sprite = m_CurrentCharacter.OpenMouseAnimationSprites.First();
        
        Debug.Log(m_CurrentCharacter.name);
        
        AnimateEat();

    }

    void Start()
    {
       
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
            m_SpriteRenderer.sprite = m_CurrentCharacter.EatingAnimationSprites[index];
        });
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
