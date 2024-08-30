using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Game.States;
using Assets.Scripts.Infrastructure;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class RoomManager : MonoBehaviour
  {
    [SerializeField] private RoomItem[] _roomItems;

    [Header("Tutorial")]
    [SerializeField] private bool _needTutorial = false;
    [Space]
    [SerializeField] private GameObject _tutorialFinger;

    private int _currentItemIndex;
    private int _completedBoxesCountInCurrentItem;

    private GameBootstrapper _gameBootstrapper;

    private EventBinding<EventStructs.ItemClicked> _itemClicked;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;

      _currentItemIndex = 0;
      _completedBoxesCountInCurrentItem = 0;

      if (_roomItems.Length > 0)
        ActivateBoxHandlerAndSpawner(_roomItems[_currentItemIndex]);
    }

    private void Start() {
      Tutorial();
    }

    private void OnEnable() {
      foreach (var item in _roomItems) {
        foreach (var boxHandler in item.BoxHandlers)
          boxHandler.BoxCompleted += OnBoxCompleted;
      }

      _itemClicked = new EventBinding<EventStructs.ItemClicked>(OnItemClicked);
    }

    private void OnDisable() {
      foreach (var item in _roomItems) {
        foreach (var boxHandler in item.BoxHandlers)
          boxHandler.BoxCompleted -= OnBoxCompleted;
      }

      _itemClicked.Remove(OnItemClicked);
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
      _gameBootstrapper.StateMachine.ChangeState(new GameWinState(_gameBootstrapper));
    }

    private void Tutorial() {
      if (_needTutorial == false) {
        _tutorialFinger.SetActive(false);
        return;
      }
    }

    private void OnItemClicked() {
      _tutorialFinger?.SetActive(false);
    }
  }
}