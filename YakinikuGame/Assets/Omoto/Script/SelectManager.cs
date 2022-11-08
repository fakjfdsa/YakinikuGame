using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public TileManager tile;
    int selectTile;
    GameObject selectObject;
    GameObject oldObject;

    Color defaultColor;

    private void Start()
    {
        defaultColor = new Color(0, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (tile.GetTile().Length > selectTile + 8)
                {
                    selectTile += 8;
                }
            }
            else
            {
                if (6 < selectTile)
                {
                    selectTile -= 8;
                }
            }
        }
       
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (tile.GetTile().Length > selectTile)
                {
                    selectTile += 1;
                }
            }
            else
            {
                if (0 < selectTile)
                {
                    selectTile -= 1;
                }
            }
        }

        if (tile.GetTile()[0, 0])
        {
            transform.position = tile.GetTile()[selectTile / 8, selectTile % 8].transform.position;
            selectObject = tile.GetTile()[selectTile / 8, selectTile % 8];
            SelectTile();
            oldObject = selectObject;
        }
    }

    public void SelectTile()
    {
        if (oldObject)
        {
            oldObject.GetComponent<Renderer>().material.color = defaultColor;
        }
        selectObject.GetComponent<Renderer>().material.color = new Color(255, 255, 0, 255);
    }
}
