using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{

    public int itemType = 3;
    public GameObject[] items;
    [SerializeField] 
    Sprite[] spriteList; 
    public Sprite itemIcon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Assign the item type, used for identifying matches
    public void SetItemType(int type)
    {
        itemType = type;
        foreach (GameObject item in items)
        {
            item.SetActive(false);            
        }
        items[itemType].SetActive(true);
        itemIcon= spriteList[type];
    }

}
