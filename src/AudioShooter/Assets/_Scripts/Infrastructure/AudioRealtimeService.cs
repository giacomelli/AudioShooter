using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class AudioRealtimeService : AudioServiceBase
{
	Action _updateAction = () => { };

	public static AudioRealtimeService Instance { get; private set;}

	void Update()
	{
		_updateAction();
	}

	void GetSpecturumAudioSource()
	{
		MusicSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
	}

	protected override void Initialize()
	{
		Instance = this;
		CanUseNegativeFrequencies = true;

		AudioAnalysisService.Instance.Analyzed += delegate {
			_updateAction = AnalyzeAudio;
		};
	}

	protected override void GetSpectrumAudioSource()
	{
		MusicSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
	}
}