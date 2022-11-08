using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandamFood : MonoBehaviour
{
    [SerializeField] private FoodDataBase _foodDataBase;
    [SerializeField] private GameObject _canvas;

    private GenerationFood _generationFood;

    private int _id;

    private Image[] _icons;
    private int[] _foodIds = new int[50];

    // Start is called before the first frame update
    void Start()
    {
        _generationFood = GetComponent<GenerationFood>();

        _icons = new Image[_canvas.transform.childCount];

        for(int i = 0; i < _foodIds.Length; i++)
        {
            _foodIds[i] = Random.Range(0, _foodDataBase._foods.Count);
        }

        for(int i = 0; i < _generationFood._foods.Length; i++)
        {
            _icons[i] = _canvas.transform.GetChild(i).GetComponent<Image>();
            _icons[_id].sprite = _foodDataBase._icons[_foodIds[_id]];
            _generationFood.GenerateFood(_foodDataBase._foods[_foodIds[_id]]);
            _id += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _generationFood.Eat();

        if(_generationFood.GetLimitOrder())
        {
            _generationFood.GenerateFood(_foodDataBase._foods[_foodIds[_id]]);
            _id += 1;
            if (_id > _foodIds.Length - 1) _id = _foodIds.Length - 1;
        }

        for(int i = 0; i < _icons.Length; i++)
        {
            _icons[i].sprite = _foodDataBase._icons[_foodIds[_id + i]];
        }

    }
}
