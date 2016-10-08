using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float _velocity;

	void Update () {
		var target = new Vector3(transform.position.x, transform.position.y, MountainDeployer.CurrentZ);
		transform.position = Vector3.Lerp(transform.position, target, _velocity);
	}
}
