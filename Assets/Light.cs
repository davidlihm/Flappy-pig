using UnityEngine;
using System.Collections;

public class Light : MonoBehaviour {
	public Transform camera;
	// Use this for initialization
	void Start () {
		gameObject.transform.position = camera.position+ new Vector3(0,5,5);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = camera.position+ new Vector3(0,5,5);
	}
}
