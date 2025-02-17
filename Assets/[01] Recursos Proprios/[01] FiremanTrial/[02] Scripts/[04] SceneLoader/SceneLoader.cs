using System;
using System.Collections;
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
            yield return null;
            AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
            operation.allowSceneActivation = false;
            
            float progressSmooth = 0f;

            while (!operation.isDone)
            {
                OnProgressUpdated?.Invoke(operation.progress);
                float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

                while (progressSmooth < targetProgress)
                {
                    progressSmooth = Mathf.MoveTowards(progressSmooth, targetProgress, Time.deltaTime * 0.5f);
                    OnProgressUpdated?.Invoke(progressSmooth);
                    yield return null;
                }
                
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                    yield return new WaitForSeconds(1f);
                    OnProgressUpdated?.Invoke(1f);
                }
                yield return null;
            }
            
            yield return new WaitForSeconds(0.5f);
            OnLoadingComplete?.Invoke();
        }
    }
}