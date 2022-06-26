using System.Collections;
using UnityEngine;

namespace PMEGame.Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        private readonly string _modePrefName = "CurrentScene";
        
        #region Methods

        private void Start()
        {
            SceneEvents.OnLoadGarage += LoadGarage;
            SceneEvents.OnLoadLevels += LoadLevels;
            SceneEvents.OnLoadCity += LoadTraffic;
        }

        private void OnDestroy()
        {
            SceneEvents.OnLoadGarage -= LoadGarage;
            SceneEvents.OnLoadLevels -= LoadLevels;
            SceneEvents.OnLoadCity -= LoadTraffic;
        }

        public void LoadLevels()
        {
            PlayerPrefs.SetString(_modePrefName, SceneEnum.Levels.ToString());
            LoadCity();
        }

        public void LoadTraffic()
        {
            PlayerPrefs.SetString(_modePrefName, SceneEnum.City.ToString());
            LoadCity();
        }

        public void LoadFreeMode()
        {
            PlayerPrefs.SetString(_modePrefName, SceneEnum.FreeMode.ToString());
            LoadCity();
        }
        
        public void LoadGarage()
        {
            SceneTransition.SwitchScene(SceneEnum.Garage);
        }

        private void LoadCity()
        {
            SceneTransition.SwitchScene(SceneEnum.City);
        }

        public void LoadGameInitializer()
        {
            SceneTransition.SwitchScene(SceneEnum.GameInitializer);
        }

        public IEnumerator LoadGarageAfterTime(int time)
        {
            yield return new WaitForSeconds(time);

            SceneTransition.SwitchScene(SceneEnum.Garage);
        }

        #endregion Methods
    }
}
