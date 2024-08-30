using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class RoomManager : MonoBehaviour
  {
    [SerializeField] private BoxHandler[] _boxHandlers;
    [SerializeField] private GameObject[] _spawnersContainers;

    private int _completedBoxesCount;

    private void Awake() {
      _completedBoxesCount = 0;

      foreach (var boxHandler in _boxHandlers)
        boxHandler.gameObject.SetActive(false);

      foreach (var spawner in _spawnersContainers)
        spawner.SetActive(false);

      if (_boxHandlers.Length > 0)
        _boxHandlers[0].gameObject.SetActive(true);
      if (_spawnersContainers.Length > 0)
        _spawnersContainers[0].SetActive(true);
    }

    private void OnEnable() {
      foreach (BoxHandler boxHandler in _boxHandlers)
        boxHandler.BoxCompleted += OnBoxCompleted;
    }

    private void OnDisable() {
      foreach (BoxHandler boxHandler in _boxHandlers)
        boxHandler.BoxCompleted -= OnBoxCompleted;
    }

    private void OnBoxCompleted() {
      _completedBoxesCount++;

      _boxHandlers[_completedBoxesCount - 1].gameObject.SetActive(false);
      _spawnersContainers[_completedBoxesCount - 1].SetActive(false);

      if (_completedBoxesCount < _boxHandlers.Length) {
        _boxHandlers[_completedBoxesCount].gameObject.SetActive(true);
        _spawnersContainers[_completedBoxesCount].SetActive(true);
      }
      else {
        AllBoxesCompleted();
      }
    }

    private void AllBoxesCompleted() {
      Debug.Log("Всі коробки завершені!");
    }
  }
}