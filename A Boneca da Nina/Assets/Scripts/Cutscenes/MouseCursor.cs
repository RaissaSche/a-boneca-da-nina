using UnityEngine;

public class MouseCursor : MonoBehaviour {

	/*void Start () {
		Cursor.visible = false;
	}*/

	void Update () {
		Vector2 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = cursorPos;
	}
}