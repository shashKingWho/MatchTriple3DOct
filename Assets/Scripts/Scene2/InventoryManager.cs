using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public int inventorySize = 7;            // Maximum number of inventory slots
    public GameObject slotPrefab;            // Prefab for each inventory slot
    public Transform slotParent;             // Parent object to hold slots in the UI
    public GameObject itemSpritePrefab;      // Prefab for the item sprites
    [SerializeField]
    private List<GameObject> slots = new List<GameObject>();  // List to store slot GameObjects
    [SerializeField]
    private List<ItemType> inventoryItems = new List<ItemType>();     // List to store items in inventory
private bool isGameWon = false; 

private ObjectSpawner objectSpawner;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize empty slots
        for (int i = 0; i < inventorySize; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            slots.Add(newSlot);
        }

         objectSpawner = FindObjectOfType<ObjectSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
                CheckWinCondition();

    }

    public void AddItemToInventory(ItemType newItem)
    {
        if (inventoryItems.Count >= inventorySize)
        {
            Debug.Log("Inventory full! Can't add more items.");
            CheckLoseCondition();
            return;
        }



        // Find if similar item exists and place new item to the rightmost of similar types
        int rightmostIndex = FindRightmostIndexOfSimilarType(newItem.itemType);

        if (rightmostIndex == -1)  // No similar type found, add "at the end"
        {
            inventoryItems.Add(newItem);
        }
        else  // Place item at the rightmost position of the similar type
        {
            inventoryItems.Insert(rightmostIndex + 1, newItem);
        }



        // Update UI by placing item sprite in the next available slot
        UpdateInventoryUI(newItem);

        // Destroy the 3D item in the world
        Destroy(newItem.gameObject);
        // Check for matches after adding the item
        CheckForMatches();
        // print("What is the inventoryItems count" + inventoryItems.Count);
                CheckWinCondition();

    }


    // Find the rightmost index of a similar item type
    private int FindRightmostIndexOfSimilarType(int itemType)
    {
        int rightmostIndex = -1;

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].itemType == itemType)
            {
                rightmostIndex = i;  // Keep updating until we find the rightmost one
            }
        }

        return rightmostIndex;
    }

    // Update the inventory UI by placing item sprite in the next available slot
    void UpdateInventoryUI(ItemType newItem)
    {
        int nextSlotIndex = inventoryItems.Count - 1;  // Find the next available slot
        GameObject slot = slots[nextSlotIndex];        // Get the corresponding slot
        GameObject itemSprite = Instantiate(itemSpritePrefab, slot.transform.position, Quaternion.identity);
        itemSprite.AddComponent<Image>().sprite = newItem.itemIcon;  // Assign the item's sprite
        itemSprite.transform.SetParent(slot.transform);  // Attach the sprite to the slot
    }

    // Check for 3 matching items
    void CheckForMatches()
    {
        Dictionary<int, List<ItemType>> matchingGroups = new Dictionary<int, List<ItemType>>();

        // Group items by their type
        foreach (ItemType item in inventoryItems)
        {
            if (!matchingGroups.ContainsKey(item.itemType))
            {
                matchingGroups[item.itemType] = new List<ItemType>();
            }
            matchingGroups[item.itemType].Add(item);
        }

        // Find groups with 3 or more items and destroy them
        foreach (var group in matchingGroups)
        {
            if (group.Value.Count >= 3)
            {
                DestroyMatchingItems(group.Value);
                ShiftInventory();
                CheckWinCondition();
                break;
            }
        }
    }

    // Destroy matching items and shift the inventory left
   
void DestroyMatchingItems(List<ItemType> matchedItems)
{
    foreach (ItemType item in matchedItems)
    {
        int index = inventoryItems.IndexOf(item);  // Get the index of the item in inventory

        if (index >= 0)  // Check if item is valid and exists in the inventory
        {
            inventoryItems.RemoveAt(index);  // Remove item from inventory list
            ClearSlot(index);  // Clear the corresponding slot UI
        }
        
        // Destroy the actual item object in the scene
        if (item != null)
        {
            Destroy(item.gameObject);
        }
    }

    Debug.Log("YAHOO00000! MATCHED items destroyed and slots cleared!");
    
    // After clearing items, shift the inventory to fill empty slots
    ShiftInventory();
}



    // Clear the sprite from the corresponding inventory slot
void ClearSlot(int slotIndex)
{
    GameObject slot = slots[slotIndex];
    
    if (slot.transform.childCount > 0)  // If there is an item in the slot
    {
        // Destroy the child object (which is the item sprite)
        Destroy(slot.transform.GetChild(0).gameObject);
    }
}

    // Shift remaining items left after a match
    void ShiftInventory()
    {
        int slotIndex = 0;
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i] != null)  // Only process non-null items
            {
                GameObject slot = slots[i];
                GameObject itemSprite = slot.transform.GetChild(0).gameObject;
                itemSprite.transform.position = slot.transform.position;
                slotIndex++;  // Move to next slot
            }
        }

        // Clear the remaining empty slots
        for (int i = inventoryItems.Count; i < inventorySize; i++)
        {
            GameObject slot = slots[i];

            // If there is an item sprite in the slot, destroy it
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        Debug.Log("Inventory shifted left.");
    }

    // Check the win condition: all items matched
    void CheckWinCondition()
    {

    if (isGameWon) return; // Exit if the game is already won

        if (objectSpawner.spawnedItems.Count==0 && inventoryItems.Count == 0)
        {
            Debug.Log("You won the game!");
            isGameWon = true; 
            GameController.Instance.GameWin();
        }
    }

    // Check the lose condition if inventory is full with no matches
    void CheckLoseCondition()
    {
        if (inventoryItems.Count >= inventorySize)
        {
            Debug.Log("No matches, and inventory is full. You lose a life!");
            GameController.Instance.GameOver();
        }
    }
}
