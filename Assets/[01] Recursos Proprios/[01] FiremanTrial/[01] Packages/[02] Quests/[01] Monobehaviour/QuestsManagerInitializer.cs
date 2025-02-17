using UnityEngine;

namespace FiremanTrial.Quests
{
    public class QuestsManagerInitializer : MonoBehaviour
    {
        private void Awake()
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