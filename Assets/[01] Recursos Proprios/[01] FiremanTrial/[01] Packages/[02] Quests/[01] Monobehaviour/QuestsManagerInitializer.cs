using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestsManagerInitializer : MonoBehaviour
    {
        private void Start()
        {
            QuestsManager.Initialize();
        }
        
        public void SaveQuestsManually()
        {
            QuestsManager.SaveQuests();
        }

        public void LoadQuestsManually()
        {
            QuestsManager.LoadQuests();
        }

        public void LoadAsNewGame()
        {
            QuestsManager.StartNewGame();
        }
    }
}