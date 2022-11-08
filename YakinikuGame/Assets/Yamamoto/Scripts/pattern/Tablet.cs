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

        // �^�u���b�g�摜�̎q�I�u�W�F�N�g�̐��ŏ�����
        _foodIcons = new Image[_tabletImage.transform.childCount];

        // �f�[�^�x�[�X����A�C�R����\��������
        for(int i = 0; i < _foodDataBase._icons.Count; i++)
        {
            // �C���[�W�摜�������Ă���
            _foodIcons[i] = _tabletImage.transform.GetChild(i).GetComponent<Image>();

            // �A�C�R���𐶐��A�K��
            var icon = Instantiate(_foodDataBase._icons[i]);
            _foodIcons[i].sprite = icon;
        }

        // �t���[�����^�u���b�g�摜�̎q�I�u�W�F�N�g�ɂ���
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
    /// �H�ނ�I��
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
    /// �^�u���b�g�̓���
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
    /// �^�u���b�g�������t���O
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
