using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> m_IdleAnimationSprites;
    [SerializeField]
    private List<Sprite> m_WalkAnimationSprites;
    [SerializeField]
    private List<Sprite> m_WalkBackAnimationSprites;
    [SerializeField]
    private List<Sprite> m_WalkFrontAnimationSprites;

    private List<Sprite> m_CurrentAnim;

    private void Awake()
    {
        m_CurrentAnim = m_IdleAnimationSprites;
    }

    private void Update()
    {
        ControlChar();
        
        if (Input.GetKey(KeyCode.W))
        {
            m_CurrentAnim = m_WalkFrontAnimationSprites;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            m_CurrentAnim = m_WalkBackAnimationSprites;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            m_CurrentAnim = m_WalkAnimationSprites;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            m_CurrentAnim = m_WalkAnimationSprites;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            m_CurrentAnim = m_IdleAnimationSprites;
        }
        
        Animate();
    }
    
    private void Animate()
    {
        int index = (int) (Time.time * 10) % m_CurrentAnim.Count;
        GetComponent<SpriteRenderer>().sprite = m_CurrentAnim[index];
    }

    private void ControlChar()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.localScale.x > 0.5f)
            {
                transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (transform.localScale.x < 1f)
            {
                transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
            }
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
