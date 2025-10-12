using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject _BeforeGoalButton;
    
    void Start()
    {
        if (ES3.KeyExists("HaveGoaled") && ES3.Load<bool>("HaveGoaled"))
        {
            _BeforeGoalButton.SetActive(true);
            #if UNITY_IOS
            UnityEngine.iOS.Device.RequestStoreReview();
            if (!ES3.KeyExists("ShowedReview"))
            {
                ES3.Save<bool>("ShowedReview", true);
            }
            #endif
        }
    }
}
