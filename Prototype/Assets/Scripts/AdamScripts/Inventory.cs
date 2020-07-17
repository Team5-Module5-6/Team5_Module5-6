using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public RawImage[] partIcons;
    public RawImage[] noteIcons;
    public RawImage key;

    [HideInInspector]
    public bool hasKey;

    private int partsFound;
    private int notesFound;

    private void Start()
    {
        partsFound = 0;
        notesFound = 0;
    }

    private void Update()
    {
        for (int i = 0; i < partIcons.Length; i++)
        {
            if(partsFound - 1 >= i)
            {
                partIcons[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < noteIcons.Length; i++)
        {
            if(notesFound - 1 >= i)
            {
                noteIcons[i].gameObject.SetActive(true);
            }
        }

        if (hasKey) key.gameObject.SetActive(true);
        else key.gameObject.SetActive(false);
    }

    public void FoundPart()
    {
        partsFound++;
    }

    public void FoundNote()
    {
        notesFound++;
    }
}
