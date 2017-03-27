using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class HTCDraw : MonoBehaviour
{
    public Material mat;
    public Color color = Color.white;
    public GameObject brush;
    GameObject tempBrush;
    LineRenderer brushData;
    public List<Vector3> pointList;
    bool startDraw=false;
    public bool isReady = false;
    public float freshRate = 25f;//刷新频率多少帧
    float freshTimer = 0;
    SteamVR_TrackedObject trackObj;
    void Start()
    {
        pointList = new List<Vector3>();
        mat.color = color;
        trackObj = GetComponent<SteamVR_TrackedObject>();
    }
    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
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
            Vector3 Point = transform.position;
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
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            startDraw = false;
            pointList.Clear();
        }
    }
}
