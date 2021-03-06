﻿using System.Collections;
using System.Collections.Generic;
using QuantumTek.QuantumInventory;
using UnityEngine;

public class InteractablePickUp : Interactable
{
    QI_Item interactableItem;

    QI_Inventory inventoryToAddTo;

    bool addedToInventory;

    public override void Start()
    {
        base.Start();
        interactableItem = GetComponent<QI_Item>();
    }

    public override void Interact(GameObject interactor)
    {
        base.Interact(interactor);

        StartCoroutine(InteractCo(interactor));

        
    }

    IEnumerator InteractCo(GameObject interactor)
    {
        interactor.GetComponent<AnimatePlayer>().TriggerPickUp();
        yield return new WaitForSeconds(0.33f);
        PlayInteractSound();

        
        if(PlayerInformation.instance.playerInventory.AddItem(interactableItem.Data, 1))
            Destroy(gameObject);
        hasInteracted = false;

        WorldItemManager.instance.RemoveItemFromWorldItemDictionary(interactableItem.Data.Name, 1);
        
            
    }
   
    void PlayInteractSound()
    {
        if (audioManager.CompareSoundNames("PickUp-" + interactSound))
        {
            audioManager.PlaySound("PickUp-" + interactSound);
        }
    }

}
