using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeWheels : MonoBehaviour
{
    [SerializeField] private GameObject[] _typeUpgradeWheels;
    [SerializeField] private float deltaChangeSpeed;

    private GameObject _currentTypeUpgrade;
    private int _indexCurrentTypeUpgrade;

    #region Injects

    protected PlayerMove PlayerMove;
    private MainCameraBrain _mainCameraBrain;
    private PlayerMoveInUpgrade _playerMoveInUpgrade;
    private PurchaseControl _purchaseControl;

    [Inject]
    private void Construct(PlayerMove playerMove,
                           MainCameraBrain mainCameraBrain,
                           PlayerMoveInUpgrade playerMoveInUpgrade,
                           PurchaseControl purchaseControl)
    {
        PlayerMove = playerMove;
        _mainCameraBrain = mainCameraBrain;
        _playerMoveInUpgrade = playerMoveInUpgrade;
        _purchaseControl = purchaseControl;
    }

    #endregion

    public void LocalAwake()
    {
        ChangeGroup(_typeUpgradeWheels[_indexCurrentTypeUpgrade]);
    }

    private void ChangeGroup(GameObject group)
    {
        if (_currentTypeUpgrade != null)
        {
            _currentTypeUpgrade.gameObject.SetActive(false);
        }

        _currentTypeUpgrade = group;

        if (_currentTypeUpgrade != null)
            _currentTypeUpgrade.gameObject.SetActive(true);
    }

    public void ChangeWheels()
    {
        if (_indexCurrentTypeUpgrade == _typeUpgradeWheels.Length - 1) return;

        ChangeGroup(_typeUpgradeWheels[++_indexCurrentTypeUpgrade]);
        ChangeSpeed();
    }

    private void ChangeSpeed()
    {
        _mainCameraBrain.ChangeTimeMoveCamera();
        _playerMoveInUpgrade.ChangeDurationMove();

        PlayerMove.UpgradeSpeed(deltaChangeSpeed);
    }
}
