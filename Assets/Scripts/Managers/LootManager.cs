using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryItem[] inventoryItems;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private int lootXPNumber = 3;

    public LootInteraction lootSelected { get; set; }

    private int RNGInventoryLoot;
    private int RNGInventoryLootAmount;
    private bool isPlayerInRangeOfLoot = false;
    private int suppliesArrayValue = 0;

    private PlayerActions actions;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
        playerXP = FindObjectOfType<PlayerXP>();
    }

    private void Start()
    {
        actions.Loot.LootInteraction.performed += ctx => LootRNG();
    }

    public void LootRNG()
    {
        if (!isPlayerInRangeOfLoot || lootSelected == null) return;

        playerXP.AddXPSupplies(lootXPNumber);
        int[] levels = playerXP.GetLevels();

        // int RNGInventoryLoot = 2;  TEST TO LOOT IRON ONLY

        RNGInventoryLoot = Random.Range(0, inventoryItems.Length);
        RNGInventoryLootAmount = Random.Range(1, 1 + levels[suppliesArrayValue]);

        inventory.AddItem(inventoryItems[RNGInventoryLoot], RNGInventoryLootAmount);

        lootSelected.ShowLootGained(inventoryItems[RNGInventoryLoot], RNGInventoryLootAmount);

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
