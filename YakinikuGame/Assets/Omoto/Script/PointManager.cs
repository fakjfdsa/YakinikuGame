using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    public Text p1Text, p2Text;

    private int p1Point, p2Point;

    public void PointSum(int num,int pNum)
    {
        if (pNum == 1)
        {
            p1Point += num;
            //ポイントの加算
            p1Text.text = "P1:"+p1Point.ToString("0000");
        }
        else
        {
            p2Point += num;
            //ポイントの加算
            p2Text.text = "P2:" + p2Point.ToString("0000");
        }
    }
}
