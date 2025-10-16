using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

public class FaceBridge : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void FaceAPI_SetReceiver(string goName);
    [DllImport("__Internal")] private static extern void FaceAPI_Start();
    [DllImport("__Internal")] private static extern void FaceAPI_Stop();
#else
    private static void FaceAPI_SetReceiver(string goName) {}
    private static void FaceAPI_Start() {}
    private static void FaceAPI_Stop() {}
#endif

    [SerializeField] private TextMeshProUGUI label;

    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FaceAPI_SetReceiver(gameObject.name);
        FaceAPI_Start();
#endif
    }

    [System.Serializable]
    class Payload { public float ipdPx; public float ipdMm; }

    // JS → Unity
    public void OnFace(string json)
    {
        try
        {
            var p = JsonUtility.FromJson<Payload>(json);
            if (p != null && p.ipdPx > 0)
                label.text = p.ipdMm > 0 ?
                    $"IPD ≈ {p.ipdMm:F1} mm  ({p.ipdPx:F1} px)" :
                    $"IPD ≈ {p.ipdPx:F1} px";
        }
        catch {}
    }

    void OnDisable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FaceAPI_Stop();
#endif
    }
}
