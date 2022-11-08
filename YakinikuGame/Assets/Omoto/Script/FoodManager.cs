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

    float[] doneness = new float[2];  //èƒÇ´â¡å∏
    float grillTime = 0;    //èƒÇ¢ÇΩéûä‘
    [SerializeField]
    float goodTime;         //Ç¢Ç¢èƒÇ´â¡å∏ÇÃéûä‘
    [SerializeField]
    float blackTime;        //è≈Ç∞ÇÈéûä‘

    public bool fire = false;
    public bool downSide = false;
    public bool turn = false;

    public float rotateSpeed;
    Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        r[1] = GetComponent<Renderer>();
        r[0] = transform.GetChild(0).GetComponent<Renderer>();
        side[1] = gameObject;
        side[0] = transform.GetChild(0).gameObject;
        startRot = transform.rotation;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            downSide = !downSide;
            turn = true;
        }

        Turn();
    }

    public void Turn()
    {
        if (turn)
        {
            if (downSide)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(1, 0, 0, 0), Time.deltaTime * rotateSpeed);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, startRot, Time.deltaTime * rotateSpeed);
            }

            if (transform.rotation == new Quaternion(1, 0, 0, 0) || transform.rotation == startRot)
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
}
