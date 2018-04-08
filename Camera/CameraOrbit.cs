
using UnityEngine;
public class CameraOrbit : MonoBehaviour
{

	#region Player

	[SerializeField] private Transform target;
	new private Camera camera;

	private bool MouseLocked { get { return Cursor.lockState == CursorLockMode.Locked ? true : false; } }

	[SerializeField] private float lerp = 1f;
	[SerializeField] private Vector3 offset = Vector3.up * 3;
	[SerializeField] private float distance = 15;
	[SerializeField] private float minDistance = 5f;
	[SerializeField] private float maxDistance = 15f;
	#endregion

	private void Awake()
	{
		camera = GetComponent<Camera>();
	}

	public Transform Target
	{
		set
		{
			target = value;
		}
	}

	void LateUpdate()
	{
		if (MouseLocked)
			PlayerInput();

		if (Input.GetKeyDown(KeyCode.F))
			ToggleMouseLock();
	}

	float x = 0;
	float y = 0;

	private void PlayerInput()
	{
		lerp = Mathf.Clamp(lerp - Input.GetAxis("Mouse ScrollWheel"), 0, 1);
		distance = Mathf.Lerp(minDistance, maxDistance, lerp);
		camera.fieldOfView = Mathf.Lerp(30, 60, lerp);

		x = x + Input.GetAxis("Mouse X") * 2;
		y = Mathf.Clamp(y - Input.GetAxis("Mouse Y") * 2, -90, 90);
		transform.rotation = Quaternion.Euler(y, x, 0);
		transform.position = target.TransformPoint(offset) - transform.forward * distance;
	}

	private void ToggleMouseLock()
	{
		if (MouseLocked)
		{
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}