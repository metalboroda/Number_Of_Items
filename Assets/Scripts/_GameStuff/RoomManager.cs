using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class RoomManager : MonoBehaviour
  {
    [SerializeField] private RoomItem[] _roomItems;

    private int _currentItemIndex;
    private int _completedBoxesCountInCurrentItem;

    private void Awake() {
      _currentItemIndex = 0;
      _completedBoxesCountInCurrentItem = 0;

      if (_roomItems.Length > 0) {
        ActivateBoxHandlerAndSpawner(_roomItems[_currentItemIndex]);
      }
    }

    private void OnEnable() {
      foreach (var item in _roomItems) {
        foreach (var boxHandler in item.BoxHandlers)
          boxHandler.BoxCompleted += OnBoxCompleted;
      }
    }

    private void OnDisable() {
      foreach (var item in _roomItems) {
        foreach (var boxHandler in item.BoxHandlers)
          boxHandler.BoxCompleted -= OnBoxCompleted;
      }
    }

    private void OnBoxCompleted() {
      _completedBoxesCountInCurrentItem++;

      if (_completedBoxesCountInCurrentItem >= _roomItems[_currentItemIndex].BoxHandlers.Length) {
        _currentItemIndex++;

        if (_currentItemIndex < _roomItems.Length) {
          _completedBoxesCountInCurrentItem = 0;

          ActivateBoxHandlerAndSpawner(_roomItems[_currentItemIndex]);
        }
        else {
          AllBoxesCompleted();
        }
      }
    }

    private void ActivateBoxHandlerAndSpawner(RoomItem item) {
      if (item.BoxHandlers.Length > 0 && !item.BoxHandlers[0].gameObject.activeSelf)
        item.BoxHandlers[0].gameObject.SetActive(true);

      if (item.SpawnersContainers.Length > 0 && !item.SpawnersContainers[0].activeSelf)
        item.SpawnersContainers[0].SetActive(true);
    }

    private void AllBoxesCompleted() {
      Debug.Log("Всі коробки завершені!");
    }
  }
}