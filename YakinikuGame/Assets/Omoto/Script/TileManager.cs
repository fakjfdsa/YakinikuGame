using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] film;

    GameObject[,] tile = new GameObject[8, 8];
    
    int[,] tileNum = new int[8, 8];
    int selectX = 0;
    int selectY = 0;

    int height = 8;
    int width = 8;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                tile[i, j] = gameObject.transform.GetChild(count).gameObject;
                if (i < 2)
                {
                    SetTileNum(i, j, 1);
                }
                else if (i > 5)
                {
                    SetTileNum(i, j, 2);
                }
                count++;
            }
        }
    }

    public void SetFilm(int x, int z)
    {
        switch (tileNum[x, z])
        {
            case 0:
                if (tile[x, z].transform.childCount > 0)
                {
                    Destroy(tile[x, z].transform.GetChild(0).gameObject);
                }
                break;
            case 1:
                Instantiate(film[0], tile[x, z].transform.position, Quaternion.identity, tile[x, z].transform);
                break;
            case 2:
                Instantiate(film[1], tile[x, z].transform.position, Quaternion.identity, tile[x, z].transform);
                break;
        }
    }

    public void SetTileNum(int x,int z,int num)
    {
        tileNum[x, z] = num;
        SetFilm(x, z);
    }


    public GameObject[,] GetTile()
    {
        return tile;
    }
}
