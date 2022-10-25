using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "FoodDataBase", menuName = "CreateItemDataBase")]
public class FoodDataBase : ScriptableObject
{
    public List<Sprite> _icons = new List<Sprite>();
    public List<GameObject> _foods = new List<GameObject>();
}
