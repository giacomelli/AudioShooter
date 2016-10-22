using UnityEngine;
using System.Collections;
using System;

public abstract class AudioServiceBase : MonoBehaviour
{
	public const int TotalBands = 8;

	protected int _samplesMultiplier;



	protected AudioSource MusicSource { get; private set; }
	protected bool CanUseNegativeFrequencies { get; set; }
	protected float[] _samples;
	float[] _freqBand = new float[TotalBands];
	float[] _bandBuffer = new float[TotalBands];
	float[] _bufferDecrease = new float[TotalBands];
	float[] _freqBandHighest = new float[TotalBands];
	float[] _audioBand = new float[TotalBands];

	public int _totalSamples = 512;
	public float _bufferIncreaseMultiplier = 1.2f;
	public float[] AudioBandBuffer = new float[TotalBands];

	void Awake()
	{
		MusicSource = AudioConfig.Instance.MusicSource;
		_samples = new float[_totalSamples];
		_samplesMultiplier = _totalSamples / 256;
		Initialize();
	}

	protected abstract void Initialize();

	protected void ResetData()
	{
		_freqBand = new float[TotalBands];
		_bandBuffer = new float[TotalBands];
		_bufferDecrease = new float[TotalBands];
		_freqBandHighest = new float[TotalBands];
		_audioBand = new float[TotalBands];
		AudioBandBuffer = new float[TotalBands];
	}

	protected void AnalyzeAudio()
	{
		GetSpectrumAudioSource();
		MakeFrequencyBands();
		BandBuffer();
		CreateAudioBands();
	}

	protected abstract void GetSpectrumAudioSource();

	protected void MakeFrequencyBands()
	{
		int count = 0;

		for (int i = 0; i < TotalBands; i++)
		{
			float average = 0;
			int sampleCount = (int)Mathf.Pow(2, i) * _samplesMultiplier;

			for (int j = 0; j < sampleCount; j++)
			{
				average += _samples[count] * (count + 1);
				count++;
			}

			average /= count;

			if (CanUseNegativeFrequencies)
			{
				_freqBand[i] = average * 10;
			}
			else {
				_freqBand[i] = Mathf.Sign(average) == 1f ? average * 20 : average * -10;
			}
		}
	}

	protected void BandBuffer()
	{
		for (int g = 0; g < TotalBands; g++)
		{
			if (_freqBand[g] > _bandBuffer[g])
			{
				_bandBuffer[g] = _freqBand[g];
				_bufferDecrease[g] = 0.005f;
			}

			if (_freqBand[g] < _bandBuffer[g])
			{
				_bandBuffer[g] -= _bufferDecrease[g];
				_bufferDecrease[g] *= _bufferIncreaseMultiplier;
			}
		}
	}

	protected void CreateAudioBands()
	{
		for (int i = 0; i < TotalBands; i++)
		{
			if (_freqBand[i] > _freqBandHighest[i])
			{
				_freqBandHighest[i] = _freqBand[i];
			}

			_audioBand[i] = _freqBand[i] / _freqBandHighest[i];
			var newBandBuffer = _bandBuffer[i] / _freqBandHighest[i];

			if (float.IsNaN(newBandBuffer))
			{
				newBandBuffer = 0f;
			}

			AudioBandBuffer[i] = newBandBuffer;
		}
	}
}