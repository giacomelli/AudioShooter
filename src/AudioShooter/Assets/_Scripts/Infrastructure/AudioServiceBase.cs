using UnityEngine;
using System.Collections;
using System;

public abstract class AudioServiceBase : MonoBehaviour
{
	protected int _samplesMultiplier;



	protected AudioSource MusicSource { get; private set; }
	protected bool CanUseNegativeFrequencies { get; set; }
	protected float[] _samples;
	float[] _freqBand = new float[8];
	float[] _bandBuffer = new float[8];
	float[] _bufferDecrease = new float[8];
	float[] _freqBandHighest = new float[8];
	float[] _audioBand = new float[8];

	public int _totalSamples = 512;
	public float[] AudioBandBuffer = new float[8];

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
		_freqBand = new float[8];
		_bandBuffer = new float[8];
		_bufferDecrease = new float[8];
		_freqBandHighest = new float[8];
		_audioBand = new float[8];
		AudioBandBuffer = new float[8];
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

		for (int i = 0; i < 8; i++)
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
		for (int g = 0; g < 8; g++)
		{
			if (_freqBand[g] > _bandBuffer[g])
			{
				_bandBuffer[g] = _freqBand[g];
				_bufferDecrease[g] = 0.005f;
			}

			if (_freqBand[g] < _bandBuffer[g])
			{
				_bandBuffer[g] -= _bufferDecrease[g];
				_bufferDecrease[g] *= 1.2f;
			}
		}
	}

	protected void CreateAudioBands()
	{
		for (int i = 0; i < 8; i++)
		{
			if (_freqBand[i] > _freqBandHighest[i])
			{
				_freqBandHighest[i] = _freqBand[i];
			}

			_audioBand[i] = _freqBand[i] / _freqBandHighest[i];
			var newBandBuffer = _bandBuffer[i] / _freqBandHighest[i];

			AudioBandBuffer[i] = float.IsNaN(newBandBuffer) ? 0f : newBandBuffer;
		}
	}
}