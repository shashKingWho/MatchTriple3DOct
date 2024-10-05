using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InventoryModel inventoryModel;

    [Space]

    public Transform[] inventorySlots;   // Transforms representing the positions of inventory slots
    public float moveSpeed = 5f;         // Speed for item movement


    // Start is called before the first frame update
    void Start()
    {
        inventoryModel = FindObjectOfType<InventoryModel>();
        inventoryModel.InventoryModelCreate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function called when the player clicks on a 3D item
    public void OnItemClicked(string itemType)
    {
        Item newItem = new Item(itemType);

        // Check if the item is already in the inventory
        if (inventoryModel.IsItemInInventory(itemType))
        {
            // Push the current selected item into the next available similar slot
            ShiftItemToRight(newItem);
        }
        else
        {
            // Add to the first available empty slot
            // AddNewItemToInventory(newItem);
        }

        if (inventoryModel.CheckForMatches())
        {
            // Handle the matching logic and animation here
            HandleMatch();
        }
        else if (inventoryModel.IsInventoryFull())
        {
            // Trigger game over if no match and the inventory is full
            FindObjectOfType<GameController>().GameOver();
        }
    }


    // Add item to the first available empty slot
    // Function to handle adding new item to the inventory
    public void AddNewItemToInventory(Item newItem, GameObject itemGameObject)
    {
        if (inventoryModel.AddItem(newItem))
        {
            // Find the next available empty slot and move the GameObject there
            int emptySlotIndex = inventoryModel.GetEmptySlotIndex();
            StartCoroutine(MoveItemToSlot(itemGameObject, inventorySlots[emptySlotIndex].position));
        }
        else
        {
            Debug.Log("Inventory is full, game over condition triggered.");
            FindObjectOfType<GameController>().GameOver();
        }
    }

    // Coroutine to move the item to the inventory slot using Lerp
    private IEnumerator MoveItemToSlot(GameObject itemGameObject, Vector3 targetPosition)
    {
        Vector3 startPosition = itemGameObject.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            itemGameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        itemGameObject.transform.position = targetPosition;  // Ensure it reaches the exact target position
    }



    // Shifts the item to the right if it's already in the inventory
    private void ShiftItemToRight(Item newItem)
    {
        int slotIndex = inventoryModel.GetRightmostItemSlot(newItem.Type);

        if (slotIndex != -1)
        {
            // Push current item to the next slot and shift others if necessary
            inventoryModel.ShiftItems(slotIndex);
            inventoryModel.AddItemToSlot(newItem, slotIndex + 1);
            // Add animation to show item moving to next slot
            for (int i = 0; i < inventoryModel.Items.Count; i++)
            {
                // if (inventoryModel.Items[i] != null)
                // {
                //     GameObject itemGameObject = inventoryModel.Items[i].gameObject; // Assuming item holds a reference to its GameObject
                //     StartCoroutine(MoveItemToSlot(itemGameObject, inventorySlots[i].position));
                // }
            }
        }
    }

    // Handles the match function and animation
    private void HandleMatch()
    {
        // Trigger any match animations here
        inventoryModel.ClearMatchedItems();
        inventoryModel.ShiftItemsToLeft(); // Shift items after match
    }

}
