using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiremanTrial
{
    public class LoadingUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup loadingPanel;
        [SerializeField] private Scrollbar progressBar;
        [SerializeField] private TextMeshProUGUI loadingText;
        private void OnEnable()
        {
            SceneLoader.OnProgressUpdated += UpdateProgress;
            SceneLoader.OnLoadingComplete += HideLoadingScreen;
        }

        private void OnDisable()
        {
            SceneLoader.OnProgressUpdated -= UpdateProgress;
            SceneLoader.OnLoadingComplete -= HideLoadingScreen;
        }

        private void Start()
        {
            CanvasGroupManager.VisibleAndInteractive(false, loadingPanel);
        }

        private void UpdateProgress(float progress)
        {
            CanvasGroupManager.VisibleAndInteractive(true, loadingPanel);
            progressBar.size = progress;
            loadingText.text = (progress*100).ToString("00") + "%";
        }

        private void HideLoadingScreen()
        {
            CanvasGroupManager.VisibleAndInteractive(false, loadingPanel);
        }
    }
}
