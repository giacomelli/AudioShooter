using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class ColorTransformer : SoundMonoBehaviour {
	public Color _minColor;
	public Color _maxColor;
	public bool RandomColor;

	Color _colorRange;
	Material _material;

	// Use this for initialization
	void Start () {

		if (RandomColor)
		{
			_minColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
			_maxColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		}

		_colorRange = _maxColor - _minColor;
		_material = GetComponent<MeshRenderer>().materials[0];
	}
	
	// Update is called once per frame
	void Update () {
		_material.color = AudioService.AudioBandBuffer[Config._band] * _colorRange + _minColor;
	}
}
