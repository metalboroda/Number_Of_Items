using __Game.Resources.Scripts.EventBus;
using Assets.Resources.Scripts.Game.States;
using Assets.Scripts.Enums;
using Assets.Scripts.Game.States;
using Assets.Scripts.Hashes;
using Assets.Scripts.Settings;
using Assets.Scripts.StateMachine;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    public static GameBootstrapper Instance { get; private set; }

    public FiniteStateMachine StateMachine;
    public SceneLoader SceneLoader;

    public bool QuestStateOnce { get; set; }

    public GameBootstrapper() {
      StateMachine = new FiniteStateMachine();
      SceneLoader = new SceneLoader();
    }

    private EventBinding<EventStructs.UiButtonEvent> _uiButtonEvent;

    private void Awake() {
      SettingsManager.ResetSettings();
      InitSingleton();
    }

    private void OnEnable() {
      _uiButtonEvent = new EventBinding<EventStructs.UiButtonEvent>(ChangeState);
    }

    private void OnDisable() {
      _uiButtonEvent.Remove(ChangeState);
    }

    private void Start() {
      SceneLoader.LoadSceneAsyncWithDelay(GameHashes.GameScene, 2f, this, () => {
        if (QuestStateOnce == false) {
          StateMachine.Init(new GameQuestState(this));

          QuestStateOnce = true;
        }
        else {
          StateMachine.ChangeState(new GameplayState(this));
        }

        EventBus<EventStructs.ComponentEvent<GameBootstrapper>>.Raise(
          new EventStructs.ComponentEvent<GameBootstrapper> { Data = this });
      });
    }

    public void RestartLevel() {
      SceneLoader.RestartSceneAsync(() => {
        if (QuestStateOnce == false) {
          StateMachine.Init(new GameQuestState(this));
        }
        else {
          StateMachine.ChangeState(new GameplayState(this));
        }

        EventBus<EventStructs.ComponentEvent<GameBootstrapper>>.Raise(
          new EventStructs.ComponentEvent<GameBootstrapper> { Data = this });
      });
    }

    private void InitSingleton() {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);
      }
      else {
        Instance = this;

        DontDestroyOnLoad(gameObject);
      }
    }

    private void ChangeState(EventStructs.UiButtonEvent uiButtonEvent) {
      switch (uiButtonEvent.UiEnums) {
        case UiEnums.QuestPlayButton:
          StateMachine.ChangeState(new GameplayState(this));
          break;
        case UiEnums.GamePauseButton:
          StateMachine.ChangeState(new GamePauseState(this));
          break;
        case UiEnums.PauseContinueButton:
          StateMachine.ChangeState(new GameplayState(this));
          break;
      }
    }
  }
}