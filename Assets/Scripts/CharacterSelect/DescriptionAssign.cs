using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DescriptionAssign : MonoBehaviour
{
    public TextMeshProUGUI title, description;
    public Image favouriteColour;

    public void AssignDescription (CharacterDescriptionScriptableObject descriptionObject)
    {
        title.text = descriptionObject.Title;
        description.text = descriptionObject.Description;
        favouriteColour.color = descriptionObject.FavouriteColour;
    }
}
