using UnityEngine;

namespace Controllers.TitleUI
{
    public class TitleUI : MonoBehaviour
    {
        public void OnStartButtonClicked()
        {
            Debug.Log("Start Button Clicked");
        }
        public void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}