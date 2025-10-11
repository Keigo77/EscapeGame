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
                if (hit.collider.gameObject.CompareTag("Gimick") && hit.collider.TryGetComponent<IMoveGimmick>(out var  moveGimmick))
                {
                    moveGimmick.MoveGimmick();
                }
                else if (hit.collider.gameObject.CompareTag("Explain"))
                {
                    hit.collider.gameObject.GetComponent<IShowText>().ShowExplainText();
                }
            }
        }
    }
}

