using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using Skahal.Common;

public class AudioRealtimeService : AudioServiceBase
{
	// Events.
	public event EventHandler Started;

	// Fields.
	Action _updateAction = () => { };

	// Static properties;
	public static AudioRealtimeService Instance { get; private set;}

	// Editor properties.
	public int _startsAudioAnalysisWhenSoundTicks;

	void Update()
	{
		_updateAction();
	}

	protected override void Initialize()
	{
		Instance = this;
		CanUseNegativeFrequencies = true;

		AudioAnalysisService.Instance.SoundTick += delegate {
			if (AudioAnalysisService.Instance.Ticks == _startsAudioAnalysisWhenSoundTicks)
			{
				_updateAction = AnalyzeAudio;
				MusicSource.Play();
				Started.Raise(this);
			}
		};
	}

	protected override void GetSpectrumAudioSource()
	{
		MusicSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
	}
}