using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform worldSpaceCanvases;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private Vector3 originalPos;

	void Awake()
	{
		if (worldSpaceCanvases == null)
		{
			worldSpaceCanvases = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		originalPos = worldSpaceCanvases.localPosition;
	}

	void Update()
	{
		if (shakeDuration > 0)
		{
			worldSpaceCanvases.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shakeDuration = 0f;
			worldSpaceCanvases.localPosition = originalPos;
		}
	}
}
