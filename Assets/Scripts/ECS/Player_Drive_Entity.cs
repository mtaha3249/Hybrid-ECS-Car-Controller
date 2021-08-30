using System;
using Unity.Entities;
using UnityEngine;

public class Player_Drive_Entity : MonoBehaviour
{
    public float _maxSpeed;
    public float _acceleration;
    public float _maxMotorTorque;
    public float _maxBrakeTorque;
    public float _maxSteerAngle;
    private float _currentSpeed;

    private EntityManager _entityManager;
    private Entity _entity;
    private Rigidbody _body;
    private Drive_Bridge _driveBridge;
    private float _motorInput;
    private float _steerInput;
    private float _brakeInput;
    private Player_Drive_Component _playerDriveComponent=>_entityManager.GetComponentData<Player_Drive_Component>(_entity);

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _driveBridge = GetComponent<Drive_Bridge>();
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype _entityArchetype = _entityManager.CreateArchetype(typeof(Player_Drive_Component));
        _entity = _entityManager.CreateEntity(_entityArchetype);
        _entityManager.AddComponentData(_entity, new Player_Drive_Component
        {
            _acceleration =  _acceleration,
            _maxSpeed = _maxSpeed,
            _maxMotorTorque = _maxMotorTorque,
            _maxBrakeTorque = _maxBrakeTorque,
            _maxSteerAngle =  _maxSteerAngle
        });
    }

    private void Update()
    {
        _playerDriveComponent._currentVelocity = _body.velocity;
        _entityManager.SetComponentData(_entity, _playerDriveComponent);

        _currentSpeed = _playerDriveComponent._currentSpeed;
        _motorInput = _playerDriveComponent._motorInput;
        _steerInput = _playerDriveComponent._steerInput;
        _brakeInput = _playerDriveComponent._brakeInput;

        SetDriveBridgeParameter();
    }

    private void SetDriveBridgeParameter()
    {
        if (_driveBridge != null)
        {
            _driveBridge.SteerInput = _steerInput;
            _driveBridge.BrakeInput = _brakeInput;

            if (_currentSpeed < _maxSpeed)
            {
                _driveBridge.MotorInput = _motorInput;
            }
            else
            {
                _driveBridge.MotorInput = 0;
            }
        }
    }
}