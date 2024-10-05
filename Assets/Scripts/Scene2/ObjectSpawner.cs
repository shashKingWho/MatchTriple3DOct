using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform itemParent;
    private int itemsPerGroup = 3;      // Number of items to spawn in a group
    public int numberOfGroups = 5;        // Total number of groups to spawn

    public float spawnRadius = 5f;
    
    public List<GameObject> spawnedItems = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

        SpawnItemGroup();

    }

    // Update is called once per frame
    void Update()
    {
        RemoveNullItems();
    }

    public void SpawnItemGroup()
    {
        for (int group = 0; group < numberOfGroups; group++)
        {
            // Generate a random center position for the group
            Vector3 groupCenter = transform.position + Random.insideUnitSphere * spawnRadius;
            groupCenter.y = 0f; // Ensure group spawns at ground level

            // Spawn items within the group
            for (int i = 0; i < itemsPerGroup; i++)
            {
                // Generate a random position near the group center
                Vector3 itemPosition = groupCenter + Random.insideUnitSphere * 1f; // Small offset within the group
                itemPosition.y = 1f;  // Ensure items spawn slightly above the ground for physics effects

                // Instantiate the item
                GameObject newItem = Instantiate(itemPrefab, itemPosition, Quaternion.identity);
                newItem.transform.parent = itemParent;

                // // Assign a type to each item 
                // newItem.GetComponent<ItemType>().SetItemType(group);

// // Randomly assign an item type from the list
//             int randomTypeIndex = Random.Range(0, newItem.GetComponent<ItemType>().items.Length);
//             newItem.GetComponent<ItemType>().SetItemType(randomTypeIndex);


                // Assign the item type based on the group number, ensuring it cycles through available types if needed
                int typeIndex = group % newItem.GetComponent<ItemType>().items.Length; // Use modulo to cycle through types
                newItem.GetComponent<ItemType>().SetItemType(typeIndex); // Assign the type based on the group number

            

                // Track spawned items
                spawnedItems.Add(newItem);
            }
        }
    }

    public void RemoveNullItems()
    {
        //Null items removed from inventory
        spawnedItems.RemoveAll(item => item == null);
    }

}
