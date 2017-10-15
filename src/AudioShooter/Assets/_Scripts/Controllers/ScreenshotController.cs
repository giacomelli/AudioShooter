using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenshotController : MonoBehaviour {

	void Update () 
	{
		if (Input.GetKey(KeyCode.T))
		{
			var filename = Path.Combine(Application.dataPath, "../../../docs/images/screenshot-{0:yyyMMdd-HHmmss}.png".With(System.DateTime.Now));

			Debug.LogFormat("Saving screenshot to {0}...", filename);

			ScreenCapture.CaptureScreenshot(filename, 2);
		}
	}
}
