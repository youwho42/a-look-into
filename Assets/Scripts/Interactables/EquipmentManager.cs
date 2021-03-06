﻿using QuantumTek.QuantumInventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour
{
    public QI_ItemData[] currentEquipment;
    int totalSlots;
    public static EquipmentManager instance;
    public UnityEvent EventUIUpdateEquipment;
    public SpriteRenderer handEquipmentHolder;
    public SpriteRenderer beltEquipmentHolder;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        totalSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new QI_ItemData[totalSlots];
    }

    public void Equip(QI_ItemData newItem, int equipedIndex)
    {
        currentEquipment[equipedIndex] = newItem;
        if (equipedIndex == (int)EquipmentSlot.Hands)
            handEquipmentHolder.sprite = newItem.Icon;
        if (equipedIndex == (int)EquipmentSlot.Extra)
            beltEquipmentHolder.sprite = newItem.Icon;
        EventUIUpdateEquipment.Invoke();
    }
  
    public bool UnEquipToInventory(QI_ItemData itemData, int equipedIndex)
    {
        if(PlayerInformation.instance.playerInventory.AddItem(itemData, 1))
        {
            currentEquipment[equipedIndex] = null;
            if (equipedIndex == (int)EquipmentSlot.Hands)
                handEquipmentHolder.sprite = null;
            EventUIUpdateEquipment.Invoke();

            return true;
        }
        return false;
    }

    public void UnEquipAndDestroy(int equipedIndex)
    {
        if (equipedIndex == (int)EquipmentSlot.Hands)
            handEquipmentHolder.sprite = null;
        currentEquipment[equipedIndex] = null;
        EventUIUpdateEquipment.Invoke();
    }
    public void UnEquipAndDestroyAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            currentEquipment[i] = null;

        }
        handEquipmentHolder.sprite = null;
        EventUIUpdateEquipment.Invoke();
    }
}
