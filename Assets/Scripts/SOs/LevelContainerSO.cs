using UnityEngine;

namespace Assets.Scripts.SOs
{
  [CreateAssetMenu(fileName = "LevelContainer", menuName = "SOs/Containers/LevelContainer")]

  public class LevelContainerSO : ScriptableObject
  {
    [SerializeField] private GameObject[] _levelPrefabs;

    public GameObject[] LevelPrefabs => _levelPrefabs;
  }
}