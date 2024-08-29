using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class DragObject : MonoBehaviour
  {
    private Vector3 _offset;

    private Camera _mainCamera;
    private BoxItemHandler _draggingObject;

    private void Start() {
      _mainCamera = Camera.main;
    }

    private void Update() {
      if (Input.GetMouseButtonDown(0)) {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
          _draggingObject = hit.collider.GetComponent<BoxItemHandler>();

          if (_draggingObject != null) {
            _offset = hit.point - hit.transform.position;
          }
        }
      }

      if (_draggingObject != null && Input.GetMouseButton(0)) {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, _draggingObject.transform.position);

        if (plane.Raycast(ray, out float distance)) {
          Vector3 point = ray.GetPoint(distance) - _offset;
          _draggingObject.transform.position = new Vector3(point.x, _draggingObject.transform.position.y, point.z);
        }
      }

      if (Input.GetMouseButtonUp(0)) {
        _draggingObject = null;
      }
    }
  }
}