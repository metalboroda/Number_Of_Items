using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class BoxesManager : MonoBehaviour
  {
    private int _completedBoxesCount;

    private BoxHandler[] _boxHandlers;

    private void Awake() {
      _boxHandlers = GetComponentsInChildren<BoxHandler>();

      _completedBoxesCount = 0;
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

      if (_completedBoxesCount >= _boxHandlers.Length) {
        AllBoxesCompleted();
      }
    }

    private void AllBoxesCompleted() {
      // Виконати щось, коли всі коробки завершені
      Debug.Log("Всі коробки завершені!");
      // Додайте тут код для подальших дій
    }
  }
}