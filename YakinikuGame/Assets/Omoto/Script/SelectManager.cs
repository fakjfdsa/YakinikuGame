using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [SerializeField]
    private int playerNum;

    public enum STATE
    {
        SELECT,
        SELECT_PLATE,
        GRAB,
    }

    public STATE state;

    int selectTile;
    GameObject selectObjectTile;
    GameObject oldObjectTile;

    public GameObject selectObj;
    public GameObject grabObj;

    public TileManager tile;
    FoodManager fManager;
    PointManager pManager;

    Color defaultTileColor;
    Color defaultFood;

    public GameObject plates;
    int selectPlate;

    int rotateNum;
    int[][] pos;

    private void Start()
    {
        defaultTileColor = new Color(0, 255, 255, 255);
        pManager = GameObject.FindObjectOfType<PointManager>();

        state = STATE.SELECT;
    }

    void Update()
    {
        //カーソル移動
        MoveCursor();

        //ひっくり返す
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (HitChekObj())
            {
                HitChekObj().GetComponentInChildren<FoodManager>().TurnStart();
            }
        }

        //食べる
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (HitChekObj())
            {
                EatFunc(HitChekObj().GetComponentInChildren<FoodManager>());
            }
        }

        //つかむ
        if (Input.GetKeyDown(KeyCode.C))
        {
            Grab();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            state = STATE.SELECT_PLATE;


        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (grabObj)
            {
                state = STATE.GRAB;
            }
            else
            {
                state = STATE.SELECT;
            }
        }

        //食材をつかんでる
        if (state == STATE.GRAB)
        {
            grabObj.transform.position = transform.position + (Vector3.up * 1.5f) - (Vector3.forward * 0.5f);

            if (Input.GetKeyDown(KeyCode.P))
            {
                GrabRotation(+1);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                GrabRotation(-1);
            }
        }
    }

    //カーソル移動
    public void MoveCursor()
    {
        if (state == STATE.SELECT|| state == STATE.GRAB)
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

        transform.position = tile.GetTile()[selectTile / 8, selectTile % 8].transform.position;
        selectObjectTile = tile.GetTile()[selectTile / 8, selectTile % 8];

        SelectTile();
        oldObjectTile = selectObjectTile;
        }

        if (state == STATE.SELECT_PLATE)
        {
            if (Input.GetButtonDown("Vertical"))
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    if (selectPlate == 0)
                    {
                        selectPlate = 4;
                    }
                    else
                    {
                        selectPlate--;
                    }
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    selectPlate++;
                }
                selectPlate %= 5;
                Debug.Log(selectPlate);
            }

            transform.position = plates.transform.GetChild(selectPlate).transform.position;

        }

    }

    //選択してるタイル
    public void SelectTile()
    {
        if (oldObjectTile)
        {
            oldObjectTile.GetComponent<Renderer>().material.color = defaultTileColor;
        }
        selectObjectTile.GetComponent<Renderer>().material.color = new Color(255, 255, 0, 255);
    }

    //選択しているオブジェクト
    public GameObject HitChekObj()
    {
        GameObject _selectObj = null;
        if (tile.GetTile()[0, 0])
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), new Vector3(0, -2, 0));

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.transform.CompareTag("Food"))
                {
                    _selectObj = hitInfo.transform.gameObject;
                }
                else
                {
                    _selectObj = null;
                }
            }
        }
        selectObj = _selectObj;
        return _selectObj;
    }

    //掴む処理
    public void Grab()
    {
        if (!grabObj)
        {
            if (HitChekObj())
            {
                grabObj = HitChekObj();
                fManager = grabObj.GetComponentInChildren<FoodManager>();

                pos = new int[fManager.GetDefaltPos().Length][];

                for (int i = 0; i < fManager.GetDefaltPos().Length; i++)
                {
                    pos[i] = new int[2];
                    pos[i][0] = fManager.GetDefaltPos()[i][0];
                    pos[i][1] = fManager.GetDefaltPos()[i][1];
                }

                defaultFood = grabObj.GetComponentInChildren<Renderer>().material.color;
                grabObj.GetComponentInChildren<Renderer>().material.color = new Color(defaultFood.r, defaultFood.g, defaultFood.b, 0.5f);
                grabObj.transform.GetChild(0).GetComponentInChildren<Renderer>().material.color = new Color(defaultFood.r, defaultFood.g, defaultFood.b, 0.5f);

                fManager.SetFire(false);

                state = STATE.GRAB;
            }
        }
        else
        {
            Debug.Log(tile.GetTileNum(selectTile / 8, selectTile % 8));
            if (playerNum == tile.GetTileNum(selectTile / 8, selectTile % 8))
            {
                grabObj.GetComponentInChildren<Renderer>().material.color = defaultFood;
                grabObj.transform.GetChild(0).GetComponentInChildren<Renderer>().material.color = defaultFood;
                grabObj.transform.position = selectObjectTile.transform.position + (Vector3.up * 0.2f);
                grabObj = null;

                fManager.SetFire(true);
                fManager = null;

                state = STATE.SELECT;
            }
        }
    }

    //掴んでるときの回転
    public void GrabRotation(int num)
    {
        if (state == STATE.GRAB)
        {
            if (rotateNum <= 0 && num < 0)
            {
                rotateNum = 3;
            }
            rotateNum += num;
            for (int i = 0; i < pos.Length; i++)
            {
                int temp = 0;
                switch (rotateNum % 4)
                {
                    case 0:

                        break;

                    case 1:
                        temp = pos[i][0];
                        pos[i][0] = pos[i][1];
                        pos[i][1] = temp * -1;
                        Debug.Log(pos[i][0]);
                        Debug.Log(pos[i][1]);
                        break;

                    case 2:
                        pos[i][0] *= -1;
                        pos[i][1] *= -1;
                        break;

                    case 3:
                        temp = pos[i][0];
                        pos[i][0] = pos[i][1] * -1;
                        pos[i][1] = temp;
                        break;
                }
            }
            grabObj.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    //食べたときの処理
    public void EatFunc(FoodManager f)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            tile.SetTileNum(selectTile / 8 + pos[i][0], selectTile % 8 + pos[i][1], 1);
        }
        pManager.PointSum(f.EatPointer(), 1);
        Destroy(f.gameObject);
        state = STATE.SELECT;
    }
}
