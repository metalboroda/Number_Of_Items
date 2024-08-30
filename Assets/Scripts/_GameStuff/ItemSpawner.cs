using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class ItemSpawner : MonoBehaviour
  {
    [SerializeField] private SpawnerItem[] _items;

    private CapsuleCollider _capsuleCollider;

    private void Awake() {
      _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start() {
      SpawnItems();
    }

    private void SpawnItems() {
      foreach (var item in _items) {
        for (int i = 0; i < item.Amount; i++) {
          Vector3 randomPosition = GetRandomPositionInsideSphere();

          float randomVector = Random.Range(-360f, 360f);
          Quaternion randomRotation = Quaternion.Euler(randomVector, randomVector, randomVector);

          Instantiate(item.BoxItemHandler, randomPosition, randomRotation, transform);
        }
      }

      _capsuleCollider.enabled = false;
    }

    private Vector3 GetRandomPositionInsideSphere() {
      Vector3 randomDirection = Random.insideUnitSphere;
      Vector3 randomPosition = _capsuleCollider.center + randomDirection * _capsuleCollider.radius;

      return transform.TransformPoint(randomPosition);
    }
  }
}