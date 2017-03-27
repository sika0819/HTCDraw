using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Test : MonoBehaviour
{
    public Material mat;
    public Color color = Color.white;
    public GameObject brush;
    GameObject tempBrush;
    LineRenderer brushData;
    public List<Vector3> pointList;
    bool startDraw = false;
    public bool isReady = false;
    public float freshRate = 25f;//刷新频率多少帧
    float freshTimer = 0;
    SteamVR_TrackedObject trackObj;
    public GameObject ColorPicker;
    public GameObject mouseUI;
    void Start()
    {
        pointList = new List<Vector3>();
        mat.color = color;
        trackObj = GetComponent<SteamVR_TrackedObject>();
        mouseUI = ColorPicker.transform.FindChild("Mouse").gameObject;
        ColorPicker.SetActive(false);

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//更换成HTC手柄输入即可
            startDraw = true;
            pointList = new List<Vector3>();
            tempBrush = Instantiate(brush) as GameObject;
            tempBrush.GetComponent<Renderer>().material = mat;
            mat.color = color;
            brushData = tempBrush.GetComponent<LineRenderer>();
        }
        if (startDraw)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 Point = Camera.main.ScreenToWorldPoint(mousePos);
            freshTimer += Time.deltaTime;
            if (freshTimer > (1 / freshRate))
            {
                freshTimer = 0;
                pointList.Add(Point);
                if (pointList.Count > 1)
                {
                    brushData.numPositions = pointList.Count;
                    brushData.SetPositions(pointList.ToArray());
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            startDraw = false;
            pointList.Clear();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ColorPicker.SetActive(true);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 Point = Camera.main.ScreenToWorldPoint(mousePos);
            mouseUI.transform.localPosition = Point;
        }
        if (Input.GetMouseButtonUp(1))
        {
            ColorPicker.SetActive(false);
        }
    }
}
