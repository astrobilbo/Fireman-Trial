using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiremanTrial
{
    public class SceneLoader : MonoBehaviour
    {
        public static event Action<float> OnProgressUpdated;
        public static event Action OnLoadingComplete;

        public void Sync(int scene) => SceneManager.LoadScene(scene);
        public void Sync(string scene) => SceneManager.LoadScene(scene);

        public void Async(int scene) => StartCoroutine(LoadSceneAsync(scene));

        private IEnumerator LoadSceneAsync(int scene)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                OnProgressUpdated?.Invoke(progress);

                // Se o carregamento chegou a 90%, espera a ativação
                if (operation.progress >= 0.9f)
                {
                    OnProgressUpdated?.Invoke(1f);
                    yield return new WaitForSeconds(1f);
                    OnLoadingComplete?.Invoke();
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}