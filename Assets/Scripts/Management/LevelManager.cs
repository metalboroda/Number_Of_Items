using __Game.Resources.Scripts.EventBus;
using Assets.Scripts.Enums;
using Assets.Scripts.Settings;
using Assets.Scripts.SOs;
using UnityEngine;

namespace Assets.Scripts.Management
{
  public class LevelManager : MonoBehaviour
  {
    [SerializeField] private LevelContainerSO _levelContainerSO;

    private int _overallLevelIndex = 0;
    private int _currentLevelIndex = 0;
    private GameObject _currentLevelPrefab;

    private GameSettings _gameSettings;

    private EventBinding<EventStructs.UiButtonEvent> _uiButtonEvent;

    private void Awake() {
      LoadSettings();
    }

    private void OnEnable() {
      _uiButtonEvent = new EventBinding<EventStructs.UiButtonEvent>(LoadNextLevel);
    }

    private void OnDisable() {
      _uiButtonEvent.Remove(LoadNextLevel);
    }

    private void Start() {
      SetSavedLevel();
      LoadLevel(_currentLevelIndex);
      OnLastLevelOfTheList();
    }

    public void LoadLevel(int levelIndex) {
      if (levelIndex >= _levelContainerSO.LevelPrefabs.Length)
        levelIndex = Random.Range(1, _levelContainerSO.LevelPrefabs.Length);

      if (levelIndex < _levelContainerSO.LevelPrefabs.Length) {
        _currentLevelPrefab = _levelContainerSO.LevelPrefabs[levelIndex];

        Instantiate(_currentLevelPrefab);
      }
    }

    private void LoadNextLevel(EventStructs.UiButtonEvent uiButtonEvent) {
      if (uiButtonEvent.UiEnums != UiEnums.WinNextLevelButton) return;

      _overallLevelIndex++;
      _gameSettings.OverallLevelIndex = _overallLevelIndex;
      _currentLevelIndex++;
      _gameSettings.LevelIndex = _currentLevelIndex;

      if (_currentLevelIndex >= _levelContainerSO.LevelPrefabs.Length)
        _currentLevelIndex = Random.Range(0, _levelContainerSO.LevelPrefabs.Length);

      SettingsManager.SaveSettings(_gameSettings);
      //LoadLevel(_currentLevelIndex);
    }

    private void LoadSettings() {
      _gameSettings = SettingsManager.LoadSettings<GameSettings>();

      if (_gameSettings == null)
        _gameSettings = new GameSettings();
    }

    private void SetSavedLevel() {
      _currentLevelIndex = _gameSettings.LevelIndex;
      _overallLevelIndex = _gameSettings.OverallLevelIndex;
    }

    private void OnLastLevelOfTheList() {
      if (_currentLevelIndex == _levelContainerSO.LevelPrefabs.Length - 1)
        EventBus<EventStructs.LastLevelEvent>.Raise(new EventStructs.LastLevelEvent { LastLevel = true });
    }
  }
}