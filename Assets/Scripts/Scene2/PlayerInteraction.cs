using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;  // Reference to the inventory manager
    [Space]
    [Space]
    public LayerMask ClickableLayer;


    // Start is called before the first frame update
    void Start()
    {

        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))  // Left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ClickableLayer))
            {
                ItemType clickedItem = hit.collider.GetComponent<ItemType>();

                if (clickedItem != null)
                {
                    inventoryManager.AddItemToInventory(clickedItem);

                }
            }
        }
    }
}
