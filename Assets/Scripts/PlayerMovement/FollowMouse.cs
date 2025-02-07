using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	void Start()
	{
		Cursor.visible = false;
	}

	void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
	}
}
