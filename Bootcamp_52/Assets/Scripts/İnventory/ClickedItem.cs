using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class ClickedItem : MonoBehaviour
{
    public Slot clickedSlot;

    public Color[] rarityColors;

    [SerializeField] RawImage image;

    [SerializeField] TextMeshProUGUI txt_Name;

    private void OnEnable()
    {
        SetUp();
    }

    void SetUp()
    {

        txt_Name.text = clickedSlot.ItemInSlot.name;

        switch (clickedSlot.ItemInSlot.rarity)
        {
            case Items.Rarity.common:
                image.color = rarityColors[0];

                break;
            case Items.Rarity.uncommon:
                image.color = rarityColors[1];

                break;
            case Items.Rarity.rare:
                image.color = rarityColors[2];

                break;
            case Items.Rarity.epic:
                image.color = rarityColors[3];

                break;
            case Items.Rarity.legendary:
                image.color = rarityColors[4];

                break;
        }

        switch (clickedSlot.ItemInSlot.type)
        {
            case Items.Types.misecellaneous:

                break;
            case Items.Types.craftingMaterial:

                break;
            case Items.Types.equipment:

                break;
        }

    }

}
