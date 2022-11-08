using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenarator : MonoBehaviour
{
    public GameObject tile;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Instantiate(tile, new Vector3(i, 0, j), Quaternion.identity, transform);
            }
        }
    }
}
