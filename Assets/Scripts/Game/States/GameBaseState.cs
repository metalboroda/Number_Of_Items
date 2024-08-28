using Assets.Scripts.Infrastructure;
using Assets.Scripts.StateMachine;

namespace Assets.Scripts.Game.States
{
  public abstract class GameBaseState : State
  {
    protected GameBootstrapper GameBootstrapper;
    protected FiniteStateMachine StateMachine;
    protected SceneLoader SceneLoader;

    protected GameBaseState(GameBootstrapper gameBootstrapper) {
      GameBootstrapper = gameBootstrapper;
      StateMachine = GameBootstrapper.StateMachine;
      SceneLoader = GameBootstrapper.SceneLoader;
    }
  }
}