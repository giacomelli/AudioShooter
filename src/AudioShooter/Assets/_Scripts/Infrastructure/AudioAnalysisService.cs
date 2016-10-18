using UnityEngine;
using System.Collections;
using System;
using Skahal.Common;

public class AudioAnalysisService : AudioServiceBase
{
	float[] _audioData;

	public static AudioAnalysisService Instance { get; private set; }

	public float _readSoundSeconds;

	public float _tickSeconds;
	public int _tickEveryReads;
	public  event EventHandler SoundTick;
	public int Ticks { get; private set; }
	public bool AudioAnalyzed { get; private set; }


	public event EventHandler Analyzed;


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
				MessagesController.Instance.ChangeCentralMessage("Generating {0,2} seconds...".With(Ticks));
				yield return new WaitForEndOfFrame();
			}
		}

		MessagesController.Instance.ChangeCentralMessage("");

		ResetData();

		MusicSource.Play();
		AudioAnalyzed = true;
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