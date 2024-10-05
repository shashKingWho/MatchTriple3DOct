using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform itemParent;
    private int itemsPerGroup = 3;      // For matching of 3 items
    public int numberOfGroups = 5;      // Total number of groups to spawn

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
            groupCenter.y = 0.2f; // Ensure group spawns higher than the ground level for physics effect

            // Spawn items within the group
            for (int i = 0; i < itemsPerGroup; i++)
            {
                // Generate a random position near the group center
                Vector3 itemPosition = groupCenter + Random.insideUnitSphere * 1f; // Small offset within the group
                itemPosition.y = 1f;  // Ensure items spawn slightly above the ground for physics effects

                // Instantiate 
                GameObject newItem = Instantiate(itemPrefab, itemPosition, Quaternion.identity);
                newItem.transform.parent = itemParent;

                // Animate 
                newItem.transform.localScale = Vector3.zero; // Start with scale of zero
                newItem.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce); // Scale to original size


                // Assign the item type based on the group number
                int typeIndex = group % newItem.GetComponent<ItemType>().items.Length; // Using modulo to cycle through types
                newItem.GetComponent<ItemType>().SetItemType(typeIndex); 



                // Track spawned items
                spawnedItems.Add(newItem);
            }
        }
    }

    public void RemoveNullItems()
    {
        //Null items removed from Tracked spawned items!!
        spawnedItems.RemoveAll(item => item == null);
    }

}
