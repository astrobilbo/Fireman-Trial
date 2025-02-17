using UnityEngine;

namespace FiremanTrial.DialogueOverride
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Game/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public string playerName = "Alex";
        public bool isLoading= false;
    }
}