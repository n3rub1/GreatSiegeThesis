using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryItem[] inventoryItems;

    public LootInteraction lootSelected { get; set; }

    private int RNGInventoryLoot;
    private int RNGInventoryLootAmount;
    private bool isPlayerInRangeOfLoot = false;

    private PlayerActions actions;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Loot.LootInteraction.performed += ctx => LootRNG();
    }

    public void LootRNG()
    {
        if (!isPlayerInRangeOfLoot || lootSelected == null) return;

        RNGInventoryLoot = Random.Range(0, inventoryItems.Length);
        RNGInventoryLootAmount = Random.Range(0, 3);

        inventory.AddItem(inventoryItems[RNGInventoryLoot], RNGInventoryLootAmount);

        lootSelected.DestroyObject();

    }

    public void SetPlayerInRange(bool value)
    {
        isPlayerInRangeOfLoot = value;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

}
