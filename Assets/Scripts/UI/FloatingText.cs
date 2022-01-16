using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatingTime, floatingSpeed;

    private RectTransform _rectTransform;
    private Text _text;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float time = 0f;
    // Update is called once per frame
    void Update()
    {
        _rectTransform.position += Vector3.up * floatingSpeed;
        time += Time.deltaTime;
        if(time>=floatingTime) Destroy(gameObject);

    }

    public void Init(int val,Vector3 pos)
    {
        _text.text = "";
        if (val > 0)
        {
            _text.color = Color.green;
            _text.text = "+";
        }
        else
        {
            _text.color = Color.red;
        }

        _text.text += val.ToString();

        _rectTransform.localPosition = pos;
    }
}
