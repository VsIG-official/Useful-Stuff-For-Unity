using System;
using UnityEngine;

public class SceneEvents : MonoBehaviour
{
    #region LoadLevels

    public static event Action OnLoadLevels;
    public static void SendLoadLevels()
    {
        OnLoadLevels?.Invoke();
    }

    #endregion LoadLevels

    #region LoadGarage

    public static event Action OnLoadGarage;
    public static void SendLoadGarage()
    {
        OnLoadGarage?.Invoke();
    }

    #endregion LoadGarage

    #region LoadCity

    public static event Action OnLoadCity;
    public static void SendLoadCity()
    {
        OnLoadCity?.Invoke();
    }

    #endregion LoadCity

    #region AllowSceneActivation

    public static event Action OnAllowSceneActivation;
    public static void SendAllowSceneActivation()
    {
        OnAllowSceneActivation?.Invoke();
    }

    #endregion AllowSceneActivation
}
