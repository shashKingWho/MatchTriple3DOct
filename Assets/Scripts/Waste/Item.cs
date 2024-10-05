using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
public string Type { get; private set; }

    public Item(string type)
    {
        Type = type;
    }
}
