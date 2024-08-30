using __Game.Resources.Scripts.EventBus;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts._GameStuff
{
  public class BoxItemHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
  {
    [SerializeField] private ItemType _itemType;

    [Header("")]
    [SerializeField] private float _yPosition = 0.25f;

    public ItemType ItemType => _itemType;

    private Vector3 _offset;

    private Rigidbody _rigidbody;
    private MeshCollider _meshCollider;

    private void Awake() {
      _rigidbody = GetComponent<Rigidbody>();
      _meshCollider = GetComponent<MeshCollider>();
    }

    public void OnPointerDown(PointerEventData eventData) {
      _rigidbody.isKinematic = true;
      _meshCollider.enabled = false;
      _offset = transform.position - GetMouseWorldPosition();

      Vector3 newPosition = transform.position;

      newPosition.y = _yPosition;

      transform.position = newPosition;

      EventBus<EventStructs.ItemClicked>.Raise(new EventStructs.ItemClicked());
    }

    public void OnDrag(PointerEventData eventData) {
      Vector3 newPosition = GetMouseWorldPosition() + _offset;

      newPosition.y = _yPosition;

      transform.position = newPosition;
    }

    public void OnPointerUp(PointerEventData eventData) {
      _rigidbody.isKinematic = false;
      _meshCollider.enabled = true;
    }

    private Vector3 GetMouseWorldPosition() {
      Vector3 mousePoint = Input.mousePosition;

      mousePoint.z = Camera.main.WorldToScreenPoint(transform.position).z;

      return Camera.main.ScreenToWorldPoint(mousePoint);
    }
  }
}