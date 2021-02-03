﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteForItems : MonoBehaviour
{
    [System.Serializable]
    public struct ItemSprites
    {
        public Sprite item;
        public Sprite shadow;
    }

    public SpriteRenderer mainGrass;
    public SpriteRenderer mainShadow;

    public List<ItemSprites> itemSpritesList = new List<ItemSprites>();

    private void Start()
    {
        SetRandomSprites();
    }

    private void SetRandomSprites()
    {
        int i = UnityEngine.Random.Range(0, itemSpritesList.Count);
        if(mainGrass != null)
            mainGrass.sprite = itemSpritesList[i].item;
        if (mainShadow != null)
            mainShadow.sprite = itemSpritesList[i].shadow;
    }
}
