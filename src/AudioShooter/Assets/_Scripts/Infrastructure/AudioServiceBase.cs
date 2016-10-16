//using UnityEngine;
//using System.Collections;
//using System;

//public abstract class AudioServiceBase : MonoBehaviour
//{
//	public int _totalSamples = 512;
//	private int _samplesMultiplier;

//	public float _readSoundSeconds;

//	AudioSource _audioSource;
//	public float[] _samples;
//	float[] _freqBand = new float[8];
//	float[] _bandBuffer = new float[8];
//	float[] _bufferDecrease = new float[8];
//	float[] _freqBandHighest = new float[8];
//	float[] _audioBand = new float[8];
//	float[] AudioBandBuffer = new float[8];

//	public static int Ticks { get; private set; }
//	public static float AudioDuration { get; private set; }
//	public static float TotalSoundsRead { get; private set; }
//	public static bool AudioAnalyzed { get; private set; }

//	float[] _audioData;

//	void Awake()
//	{
//		_audioSource = AudioConfig.Instance.Music;

//		var clip = _audioSource.clip;
//		_audioData = new float[clip.samples];
//		clip.GetData(_audioData, 0);

//		AudioDuration = clip.length;

//		_samples = new float[_totalSamples];
//		_samplesMultiplier = _totalSamples / 256;

//		TotalSoundsRead = clip.length / _readSoundSeconds;
//	}

//	void Start()
//	{
//		StartCoroutine(AnalyzeSound());
//	}

//	IEnumerator AnalyzeSound()
//	{
//		MessagesController.Instance.ChangeCentralMessage("Analizing sound...");
//		yield return new WaitForEndOfFrame();

//		Debug.LogWarning("Analyzing sound...");

//		secoundsRead = 0;
//		var soundTicks = 0f;
//		for (float i = 0; i < _audioSource.clip.length; i += _readSoundSeconds)
//		{
//			secoundsRead += _readSoundSeconds;

//			var samplesOffset = Mathf.RoundToInt(secoundsRead * _audioSource.clip.frequency);
//			Debug.Log("offset: " + samplesOffset);

//			// Does the music ended?
//			if (samplesOffset + _totalSamples > _audioData.Length)
//			{
//				Debug.LogWarning("Music ended!");
//				break;
//			}

//			Array.Copy(_audioData, samplesOffset, _samples, 0, _totalSamples);

//			MakeFrequencyBands();
//			BandBuffer();
//			CreateAudioBands();

//			soundTicks += _readSoundSeconds * _audioSource.clip.frequency;
		
//			if (soundTicks >= _audioSource.clip.frequency)
//			{
//				Ticks++;

//				Debug.LogWarning("tick");
//				soundTicks = 0;
//				SoundTick(this, EventArgs.Empty);
//				MessagesController.Instance.ChangeCentralMessage("Generating {0,2} seconds...".With(Ticks));
//				yield return new WaitForEndOfFrame();
//			}
//		}

//		MessagesController.Instance.ChangeCentralMessage("");

//		_freqBand = new float[8];
//		_bandBuffer = new float[8];
//		_bufferDecrease = new float[8];
//		_freqBandHighest = new float[8];
//		_audioBand = new float[8];
//		AudioBandBuffer = new float[8];

//		_audioSource.Play();
//		AudioAnalyzed = true;
//	}

//	float t;
//	float secoundsRead = 0f;

//	void Update()
//	{
//		if (AudioAnalyzed)
//		{
//			GetSpectrumAudioSource();
//			MakeFrequencyBands();
//			BandBuffer();
//			CreateAudioBands();
//		}
//	}

//	void GetSpectrumAudioSource()
//	{
//		_audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
//	}

//	void MakeFrequencyBands()
//	{
//		int count = 0;

//		for (int i = 0; i < 8; i++)
//		{
//			float average = 0;
//			int sampleCount = (int)Mathf.Pow(2, i) * _samplesMultiplier;

//			for (int j = 0; j < sampleCount; j++)
//			{
//				average += _samples[count] * (count + 1);
//				count++;
//			}

//			average /= count;

//			if (AudioAnalyzed)
//			{
//				_freqBand[i] = average * 10;
//			}
//			else {
//				_freqBand[i] = Mathf.Sign(average) == 1f ? average * 20 : average * -10;
//			}
//		}
//	}

//	void BandBuffer()
//	{
//		for (int g = 0; g < 8; g++)
//		{
//			if (_freqBand[g] > _bandBuffer[g])
//			{
//				_bandBuffer[g] = _freqBand[g];
//				_bufferDecrease[g] = 0.005f;
//			}

//			if (_freqBand[g] < _bandBuffer[g])
//			{
//				_bandBuffer[g] -= _bufferDecrease[g];
//				_bufferDecrease[g] *= 1.2f;
//			}
//		}
//	}

//	void CreateAudioBands()
//	{
//		for (int i = 0; i < 8; i++)
//		{
//			if (_freqBand[i] > _freqBandHighest[i])
//			{
//				_freqBandHighest[i] = _freqBand[i];
//			}

//			_audioBand[i] = _freqBand[i] / _freqBandHighest[i];
//			var newBandBuffer = _bandBuffer[i] / _freqBandHighest[i];

//			AudioBandBuffer[i] = float.IsNaN(newBandBuffer) ? 0f : newBandBuffer;
//		}
//	}
//}