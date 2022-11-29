using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public enum STATUS
    {
        NAMA,
        GOOD,
        BLACK,
    }

    Renderer[] r=new Renderer[2];
    GameObject[] side = new GameObject[2];

    [SerializeField]
    STATUS[] status = new STATUS[2];

    [SerializeField]
    Color namaColor;
    [SerializeField]
    Color goodColor;
    Color blackColor = Color.black;
    int[][] pos;
    [SerializeField]
    int[] posX;
    [SerializeField]
    int[] posY;

    float[] doneness = new float[2];  //焼き加減
    float grillTime = 0;    //焼いた時間
    [SerializeField]
    float goodTime;         //いい焼き加減の時間
    [SerializeField]
    float blackTime;        //焦げる時間
    [SerializeField]
    int eatPoint = 100;           //食べたときの加算ポイント

    private bool fire = false;
    private bool downSide = false;
    private bool turn = false;

    public float turnSpeed;
    Quaternion startTurn;

    // Start is called before the first frame update
    void Start()
    {
        pos = new int[posX.Length][];

        for (int i = 0; i < posX.Length; i++)
        {
            pos[i] = new int[2];

            pos[i][0] = posX[i];
            pos[i][1] = posY[i];
        }

        r[1] = GetComponent<Renderer>();
        r[0] = transform.GetChild(0).GetComponent<Renderer>();
        side[1] = gameObject;
        side[0] = transform.GetChild(0).gameObject;
        startTurn = transform.rotation;

        ColorChange(0);
        ColorChange(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            if (downSide)
            {
                Grill(1);
            }
            else
            {
                Grill(0);
            }
        }
        Turn();
    }

    public void Turn()
    {
        if (turn)
        {
            if (downSide)
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(1, 0, 0, 0), Time.deltaTime * turnSpeed);
                transform.Rotate(1,0,0);
            }
            else
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, startTurn, Time.deltaTime * turnSpeed);
                transform.Rotate(-1, 0, 0);
            }

            if (transform.localRotation == new Quaternion(1, 0, 0, 0) || transform.localRotation == startTurn)
            {
                fire = true;
                turn = false;
            }
            else
            {
                fire = false;
            }
        }
    }

    public void TurnStart()
    {
        downSide = !downSide;
        turn = true;
    }

    public void Grill(int num)
    {
        if (doneness[num] < 1.01f)
        {
            doneness[num] += Time.deltaTime / goodTime;
            status[num] = STATUS.NAMA;
        }
        else if (doneness[num] < 2.0f)
        {
            doneness[num] += Time.deltaTime / blackTime;
            status[num] = STATUS.GOOD;
        }
        else
        {
            status[num] = STATUS.BLACK;
        }

        ColorChange(num);
    }

    public void ColorChange(int num)
    {
        if (doneness[num] < 1.01f)
            r[num].material.color = Color.Lerp(namaColor, goodColor, doneness[num]);
        else r[num].material.color = Color.Lerp(goodColor, blackColor, doneness[num] - 1);

    }

    public int EatPointer()
    {
        float mag = 1;
        if(status[0]==STATUS.NAMA || status[1] == STATUS.NAMA)
        {
            mag = 0.5f;
        }
        else if (status[0] == STATUS.GOOD || status[1] == STATUS.GOOD)
        {
            mag = 1f;
        }
        else if (status[0] == STATUS.BLACK || status[1] == STATUS.BLACK)
        {
            mag = 0.5f;
        }

        return (int)(eatPoint * mag);
    }

    public void SetFire(bool b)
    {
        fire = b;
    }
    
    public int[][] GetDefaltPos()
    {
        return pos;
    }

    public void SetPos(int[][] _pos)
    {
        pos = _pos;
    }
}
