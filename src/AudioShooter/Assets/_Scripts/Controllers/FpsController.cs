using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FpsController : MonoBehaviour
{

	#region Fields
	private float m_accum = 0;
	private int m_frames = 0;
	private float m_timeLeft;
	#endregion

	#region Properties
	public float UpdateInterval = 0.5F;
	public Text _label;
	#endregion

	#region Life cycle
	private void Start()
	{
		m_timeLeft = UpdateInterval;
	}

	private void Update()
	{
		m_timeLeft -= Time.deltaTime;
		m_accum += Time.timeScale / Time.deltaTime;
		++m_frames;

		// Interval ended - update HUd text and start new interval.
		if (m_timeLeft <= 0.0)
		{
			// Display two fractional digits (f2 format).
			float fps = m_accum / m_frames;
			_label.text = System.String.Format("{0:F2} FPS", fps);

			m_timeLeft = UpdateInterval;
			m_accum = 0.0F;
			m_frames = 0;
		}
	}
	#endregion
}