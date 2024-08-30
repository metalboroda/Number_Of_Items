﻿using Assets.Resources.Scripts.Game.States;
using Assets.Scripts.Infrastructure;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts._GameStuff
{
  public class BoxHandler : MonoBehaviour
  {
    public event Action BoxCompleted;

    [SerializeField] private int _receiveLimit = 5;

    [Header("")]
    [SerializeField] private TextMeshProUGUI _limitText;

    [Header("References")]
    [SerializeField] private BoxCollider _boxCollider;

    private int _receiveCounter = 0;
    private int _receivedCounter = 0;
    private ItemType _firstItemType;

    private GameBootstrapper _gameBootstrapper;

    private void Awake() {
      _gameBootstrapper = GameBootstrapper.Instance;

      _receivedCounter = _receiveLimit;
      _limitText.text = _receivedCounter.ToString();
    }

    private void OnTriggerEnter(Collider other) {
      if (other.TryGetComponent<BoxItemHandler>(out var boxItemHandler)) {
        if (_receiveCounter == 0) {
          _firstItemType = boxItemHandler.ItemType;
        }
        else {
          if (boxItemHandler.ItemType != _firstItemType) {
            _gameBootstrapper.StateMachine.ChangeState(new GameLoseState(_gameBootstrapper));

            _boxCollider.enabled = false;
          }
        }

        _receiveCounter++;
        _receivedCounter--;
        _limitText.text = _receivedCounter.ToString();

        Destroy(boxItemHandler.gameObject);

        if (_receiveCounter >= _receiveLimit)
          BoxCompleted?.Invoke();
      }
    }
  }
}