using UnityEngine;

/// <summary>
/// Script for panning and zooming map
/// </summary>
public class PanZoom : MonoBehaviour
{
	private Vector3 direction;
	[SerializeField]
	private float minZoomOut = 1f;
	[SerializeField]
	private float maxZoomOut = 8f;

	private Camera mainCamera;

	public bool canClickOnTiles = true;

	// set this value to first position of mouse/finger but in ScreenToWorldPoint
	private Vector3 mousePos;

	// set this value to first position of mouse/finger
	private Vector3 firstMousePos;

	// set this value to first position of mouse/finger, but after moving
	private Vector3 secondMousePos;

	[SerializeField]
	private float minX, minY, maxX, maxY;

	/// <summary>
	/// Starts this instance
	/// </summary>
	void Start()
	{
		mainCamera = Camera.main.GetComponent<Camera>();
		Vector3 stageDimensions = mainCamera.ScreenToWorldPoint
			(new Vector3(Screen.width, Screen.height, 0));
		Debug.Log(stageDimensions);
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			firstMousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.touchCount == 2)
		{
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

			float difference = currentMagnitude - prevMagnitude;

			Zoom(difference * 0.01f);
		}

		else if (Input.GetMouseButton(0))
		{
			secondMousePos = Input.mousePosition;

			if (firstMousePos == secondMousePos)
			{
				canClickOnTiles = true;
			}
			else
			{
				canClickOnTiles = false;
				direction = mousePos - mainCamera.ScreenToWorldPoint(Input.mousePosition);
				mainCamera.transform.position += direction;

				transform.position = new Vector3(Mathf.Clamp(transform.position.x,
					minX, maxX), Mathf.Clamp(transform.position.y,
					minY, maxY), transform.position.z);
			}
		}
	}

	/// <summary>
	/// Zooms
	/// </summary>
	/// <param name="increment">The increment.</param>
	void Zoom(float increment)
	{
		mainCamera.orthographicSize = Mathf.Clamp(
			mainCamera.orthographicSize - increment, minZoomOut, maxZoomOut);
	}
}
