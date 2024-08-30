using System;
using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  [Serializable]
  public class RoomItem
  {
    [SerializeField] private BoxHandler[] _boxHandlers;
    [SerializeField] private GameObject[] _spawnersContainers;

    public BoxHandler[] BoxHandlers => _boxHandlers;
    public GameObject[] SpawnersContainers => _spawnersContainers;
  }
}