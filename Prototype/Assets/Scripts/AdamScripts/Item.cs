using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum itemType
    {
        GeneratorPart,
        LabNote,
        Key
    }

    [SerializeField]
    public itemType current;

    public string itemName;
    public Sprite icon = null;
}
