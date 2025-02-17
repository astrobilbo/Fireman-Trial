using UnityEngine;

namespace FiremanTrial.DialogueOverride
{
    public class PlayerDataChanger : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        public void Save()
        {
            PermanentData.Save(playerData, nameof(PlayerData));
        }

        public void Load()
        {
            playerData= PermanentData.Load(playerData, nameof(PlayerData));
        }
    }
}