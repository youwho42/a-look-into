﻿using QuantumTek.QuantumInventory;
using QuantumTek.QuantumAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    public static PlayerInformation instance;

    public Transform player;
    public QI_Inventory playerInventory;
    public bool uiScreenVisible;
    
    public PlayerInput playerInput;

    public PlayerStats playerStats;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
   

    public void TogglePlayerInput(bool toggleOn)
    {
        playerInput.enabled = toggleOn;
    }
}
