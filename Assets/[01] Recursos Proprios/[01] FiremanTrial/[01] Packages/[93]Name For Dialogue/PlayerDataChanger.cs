using UnityEngine;

namespace FiremanTrial.DialogueOverride
{
    public class PlayerDataChanger : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        public void Save()
        {
            var serializableData = new PlayerDataSerializable(playerData);
            PermanentData.Save(serializableData, nameof(PlayerData));

        }

        public void Load()
        {
            var serializableData = PermanentData.Load(new PlayerDataSerializable(playerData), nameof(PlayerData));
            serializableData.ApplyTo(playerData);
            playerData.isLoading = true;
        }

        public void NewGame()
        {
            playerData.playerName = "Alex";
            playerData.isLoading = false;
        }
    }

    public class PlayerDataSerializable
    {
        public string playerName;

        public PlayerDataSerializable(PlayerData data)
        {
            playerName = data.playerName;
        }

        public void ApplyTo(PlayerData data)
        {
            data.playerName = playerName;
        }
    }
}