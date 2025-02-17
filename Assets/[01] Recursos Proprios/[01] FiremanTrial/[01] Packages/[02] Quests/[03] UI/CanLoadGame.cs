using System;
using UnityEditor.Build.Content;
using UnityEngine;

namespace FiremanTrial.Quests.UI
{
    public class CanLoadGame : MonoBehaviour
    {
        [SerializeField] private GameObject loadGameObject;
        private void Start()
        {
            if(!QuestsManager.HasLoadData) loadGameObject.SetActive(false);
        }

       
    }
}
