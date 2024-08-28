using Assets.Scripts.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.Game.States
{
  public class GameQuestState : GameBaseState
  {
    public GameQuestState(GameBootstrapper gameBootstrapper) : base(gameBootstrapper) {
    }

    public override void Enter() {
      Time.timeScale = 0f;
    }
  }
}