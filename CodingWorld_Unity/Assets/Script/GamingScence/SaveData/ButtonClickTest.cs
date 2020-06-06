using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClickTest : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || GUIUtility.hotControl != 0)
            {
                Debug.Log("单击到了UI");
                Debug.Log(Skode_GetCurrentSelect().name);
            }
            else
            {
                Debug.Log("没有单击到UI");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject go = hit.collider.gameObject;    //获得选中物体
                    string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                    print(goName);
                }

            }
        }

    }

    public GameObject Skode_GetCurrentSelect()
    {
        GameObject obj = null;

        GraphicRaycaster[] graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();

        foreach (var item in graphicRaycasters)
        {
            item.Raycast(eventData, list);
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    obj = list[i].gameObject;
                }
            }
        }

        return obj;
    }
}
