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
            if (!ES3.KeyExists("ShowedReview"))
            {
                UnityEngine.iOS.Device.RequestStoreReview();
                ES3.Save<bool>("ShowedReview", true);
            }
            #endif
        }
    }
}
