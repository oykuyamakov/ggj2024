using System;
using System.Collections.Generic;
using CharacterImplementations;
using Roro.Scripts.GameManagement;
using UnityCommon.Runtime.Extensions;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    [SerializeField]
    private float m_Speed = 2f;

    private Character m_CurrentCharacter;

    private List<Sprite> m_CurrentAnim;
    
    private WalkDir m_CurrentWalkDir = WalkDir.Down;
    
    public bool CanMove = false;

    private Vector3 m_InitPos;

    private float m_Limit = 8.58f;

    private void Awake()
    {
        m_InitPos = transform.position;
        
        CanMove = true;
        
        m_CurrentCharacter = GameManager.Instance.CurrentCharacter;
        
        m_CurrentAnim = m_CurrentCharacter.IdleAnimationSprites;
    }
    
    public void ResetPos()
    {
        transform.position = m_InitPos;
    }

    private void Update()
    {
        if(!CanMove)
            return;
        
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
            var localScale = transform.localScale;
            localScale = localScale.WithX( Math.Abs(localScale.x));
            transform.localScale = localScale;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_CurrentWalkDir = WalkDir.Right;
            
            m_CurrentAnim = m_CurrentCharacter.WalkLeftAnimationSprites;
            var localScale = transform.localScale;
            localScale = localScale.WithX(-Math.Abs(localScale.x));
            transform.localScale = localScale;
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
                    m_CurrentAnim =m_CurrentCharacter.IdleAnimationSprites;
                    break;
                case WalkDir.Down:
                    m_CurrentAnim =m_CurrentCharacter.IdleBackAnimationSprites;
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
            if (transform.localScale.y > 0.3f)
            {
                var localScale = transform.localScale;
                localScale = localScale.WithX(Math.Abs(localScale.x));
                localScale -= new Vector3(0.3f, 0.3f, 0.3f) * Time.deltaTime;
                transform.localScale = localScale;
            }
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (transform.localScale.y < 2f)
            {
                var localScale = transform.localScale;
                localScale = localScale.WithX(Math.Abs(localScale.x));
                localScale += new Vector3(0.3f, 0.3f, 0.3f) * Time.deltaTime;
                transform.localScale = localScale;
            }
        }
        else if (Input.GetKey(KeyCode.A) && transform.position.x > -m_Limit)
        {
            transform.position += Vector3.left * (Time.deltaTime * m_Speed);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < m_Limit)
        {
            transform.position += Vector3.right * (Time.deltaTime * m_Speed);
        }
    }
}

public enum WalkDir
{
    Right,
    Left,
    Up,
    Down
}