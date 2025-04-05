using UnityEngine;

public class MakeRay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
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
}
