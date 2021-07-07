using System.IO;
using UnityEngine;
using Valve.VR;

#if (UNITY_EDITOR) 
public class Screenshots : MonoBehaviour
{
    public SteamVR_Action_Boolean screenshotBtn;

    void Update()
    {
        if (screenshotBtn.stateDown)
        {
            string filePath = Application.dataPath + "/Screenshots/";
            string screenshotName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png";

            ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(filePath, screenshotName));

            //ScreenCapture.CaptureScreenshot(Application.dataPath + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
            UnityEditor.AssetDatabase.Refresh();
            print("Smile!");
        }
    }
}
#endif