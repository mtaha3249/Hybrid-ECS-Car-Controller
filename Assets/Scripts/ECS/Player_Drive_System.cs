using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Player_Drive_System : SystemBase
{
    protected override void OnUpdate()
    {
        float _hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        float space = Input.GetAxis("Jump");
        Entities.ForEach((Player_Drive_Component _playerDriveComponent) =>
        {
            float currentXSpeed = math.pow(_playerDriveComponent._currentVelocity.x, 2);
            float currentYSpeed = math.pow(_playerDriveComponent._currentVelocity.y, 2);
            float currentZSpeed = math.pow(_playerDriveComponent._currentVelocity.z, 2);
            _playerDriveComponent._currentSpeed = math.sqrt(currentXSpeed + currentYSpeed + currentZSpeed);
            
            _playerDriveComponent._motorInput =
                _playerDriveComponent._acceleration * _playerDriveComponent._maxMotorTorque * vAxis *
                (1 - (_playerDriveComponent._currentSpeed / _playerDriveComponent._maxSpeed));

            _playerDriveComponent._steerInput = _playerDriveComponent._maxSteerAngle * _hAxis;

            if (space == 1)
            {
                _playerDriveComponent._brakeInput = _playerDriveComponent._maxBrakeTorque *
                                                    (_playerDriveComponent._currentSpeed /
                                                     _playerDriveComponent._maxSpeed) *
                                                    _playerDriveComponent._acceleration;
            }
            else
            {
                _playerDriveComponent._brakeInput = 0;
            }
        }).WithoutBurst().Run();
    }
}