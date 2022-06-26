using UnityEngine;

/// <summary>
/// Script for panning and zooming map
/// </summary>
public class PanZoom : MonoBehaviour
{
	private Vector3 touchStart;
	private Vector3 direction;
	public float minZoomOut = 1f;
	public float maxZoomOut = 8f;

	private Camera mainCamera;

	//[HideInInspector]
	public bool doubleClick = false;

	[SerializeField]
	private float lastClickTime;
	[SerializeField]
	private float timeSinceLastClick;
	[SerializeField]
	private float DOUBLE_CLICK_TIME = .5f;

	[SerializeField]
	private float minX, minY, maxX, maxY;
	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main.GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			touchStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			timeSinceLastClick = Time.time - lastClickTime;
			if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
			{
				// double click!
				doubleClick = true;
			}
			else
			{
				// normal click
				doubleClick = false;
			}
			lastClickTime = Time.time;
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
			direction = touchStart - mainCamera.ScreenToWorldPoint(Input.mousePosition);
			mainCamera.transform.position += direction;

			transform.position = new Vector3(Mathf.Clamp(transform.position.x,
				minX, maxX), Mathf.Clamp(transform.position.y,
				minY, maxY), transform.position.z);
		}

	}

	void Zoom(float increment)
	{
		mainCamera.orthographicSize = Mathf.Clamp(
			mainCamera.orthographicSize - increment, minZoomOut, maxZoomOut);
	}
}