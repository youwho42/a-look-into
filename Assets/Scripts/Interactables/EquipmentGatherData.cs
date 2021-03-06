﻿using QuantumTek.QuantumInventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum Tek/Quantum Inventory/Equipment Item/GathererItem", fileName = "New Gatherer Item")]
public class EquipmentGatherData : EquipmentData
{
    public List<QI_ItemData> gatherItemData = new List<QI_ItemData>();
    public MiniGameType miniGameType;
    public LayerMask gathererLayer;
    public float detectionRadius;
    public bool takeSample;
    public bool oneUse;
    public override void UseEquippedItem()
    {
        base.UseEquippedItem();
        Collider2D[] hit = Physics2D.OverlapCircleAll(PlayerInformation.instance.player.position, detectionRadius, gathererLayer);
        if (hit.Length > 0)
        {
            GetNearestItem(hit);
        }
    }

    public void GetNearestItem(Collider2D[] colliders)
    {
        // Find nearest item.
        Collider2D nearest = null;
        float distance = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            
            float tempDistance = Vector3.Distance(PlayerInformation.instance.player.position, colliders[i].transform.position);
            if (nearest == null || tempDistance < distance)
            {
                nearest = colliders[i];
                distance = tempDistance;
            }
            

        }
        if (nearest != null)
        {
            if (miniGameType == MiniGameType.None)
            {

                if (nearest.gameObject.TryGetComponent(out QI_Item nearestItem))
                {
                    for (int i = 0; i < gatherItemData.Count; i++)
                    {
                        if (gatherItemData[i] != nearestItem.Data)
                        {
                            continue;
                        }
                        if (PlayerInformation.instance.playerInventory.AddItem(nearestItem.Data, 1))
                        {

                            if (!takeSample)
                                Destroy(nearest.gameObject);
                            else
                                EquipmentManager.instance.UnEquipAndDestroy(0);
                        }

                    }
                }
                else if (nearest.gameObject.TryGetComponent(out GatherableItem nearestItemList))
                {
                    foreach (QI_ItemData itemData in nearestItemList.dataList)
                    {
                        for (int i = 0; i < gatherItemData.Count; i++)
                        {
                            if (gatherItemData[i] != itemData)
                            {
                                continue;
                            }
                            if (nearestItemList.RemoveItem())
                            {
                                if (PlayerInformation.instance.playerInventory.AddItem(itemData, 1))
                                {

                                    if (!takeSample)
                                    {
                                        Destroy(nearest.gameObject);
                                    }
                                    else if (oneUse)
                                    {

                                        EquipmentManager.instance.UnEquipAndDestroy(0);
                                    }

                                }
                            }

                        }
                    }
                }
            }
            else
            {

                if (nearest.gameObject.TryGetComponent(out GatherableItem nearestItemList))
                {
                    if (!nearestItemList.hasBeenHarvested)
                    {
                        foreach (QI_ItemData itemData in nearestItemList.dataList)
                        {
                            for (int i = 0; i < gatherItemData.Count; i++)
                            {
                                if (gatherItemData[i] != itemData)
                                {
                                    continue;
                                }
                                MiniGameManager.instance.StartMiniGame(miniGameType, itemData, nearest.gameObject);
                                nearestItemList.hasBeenHarvested = true;
                            }
                        }
                        
                    }
                }
                if (nearest.gameObject.TryGetComponent(out QI_Item nearestItem))
                {
                    foreach (QI_ItemData itemData in gatherItemData)
                    {
                        if(itemData == nearestItem.Data)
                            MiniGameManager.instance.StartMiniGame(miniGameType, nearestItem.Data, nearest.gameObject);
                    }
                }
            }
        }
        

    }

}
