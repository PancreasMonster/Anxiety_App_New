using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CharacterDescriptionScriptableObject", order = 1)]
public class CharacterDescriptionScriptableObject : ScriptableObject
{
    public string Title;
    public string Description;
    public Color FavouriteColour = new Color(0,0,0,1);
}
