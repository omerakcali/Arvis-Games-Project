using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private Text goldText, gemText,timeText;
    [SerializeField] private RectTransform floatingGold, floatingGem;
    [SerializeField] private float floatTime = 1.5f;
    [SerializeField] private float floatspeed = 0.0005f;

    private Vector3 goldFirstPos, gemFirstPos;

    private void Awake()
    {
        goldFirstPos = floatingGold.localPosition;
        gemFirstPos = floatingGem.localPosition;
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetBar(float currentVal, float maxVal)
    {
        slider.value = currentVal / maxVal;
        timeText.text = (maxVal - currentVal).ToString("0");
    }

    public void FloatText(int gold, int gem)
    {
        this.gold = gold;
        this.gem = gem;
        StartCoroutine(nameof(FloatTextRoutine));
    }
    int gold,gem;

    private float currentTime = 0f;

    IEnumerator FloatTextRoutine()
    {
        floatingGem.gameObject.SetActive(true);
        floatingGold.gameObject.SetActive(true);

        goldText.color = Color.green;
        if (gold < 0)
        {
            goldText.text = "-";
            goldText.color = Color.red;
        }
        else
        {
            goldText.text = "+";
            if (gold == 0) floatingGold.gameObject.SetActive(false);
        }
        goldText.text += gold.ToString();

        gemText.color = Color.green;
        if (gem < 0)
        {
            gemText.text = "-";
            gemText.color = Color.red;
        }
        else
        {
            gemText.text = "+";
            if (gem == 0) floatingGem.gameObject.SetActive(false);
        }
        gemText.text += gem.ToString();
        
        while (currentTime <= floatTime)
        {
            currentTime += Time.deltaTime;
            floatingGold.position += Vector3.up*floatspeed*Mathf.Sin(currentTime);
            floatingGem.position+= Vector3.up*floatspeed*Mathf.Sin(currentTime);
            yield return null;
        }

        floatingGem.localPosition = gemFirstPos;
        floatingGold.localPosition= goldFirstPos;
        floatingGem.gameObject.SetActive(false);
        floatingGold.gameObject.SetActive(false);

        currentTime = 0f;
    }
}