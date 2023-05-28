﻿using UnityEngine;

class UpgradeInventoryUI : InventoryUI
{
    [SerializeField] private Player player;

    protected override void Start()
    {
        Inventory = player.UpgradeInventory;
        base.Start();
    }
}