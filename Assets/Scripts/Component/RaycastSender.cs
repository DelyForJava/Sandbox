using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSender : MonoBehaviour
{
    public int FaceValue
    {
        get;set;
    }

    private Camera uiCamera;
    //private sbyte mask = 0x0f;
    //private sbyte val = 40;

    //void Split(sbyte v, sbyte m)
    //{
    //    var t = v >> 4;
    //    var vv = v & m;
    //}

    // Use this for initialization
    void Start()
    {
   
        uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("click object name is " + gameObject.name);

            Ray ray = uiCamera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
                GameObject gameObj = hitInfo.collider.gameObject;
                Debug.Log("click object name is 1" + gameObj.name);
                if (gameObj.tag == "boot")//当射线碰撞目标为boot类型的物品 ，执行拾取操作
                {
                    Debug.Log("pick up!");
                }
            }
        }
    }
}
