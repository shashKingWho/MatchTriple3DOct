using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryModel : MonoBehaviour
{
    public List<Item> Items { get; private set; }
    public const int MaxSlots = 7;

    public void InventoryModelCreate()
    {
        Items = new List<Item>(new Item[MaxSlots]);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Adds item to the inventory
    public bool AddItem(Item newItem)
    {
        int emptySlotIndex = GetEmptySlotIndex();
        if (emptySlotIndex != -1)
        {
            Items[emptySlotIndex] = newItem;
            return true;
        }
        return false; // Inventory full
    }

        // Add item to a specific slot
    public void AddItemToSlot(Item newItem, int slotIndex)
    {
        Items[slotIndex] = newItem;
    }

        // Check if item is already in the inventory
    public bool IsItemInInventory(string itemType)
    {
        foreach (Item item in Items)
        {
            if (item != null && item.Type == itemType)
            {
                return true;
            }
        }
        return false;
    }



    // Returns the rightmost slot index for the given item type
    public int GetRightmostItemSlot(string itemType)
    {
        for (int i = MaxSlots - 1; i >= 0; i--)
        {
            if (Items[i] != null && Items[i].Type == itemType)
            {
                return i;
            }
        }
        return -1; // Item not found
    }


        // Shifts items to the right starting from the given index
    public void ShiftItems(int fromIndex)
    {
        for (int i = MaxSlots - 1; i > fromIndex; i--)
        {
            Items[i] = Items[i - 1];
        }
        Items[fromIndex] = null;
    }

    // Check for matches of 3
    public bool CheckForMatches()
    {
        Dictionary<string, int> itemCount = new Dictionary<string, int>();

        foreach (var item in Items)
        {
            if (item != null)
            {
                if (!itemCount.ContainsKey(item.Type))
                {
                    itemCount[item.Type] = 0;
                }
                itemCount[item.Type]++;
            }
        }

        foreach (var count in itemCount.Values)
        {
            if (count >= 3)
            {
                return true; // Match found
            }
        }
        return false; // No matches
    }




        // Clears matched items from the inventory
    public void ClearMatchedItems()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            if (Items[i] != null && Items.Count(x => x != null && x.Type == Items[i].Type) >= 3)
            {
                Items[i] = null; // Clear matched items
            }
        }
    }
    

    // Shifts items left after a match
    public void ShiftItemsToLeft()
    {
        for (int i = 0; i < MaxSlots - 1; i++)
        {
            if (Items[i] == null)
            {
                for (int j = i + 1; j < MaxSlots; j++)
                {
                    if (Items[j] != null)
                    {

                        Items[i] = Items[j];
                        Items[j] = null;
                        // Shift the item to the left to fill the empty slot
                        
                        break;
                    }
                }
            }
        }}

// Returns the index of the first empty slot in the inventory
    public int GetEmptySlotIndex()
    {
        for (int i = 0; i < MaxSlots; i++)
        {
            if (Items[i] == null)
            {
                return i;
            }
        }
        return -1; // No empty slots
    }

    // Check if the inventory is full (all slots occupied)
    public bool IsInventoryFull()
    {
        return GetEmptySlotIndex() == -1;
    }
}

