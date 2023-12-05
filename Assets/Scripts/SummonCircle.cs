using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonCircle : MonoBehaviour
{
    private float MinX,MaxX,MinY,MaxY;
    private Vector2 pos;
    public GameObject Circle1;
    public Canvas Canvas;
    void Start()
    {
        SetMinAndMax();
        SpawnObj();
    }
    void Update()
    {
        

    }

    private void SetMinAndMax()
    {
        //Vector2 Bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height, Screen.width));

        MinY = (-Canvas.GetComponent<RectTransform>().rect.height / 2);
        MaxY = (Canvas.GetComponent<RectTransform>().rect.height/2);
        MinX = (-Canvas.GetComponent<RectTransform>().rect.width / 2);
        MaxX = (Canvas.GetComponent<RectTransform>().rect.width / 2);
    }

    private void SpawnObj()
    {
        pos = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        GameObject obj = Instantiate(Circle1, pos, Quaternion.identity);
        obj.transform.SetParent(Canvas.transform);
        obj.transform.localPosition = pos;
    }
}