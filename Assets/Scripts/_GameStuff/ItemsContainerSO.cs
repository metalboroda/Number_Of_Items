using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class ItemsContainerSO : ScriptableObject
  {
    [SerializeField] private BoxItemHandler[] _boxItemHandlers;

    public BoxItemHandler[] BoxItemHandlers => _boxItemHandlers;
  }
}