using UnityEngine;
using System.Collections;
using System;
using Skahal.Common;
using Skahal.Logging;

public class AudioAnalysisService : AudioServiceBase
{
	// Events.
	public event EventHandler SoundTick;
	public event EventHandler Analyzed;

	// Fields.
	float[] _audioData;

	// Static properties.
	public static AudioAnalysisService Instance { get; private set; }

	// Editor properties.
	public float _readSoundSeconds;
	public float _tickSeconds;
	public int _tickEveryReads;
	public float _ticksInterval;

	// Public properties.
	public int Ticks { get; private set; }

	void Start()
	{
		StartCoroutine(AnalyzeSound());
	}

	IEnumerator AnalyzeSound()
	{
		MessagesController.Instance.ChangeCentralMessage("Analizing sound...");
		yield return new WaitForEndOfFrame();

		Debug.LogWarning("Analyzing sound...");

		var secoundsRead = 0f;
		var soundTicks = 0f;
		for (float i = 0; i < MusicSource.clip.length; i += _readSoundSeconds)
		{
			secoundsRead += _readSoundSeconds;

			var samplesOffset = Mathf.RoundToInt(secoundsRead * MusicSource.clip.frequency);
			Debug.Log("offset: " + samplesOffset);

			// Does the music ended?
			if (samplesOffset + _totalSamples > _audioData.Length)
			{
				Debug.LogWarning("Music ended!");
				break;
			}

			Array.Copy(_audioData, samplesOffset, _samples, 0, _totalSamples);

			MakeFrequencyBands();
			BandBuffer();
			CreateAudioBands();

			soundTicks += _readSoundSeconds * MusicSource.clip.frequency;
		
			if (soundTicks >= MusicSource.clip.frequency)
			{
				Ticks++;

				Debug.LogWarning("tick");
				soundTicks = 0;
				SoundTick(this, EventArgs.Empty);
				SHLog.Debug("Generating {0,2} seconds...", Ticks);
				yield return new WaitForSeconds(_ticksInterval);
			}
		}

		ResetData();

		Analyzed.Raise(this);
	}


	protected override void Initialize()
	{
		Instance = this;
		CanUseNegativeFrequencies = false;
		SoundTick += (sender, e) =>
		{
		};

		var clip = MusicSource.clip;
		_audioData = new float[clip.samples];
		clip.GetData(_audioData, 0);
		Debug.Log(_audioData.Length);

		_samples = new float[_totalSamples];
		_samplesMultiplier = _totalSamples / 256;
	}

	protected override void GetSpectrumAudioSource()
	{
		throw new NotImplementedException();
	}
}