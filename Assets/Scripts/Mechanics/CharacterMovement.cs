using System;
using System.Collections;
using System.Collections.Generic;
using CharacterImplementations;
using UnityCommon.Runtime.Extensions;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    List<Character> m_Characters;
    
    [SerializeField]
    private Character m_CurrentCharacter;

    private List<Sprite> m_CurrentAnim;
    
    private WalkDir m_CurrentWalkDir = WalkDir.Down;

    private void Awake()
    {
        m_CurrentCharacter = m_Characters[0];
        
        m_CurrentAnim = m_CurrentCharacter.IdleAnimationSprites;
    }

    private void Update()
    {
        ControlChar();
        
        if (Input.GetKey(KeyCode.S))
        {
            m_CurrentWalkDir = WalkDir.Up;
            
            m_CurrentAnim = m_CurrentCharacter.WalkFrontAnimationSprites;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            m_CurrentWalkDir = WalkDir.Down;
            
            m_CurrentAnim = m_CurrentCharacter.WalkBackAnimationSprites;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_CurrentWalkDir = WalkDir.Left;
            
            m_CurrentAnim = m_CurrentCharacter.WalkLeftAnimationSprites;
            
            transform.localScale = transform.localScale.WithX(1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_CurrentWalkDir = WalkDir.Right;
            
            m_CurrentAnim = m_CurrentCharacter.WalkLeftAnimationSprites;
            transform.localScale = transform.localScale.WithX(-1);
        }
        else
        {
            switch (m_CurrentWalkDir)
            {
                case WalkDir.Right:
                    m_CurrentAnim =m_CurrentCharacter.IdleLeftAnimationSprites;
                    break;
                case WalkDir.Left:
                    m_CurrentAnim =m_CurrentCharacter.IdleLeftAnimationSprites;
                    break;
                case WalkDir.Up:
                    m_CurrentAnim =m_CurrentCharacter.IdleBackAnimationSprites;
                    break;
                case WalkDir.Down:
                    m_CurrentAnim =m_CurrentCharacter.IdleAnimationSprites;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        Animate();
    }
    
    private void Animate()
    {
        int index = (int) (Time.time * 5) % m_CurrentAnim.Count;
        GetComponent<SpriteRenderer>().sprite = m_CurrentAnim[index];
    }

    private void ControlChar()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // if (transform.localScale.x > 0.5f)
            // {
                transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f) * Time.deltaTime;
            //}
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // if (transform.localScale.x < 1f)
            // {
                transform.localScale += new Vector3(0.3f, 0.3f, 0.3f) * Time.deltaTime;
            //}
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime;
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }
}

public enum WalkDir
{
    Right,
    Left,
    Up,
    Down
}