using Assets.Scripts.Game.States;
using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Resources.Scripts.Game.States
{
  public class GameplayState : GameBaseState
  {
    public GameplayState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper) {
    }

    public override void Enter() {
      Time.timeScale = 1f;
    }
  }
}