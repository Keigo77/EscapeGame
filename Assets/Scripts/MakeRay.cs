using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeRay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) // UIをタッチした時は，rayを飛ばさない
            {
                //Debug.Log("UIに触った");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Gimick"))
                {
                    hit.collider.gameObject.GetComponent<IMoveGimick>().MoveGimick();
                }
                else if (hit.collider.gameObject.CompareTag("Explain"))
                {
                    hit.collider.gameObject.GetComponent<IShowText>().ShowExplainText();
                }
            }
        }
    }
}
    /*
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())      // UIをタッチした時は，rayを消す
            {
                Debug.Log("UIに触れました");
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Gimick"))
                {
                    hit.collider.gameObject.GetComponent<IMoveGimick>().MoveGimick();
                } else if (hit.collider.gameObject.CompareTag("Explain"))
                {
                    hit.collider.gameObject.GetComponent<IShowText>().ShowExplainText();
                }
            }
        }
    }
    */

