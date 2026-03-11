using UnityEngine;
using UnityEngine.SceneManagement;

namespace _04_UI.Screens
{
    public class SceneTrigger : MonoBehaviour
    {
        public string targetSceneId;

        public void ChangeToTarget()
        {
            SceneManager.LoadScene(targetSceneId);
        }
    }
}