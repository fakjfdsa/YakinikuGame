using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    [SerializeField] private FoodDataBase _foodDataBase;
    [SerializeField] private Image _tabletImage;
    [SerializeField] private Image _flame;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _moveSpeed;

    private GenerationFood _generationFood;

    private Vector3 _tabletPos;

    private int _foodId;

    private bool _isMove;

    private Image[] _foodIcons;

    public enum STATE
    { 
        BAKE,
        ORDER,
    }
    public STATE _state = STATE.BAKE;

    // Start is called before the first frame update
    void Start()
    {
        _generationFood = GetComponent<GenerationFood>();

        // タブレット画像の子オブジェクトの数で初期化
        _foodIcons = new Image[_tabletImage.transform.childCount];

        // データベースからアイコンを表示させる
        for(int i = 0; i < _foodDataBase._icons.Count; i++)
        {
            // イメージ画像を持ってくる
            _foodIcons[i] = _tabletImage.transform.GetChild(i).GetComponent<Image>();

            // アイコンを生成、適応
            var icon = Instantiate(_foodDataBase._icons[i]);
            _foodIcons[i].sprite = icon;
        }

        // フレームをタブレット画像の子オブジェクトにする
        _flame.transform.SetParent(_tabletImage.transform, false);
        _flame.rectTransform.localPosition = _foodIcons[0].rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == STATE.BAKE)
        {
            CheckTablet(STATE.ORDER, _targetPos);

            _generationFood.Eat();
        }
        else if(_state == STATE.ORDER)
        {
            SelectFood();

            if(Input.GetKeyDown(KeyCode.Return))
            {
                _generationFood.GenerateFood(_foodDataBase._foods[_foodId]);
            }

            CheckTablet(STATE.BAKE, _startPos);
        }

        MoveTablet();
    }

    /// <summary>
    /// 食材を選択
    /// </summary>
    private void SelectFood()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _foodId -= 1;
            if (_foodId <= 0) _foodId = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _foodId += 1;
            if (_foodId > _foodIcons.Length - 1) _foodId = _foodIcons.Length - 1;
        }

        _flame.rectTransform.localPosition = _foodIcons[_foodId].rectTransform.localPosition;
    }

    /// <summary>
    /// タブレットの動き
    /// </summary>
    private void MoveTablet()
    {
        if (_isMove)
        {
            var pos = _tabletImage.rectTransform.localPosition;
            _tabletImage.rectTransform.localPosition = Vector3.MoveTowards(pos, _tabletPos, _moveSpeed * Time.deltaTime);

            if ((pos - _tabletPos).magnitude <= 0f)
            {
                _isMove = false;
            }
        }
    }

    /// <summary>
    /// タブレットが動くフラグ
    /// </summary>
    /// <param name="state"></param>
    private void CheckTablet(STATE state, Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMove = true;
            _tabletPos = pos;
            _state = state;
        }
    }

}
