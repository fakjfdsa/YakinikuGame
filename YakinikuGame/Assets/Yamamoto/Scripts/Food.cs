using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] protected float _bakeLimitTime;// ちょうどいい焼き加減の時間(これを超えると焦げる)
    [SerializeField] protected float _point;//

    protected float _bakeTime;// 焼いている時間
}
