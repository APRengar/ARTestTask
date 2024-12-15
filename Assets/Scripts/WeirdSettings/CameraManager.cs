using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class CameraManager : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
             // Проверяем разрешение на использование камеры
            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                // Запрашиваем разрешение
                Permission.RequestUserPermission(Permission.Camera);
            }
            else
            {
                CheckCameraPermission();
            }

        }
    }
    void CheckCameraPermission()
    {
        // Проверяем, есть ли уже разрешение на использование камеры
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject packageManager = context.Call<AndroidJavaObject>("getPackageManager");

        string cameraPermission = "android.permission.CAMERA";

        int permissionGranted = packageManager.Call<int>("checkPermission", cameraPermission, context.Call<string>("getPackageName"));

        if (permissionGranted != 0) // 0 означает разрешено
        {
            RequestCameraPermission();
        }
    }

    void RequestCameraPermission()
    {
        // Запрос разрешения на использование камеры
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("requestPermissions", new string[] { "android.permission.CAMERA" }, 0);
    }
}
