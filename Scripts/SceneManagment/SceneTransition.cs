using UnityEngine;
using UnityEngine.SceneManagement;
using PMEGame.Ads;

namespace PMEGame.Scenes
{
    public class SceneTransition : MonoBehaviour
    {
        #region Fields

        private static SceneTransition s_instance;
        private Animator _animator;
        [HideInInspector]
        public AsyncOperation _loadingSceneOperation;
        private static bool s_isSwitching;

        private static bool s_shouldPlayOpeningAnimation;

        private const string SceneClosingTrigger = "SceneClosing";
        private const string SceneOpeningTrigger = "SceneOpening";

        #endregion Fields

        #region Methods

        private void Start()
        {
            s_instance = this;

            _animator = GetComponent<Animator>();

            SceneEvents.OnAllowSceneActivation += AllowSceneActivation;

            if (s_shouldPlayOpeningAnimation)
            {
                _animator.SetTrigger(SceneOpeningTrigger);

                s_shouldPlayOpeningAnimation = false;
            }
        }

        private void OnDestroy()
        {
            SceneEvents.OnAllowSceneActivation -= AllowSceneActivation;
        }

        public static void SwitchScene(SceneEnum scene)
        {
            if (!s_isSwitching)
            {
                s_isSwitching = true;

                s_instance._animator.SetTrigger(SceneClosingTrigger);

                s_instance._loadingSceneOperation =
                    SceneManager.LoadSceneAsync(scene.ToString());
                s_instance._loadingSceneOperation.allowSceneActivation = false;
            }
        }

        public static void SwitchSceneQuickly(SceneEnum scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }

        public void OnAnimationOver()
        {
            s_shouldPlayOpeningAnimation = true;
        }

        public void AllowSceneActivation()
        {
            s_instance._loadingSceneOperation.allowSceneActivation = true;
            s_isSwitching = false;
        }

        #endregion Methods
    }
}
