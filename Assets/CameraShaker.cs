using UnityEngine;
using System.Collections;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	Vector3 originalPos;

	Vector3 ogPos;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

	void FixedUpdate()
	{
		

		if (shakeDuration > 0)
		{
			Camera.main.GetComponent<CinemachineBrain>().enabled = false;
			camTransform.position = ogPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.fixedDeltaTime * decreaseFactor;


		}
		else
		{
			Camera.main.GetComponent<CinemachineBrain>().enabled = true;

			shakeDuration = 0f;
			ogPos = Camera.main.transform.position;
			
		}
	}
}