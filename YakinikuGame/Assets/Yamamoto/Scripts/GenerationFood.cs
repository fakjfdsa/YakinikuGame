using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationFood : MonoBehaviour
{
    public const int _displayCount = 5;

    private int _id;

    public GameObject[] _foods = new GameObject[_displayCount];

    public bool GetLimitOrder()
    {
        var n = 0;
        for(int i = 0; i < _foods.Length; i++)
        {
            if(_foods[i] == null) n += 1;
        }

        if (n > 0) return true;

        return false;
    }

    public void GenerateFood(GameObject obj)
    {
        var i = 0;
        for (i = 0; i < _displayCount - 1 && _foods[i] != null; i++) ;
        if(_foods[i] == null) _id = i;

        if (_id < _displayCount)
        {
            var o = Instantiate(obj, new Vector3(-7f, 0f, 5f - (2.5f * _id)), Quaternion.identity);
            _foods[_id] = o;
        }
        else
        {
            Debug.Log("‚à‚¤’•¶‚Å‚«‚Ü‚¹‚ñ");
        }
    }

    public void Eat()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            Destroy(_foods[0]);
            _foods[0] = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Destroy(_foods[1]);
            _foods[1] = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(_foods[2]);
            _foods[2] = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(_foods[3]);
            _foods[3] = null;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Destroy(_foods[4]);
            _foods[4] = null;
        }
    }

}
