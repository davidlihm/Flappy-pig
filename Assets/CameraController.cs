using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public float dampTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	public Transform target1;
	public Transform target2;
	public Transform light;
	public Transform plane;
	void Start () {

	}
	
	
	void Update () {
		Vector3 avePosition = (target2.position + target1.position) / 2;
		if(target1 && target2)
		{
			Vector3 point = camera.WorldToViewportPoint(avePosition);
			Vector3 delta = avePosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta + new Vector3(3,0,0);
			destination.y = 0;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			plane.position = transform.position+new Vector3(0,0,14);
		}
		light.position = avePosition - new Vector3(-5,10,10);
	}
}
