using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioService : MonoBehaviour
{
	AudioSource _audioSource;
	public static float[] _samples = new float[512];
	static float[] _freqBand = new float[8];
	static float[] _bandBuffer = new float[8];
	float[] _bufferDecrease = new float[8];
	float[] _freqBandHighest = new float[8];
	public static float[] _audioBand = new float[8];
	public static float[] AudioBandBuffer = new float[8];
	public static float[] Samples;
	public static float AudioSeconds { get; private set; }

	// Use this for initialization
	void Awake()
	{
		_audioSource = GetComponent<AudioSource>();

		var clip = _audioSource.clip;
		Samples = new float[111];
		clip.GetData(Samples, 111111);
		AudioSeconds = clip.length;
	}

	// Update is called once per frame
	void Update()
	{
		GetSpecturumAudioSource();
		MakeFrequencyBands();
		BandBuffer();
		CreateAudioBands();
	}

	void GetSpecturumAudioSource()
	{
		_audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
	}

	void MakeFrequencyBands()
	{
		int count = 0;

		for (int i = 0; i < 8; i++)
		{
			float average = 0;
			int sampleCount = (int)Mathf.Pow(2, i) * 2;

			for (int j = 0; j < sampleCount; j++)
			{
				average += _samples[count] * (count + 1);
				count++;
			}

			average /= count;

			_freqBand[i] = average * 10;
		}
	}

	void BandBuffer()
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

	void CreateAudioBands()
	{
		for (int i = 0; i < 8; i++)
		{
			if (_freqBand[i] > _freqBandHighest[i])
			{
				_freqBandHighest[i] = _freqBand[i];
			}

			_audioBand[i] = _freqBand[i] / _freqBandHighest[i];
			AudioBandBuffer[i] = _bandBuffer[i] / _freqBandHighest[i];
		}
	}
}