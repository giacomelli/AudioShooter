using UnityEngine;
using System.Collections;

public class AudioServiceConsoleController : MonoBehaviour {

	public AudioServiceBase _audioService;
	public Rect _area;

	void OnGUI()
	{
		GUI.Window(1, _area, (id) =>
		{
			var metrics = _audioService.AudioBandBuffer;

			for (int i = 0; i < AudioServiceBase.TotalBands; i++)
			{
				var metric = metrics[i];
				GUILayout.Label("{0}: {1}".With(i, metric));
				GUILayout.HorizontalSlider(metric, 0f, 1f);
			}
		}, "Audio Service console");
	}
}
