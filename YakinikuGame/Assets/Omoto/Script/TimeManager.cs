using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    Text t;
    [SerializeField]
    float timeLimit = 60;
    float timer;

    public bool startTime;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        t.text = "Time:" + timeLimit.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            timer += Time.deltaTime;
            t.text = "Time:" + (timeLimit - timer).ToString("0");
        }

        if (timeLimit < timer)
        {
            Debug.Log("I—¹");
        }
    }
}
