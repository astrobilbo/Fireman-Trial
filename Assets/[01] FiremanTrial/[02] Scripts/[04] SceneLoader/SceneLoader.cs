using UnityEngine;
using UnityEngine.SceneManagement;

namespace FiremanTrial
{
    public class SceneLoader : MonoBehaviour
    {
        public void Sync(int scene)
        {
            SceneManager.LoadScene(scene);
        }
        public void Sync(string scene)
        {
            SceneManager.LoadScene(scene);
        }
        public void Async(int scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
        public void Async(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}
