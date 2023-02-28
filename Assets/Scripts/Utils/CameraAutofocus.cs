using System.Collections;
using UnityEngine;
using Vuforia;

public class CameraAutofocus : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => VuforiaARController.Instance.HasStarted);

        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
}
