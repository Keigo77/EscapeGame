using UnityEngine;

public class SendReportButton : MonoBehaviour
{
    public void OpenGoogleForm()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSdS5BqMbl-6UHEkta_DTfjz7WTPYskyzzyWC2MLFfOpUhD5QA/viewform");
    }
}
