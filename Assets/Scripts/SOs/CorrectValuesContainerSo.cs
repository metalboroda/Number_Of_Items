using UnityEngine;

namespace Assets.Scripts.SOs
{
  [CreateAssetMenu(fileName = "CorrectValuesContainer", menuName = "SOs/Containers/CorrectValuesContainer")]
  public class CorrectValuesContainerSo : ScriptableObject
  {
    [field: SerializeField] public Sprite Sprite {  get; private set; }
    [field: SerializeField] public string Value { get; private set; }
  }
}