using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Some help from: https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/

public class UIMechanics : MonoBehaviour
{
    [SerializeField] IcebergManager ice;

    [SerializeField] float startingTime = 60;
    [SerializeField] Text timer;
    private float time;

    [SerializeField] float speed;
    [SerializeField] RectTransform thermTemp;
    [SerializeField] GameObject[] thermSpriteArray;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "Time Remaining: " + startingTime;
        time = startingTime;
        thermTemp.localScale = new Vector2(0.1f, 1);
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;

            if (ice.meltRate > 1.5)
                ice.meltRate -= (Time.deltaTime / 30);
            else if (ice.meltRate < 1.5 && ice.meltRate > 1)
                ice.meltRate -= (Time.deltaTime / 25);
            else if (ice.meltRate < 1 && ice.meltRate > 0.3)
                ice.meltRate -= (Time.deltaTime / 30);

            if (thermTemp.localScale.x < 7.48)
                thermTemp.localScale = new Vector2(thermTemp.localScale.x + (speed * Time.deltaTime), thermTemp.localScale.y);
            UpdateUI();
        }
        else if ((int)time == 0)
            Debug.Log("You win!");
    }


    private void UpdateUI()
    {
        timer.text = "Time Remaining: " + (int)time;
        //thermTemp.sizeDelta = new Vector2(thermTemp.rect.width + Time.deltaTime, thermTemp.rect.height);
        //thermTemp.transform.position = new Vector2(thermTemp.transform.position.x + 0.01f, thermTemp.transform.position.y);

        if ((int)time == ((int)startingTime * 0.75))
        {
            thermSpriteArray[0].SetActive(false);
            thermSpriteArray[1].SetActive(true);
        }
        else if ((int)time == ((int)startingTime * 0.5))
        {
            thermSpriteArray[1].SetActive(false);
            thermSpriteArray[2].SetActive(true);
        }
        else if ((int)time == ((int)startingTime * 0.25))
        {
            thermSpriteArray[2].SetActive(false);
            thermSpriteArray[3].SetActive(true);
        }
    }
}
