using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using MechanicEvents;
using Mechanics;
using Roro.Scripts.GameManagement;
using UnityEngine;

public class SubwaySceneManager : MonoBehaviour
{
    [SerializeField] 
    private List<SubwaySeat> m_SubwaySeats;
    
    private int m_RequiredSeats = 1;
    
    private int m_AvailableSeats = 0;
    
    private int m_AcquiredSeats = 0;

    private void Awake()
    {
        InitializeSubway();
    }

    private void InitializeSubway()
    {
        m_RequiredSeats = GameManager.Instance.CurrentSceneIndex > 2 ? 2 : 1;
        
        m_AvailableSeats = GameManager.Instance.CurrentSceneIndex > 2 ? 5 : 3;
        
        for (int i = 0; i < m_AvailableSeats; i++)
        {
            m_SubwaySeats[i].Initialize(this);
        }
    }

    public void OnSeatAcquired()
    {
        m_AcquiredSeats++;
        if (m_AcquiredSeats == m_RequiredSeats)
        {
            PauseSeats();
            
            using var evt = MechanicResultEvent.Get(true);
            evt.SendGlobal();
        }
    }

    public void OnSeatLost()
    {
        m_AvailableSeats--;

        if (m_AvailableSeats == 0)
        {
            PauseSeats();
            
            using var evt = MechanicResultEvent.Get(false);
            evt.SendGlobal();
        }
    }
    
    private void PauseSeats()
    {
        foreach (var seat in m_SubwaySeats)
        {
            seat.IsActive = false;
        }
    }
}
