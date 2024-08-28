using Assets.Scripts.Game.States;
using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.States
{
  public class GamePauseState : GameBaseState
  {
    public GamePauseState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper) {
    }

    public override void Enter() {
      Time.timeScale = 0f;
    }

    public override void Exit() {
      Time.timeScale = 1f;
    }
  }
}