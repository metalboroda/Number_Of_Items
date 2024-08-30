using System;
using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class BoxHandler : MonoBehaviour
  {
    public event Action BoxCompleted;
    public event Action GameLost;

    [SerializeField] private int _receiveLimit = 5;

    [Header("")]
    [SerializeField] private BoxCollider _boxCollider;

    private int _receiveCounter = 0;
    private ItemType _firstItemType;

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent<BoxItemHandler>(out var boxItemHandler)) {
        if (_receiveCounter == 0) {
          _firstItemType = boxItemHandler.ItemType;
        }
        else {
          if (boxItemHandler.ItemType != _firstItemType) {
            Debug.Log("Item types do not match. You lost!");

            _boxCollider.enabled = false;
          }
        }

        _receiveCounter++;

        Destroy(boxItemHandler.gameObject);

        if (_receiveCounter >= _receiveLimit) {
          Debug.Log("Limit reached");

          BoxCompleted?.Invoke();
        }
      }
    }
  }
}