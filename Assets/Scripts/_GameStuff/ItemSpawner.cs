using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class ItemSpawner : MonoBehaviour
  {
    [SerializeField] private float _sphereRadius = 5f;
    [SerializeField] private Vector3 _sphereCenter = Vector3.zero;

    [Header("")]
    [SerializeField] private SpawnerItem[] _items;

    public SpawnerItem[] Items => _items;

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
    }

    private Vector3 GetRandomPositionInsideSphere() {
      Vector3 randomDirection = Random.insideUnitSphere.normalized;
      Vector3 randomPosition = _sphereCenter + randomDirection * Random.Range(0, _sphereRadius);

      return transform.TransformPoint(randomPosition);
    }

    private void OnDrawGizmos() {
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.TransformPoint(_sphereCenter), _sphereRadius);
    }
  }
}