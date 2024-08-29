using System;
using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  [Serializable]
  public class SpawnerItem
  {
    [SerializeField] private BoxItemHandler _boxItemHandler;
    [Space]
    [SerializeField] private int _amount;

    public BoxItemHandler BoxItemHandler => _boxItemHandler;
    public int Amount => _amount;
  }
}