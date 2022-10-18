using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    [SerializeField] private Image _tabletImage;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _moveSpeed;

    private Vector3 _tabletPos;

    private bool _isMove;

    private enum STATE
    { 
        BAKE,
        ORDER,
    }
    private STATE _state = STATE.BAKE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == STATE.BAKE)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isMove = true;
                _tabletPos = _targetPos;
                _state = STATE.ORDER;
            }
        }
        else if(_state == STATE.ORDER)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isMove = true;
                _tabletPos = _startPos;
                _state = STATE.BAKE;
            }
        }        

        if(_isMove)
        {
            var pos = _tabletImage.rectTransform.localPosition;
            _tabletImage.rectTransform.localPosition = Vector3.MoveTowards(pos, _tabletPos, _moveSpeed * Time.deltaTime);

            if((pos - _tabletPos).magnitude <= 0f)
            {
                _isMove = false;
            }
        }
    }
}
