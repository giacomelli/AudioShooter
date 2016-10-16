//using UnityEngine;
//using System.Collections;
//using System;
//using System.Linq;
//using System.Collections.Generic;

//public class AudioRealtimeService : MonoBehaviour
//{
//	public int _totalSamples = 512;
//	private int _samplesMultiplier;


//	public float _warmupSeconds;
//	public float _readSoundSeconds;
//	float _samplesByRead;

//	public float _tickSeconds;
//	public int _tickEveryReads;
//	public AudioSource _musicToAnalyze;
//	public AudioSource _musicToPlay;
//	public static float[] _samples;
//	static float[] _freqBand = new float[8];
//	static float[] _bandBuffer = new float[8];
//	float[] _bufferDecrease = new float[8];
//	float[] _freqBandHighest = new float[8];
//	public static float[] _audioBand = new float[8];
//	public static float[] AudioBandBuffer = new float[8];
//	public static event EventHandler SoundTick;
//	public static event EventHandler Initialized;

//	float soundTimeEachRead;
//	public static int Ticks { get; private set; }
//	public static float AudioDuration { get; private set; }
//	public static float TotalSoundsRead { get; private set; }
//	public static bool AudioAnalyzed { get; private set; }

//	float[] _audioData;

//	// Use this for initialization
//	void Awake()
//	{
//		SoundTick += (sender, e) =>
//		{
//			//Debug.Log("tick");
//		};


//		var clip = _musicToAnalyze.clip;
//		_audioData = new float[clip.samples];
//		clip.GetData(_audioData, 0);

//		Debug.Log(_audioData.Length);
//		AudioDuration = clip.length;

//		_samples = new float[_totalSamples];
//		_samplesMultiplier = _totalSamples / 256;

//		_samplesByRead = clip.frequency * _readSoundSeconds;

//		soundTimeEachRead = _musicToAnalyze.clip.length / _readSoundSeconds;
//		TotalSoundsRead = clip.length / _readSoundSeconds;
//		Debug.Log(TotalSoundsRead);
//		Debug.Log(1f / TotalSoundsRead);

//	}

//	void Start()
//	{
//		StartCoroutine(AnalyzeSound());
//		//StartCoroutine(ReadSound());
//	}

//	List<float[]> _allAudioBandBuffer = new List<float[]>();

//	IEnumerator AnalyzeSound()
//	{
//		MessagesController.Instance.ChangeCentralMessage("Analizing sound...");
//		yield return new WaitForEndOfFrame();

//		Debug.LogWarning("Analyzing sound...");

//		secoundsRead = 0;
//		var soundTicks = 0f;
//		for (float i = 0; i < _musicToAnalyze.clip.length; i += _readSoundSeconds)
//		{
//			secoundsRead += _readSoundSeconds;

//			var samplesOffset = Mathf.RoundToInt(secoundsRead * _musicToAnalyze.clip.frequency);
//			Debug.Log("offset: " + samplesOffset);

//			// Does the music ended?
//			if (samplesOffset + _totalSamples > _audioData.Length)
//			{
//				Debug.LogWarning("Music ended!");
//				break;
//			}

//			Array.Copy(_audioData, samplesOffset, _samples, 0, _totalSamples);

//			//Debug.Log(samplesOffset);
//			//Debug.Log(String.Join(", ",_audioData.Select(s => s.ToString()).ToArray()));
//			//Debug.Log(String.Join(", ",_samples.Select(s => s.ToString()).ToArray()));

//			//GetSpecturumAudioSource();

//			MakeFrequencyBands();
//			BandBuffer();
//			CreateAudioBands();

//			soundTicks += _readSoundSeconds * _musicToAnalyze.clip.frequency;
//			Debug.LogFormat("{0} | {1}", soundTicks, _musicToAnalyze.clip.frequency);
//			//Debug.Log(AudioBandBuffer[2]);

//			if (soundTicks >= _musicToAnalyze.clip.frequency)
//			{
//				Ticks++;

//				Debug.LogWarning("tick");
//				soundTicks = 0;
//				SoundTick(this, EventArgs.Empty);
//				MessagesController.Instance.ChangeCentralMessage("Generating {0,2} seconds...".With(Ticks));
//				yield return new WaitForEndOfFrame();
//			}

//			//var newAudioBundBuffer = new float[_totalSamples];
//			//AudioBandBuffer.CopyTo(newAudioBundBuffer, 0); 

//			//_allAudioBandBuffer.Add(newAudioBundBuffer);

//		}

//		Debug.LogWarningFormat("All audio band buffer count: {0}", _allAudioBandBuffer.Count);
//		MessagesController.Instance.ChangeCentralMessage("");

//		_freqBand = new float[8];
//		_bandBuffer = new float[8];
//		_bufferDecrease = new float[8];
//		_freqBandHighest = new float[8];
//		_audioBand = new float[8];
//		AudioBandBuffer = new float[8];

//		_musicToPlay.Play();
//		AudioAnalyzed = true;
//		//Initialized(this, EventArgs.Empty);
//	}

//	float t;
//	float secoundsRead = 0f;//_warmupSeconds;

//	IEnumerator ReadSound()
//	{
//		var soundTicks = _tickEveryReads;
//		//_musicToAnalyze.velocityUpdateMode = AudioVelocityUpdateMode.Dynamic;

//		////_musicToAnalyze.Play();
//		//_musicToPlay.Play();


//		//yield return new WaitForSecondsRealtime(_warmupSeconds);

//		for (int i = 0; i < _allAudioBandBuffer.Count; i++)
//		{
//			//_musicToAnalyze.Pause();
//			//_musicToAnalyze.timeSamples = 10000;//secoundsRead;
//			//_musicToAnalyze.timeSamples = Mathf.RoundToInt(secoundsRead * 100000f);
//			//Debug.Log(_musicToAnalyze.timeSamples);
//			//_musicToAnalyze.SetScheduledEndTime(12);

//			AudioBandBuffer = _allAudioBandBuffer[i];

//			soundTicks++;

//			//Debug.Log(AudioBandBuffer[2]);
//			if (soundTicks >= _tickEveryReads)
//			{
//				Ticks++;

//				Debug.LogWarning("tick");
//				soundTicks = 0;
//				SoundTick(this, EventArgs.Empty);
//			}

//			Debug.LogFormat("{0} / {1}", i, _allAudioBandBuffer.Count);
//			yield return new WaitForSeconds(_readSoundSeconds);
//		}
//	}

//	void Update()
//	{
//		if (AudioAnalyzed)
//		{
//			GetSpecturumAudioSource();
//			MakeFrequencyBands();
//			BandBuffer();
//			CreateAudioBands();
//		}
//	}

//	//IEnumerator RaiseSoundTick()
//	//{
//	//	while (true)
//	//	{
//	//		SoundTick(this, EventArgs.Empty);
//	//		yield return new WaitForSecondsRealtime(_tickSeconds);
//	//	}
//	//}

//	void GetSpecturumAudioSource()
//	{
//		_musicToPlay.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
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