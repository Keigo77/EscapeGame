using UnityEngine;

public class AspectRatioFitter : MonoBehaviour
{
    //デフォルトの画面比率
    [Range(5, 16)]
    public int BaseAspectWidth = 16;
    [Range(5, 16)]
    public int BaseAspectHeight = 9;


    void Awake()
    {

        Camera camera = gameObject.GetComponent<Camera>();

        float baseAspect = (float)BaseAspectHeight / (float)BaseAspectWidth;
        float nowAspect = (float)Screen.height / (float)Screen.width;
        float changeAspect;

        if (baseAspect > nowAspect)
        {
            changeAspect = nowAspect / baseAspect;
            camera.rect = new Rect((1.0f - changeAspect) * 0.5f, 0.0f, changeAspect, 1.0f);
        }
        else
        {
            changeAspect = baseAspect / nowAspect;
            camera.rect = new Rect(0.0f, (1.0f - changeAspect) * 0.5f, 1.0f, changeAspect);
        }

    }
}