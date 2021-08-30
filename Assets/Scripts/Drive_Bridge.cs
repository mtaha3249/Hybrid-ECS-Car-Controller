using System;
using UnityEngine;

[Serializable]
public class WheelInformation
{
    public bool canDrive, canSteer;
    public WheelCollider _wheelCollider;
    public Transform _wheelModel;
}

public class Drive_Bridge : MonoBehaviour
{
    public WheelInformation _frontRightWheel, _frontLeftWheel, _backRightWheel, _backLeftWheel;

    private float _motorInput, _steerInput, _brakeInput;

    public float MotorInput
    {
        get => _motorInput;
        set => _motorInput = value;
    }

    public float SteerInput
    {
        get => _steerInput;
        set => _steerInput = value;
    }

    public float BrakeInput
    {
        get => _brakeInput;
        set => _brakeInput = value;
    }

    public Transform COM;
    private Rigidbody _body;

    private void Start()
    {
        _motorInput = _brakeInput = _steerInput = 0;

        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _body.centerOfMass = COM.localPosition;
        ApplyAccelerate();
        ApplySteer();
        ApplyBrake();
        UpdateWheelPositions();
    }

    void ApplyAccelerate()
    {
        if (_frontRightWheel.canDrive)
            _frontRightWheel._wheelCollider.motorTorque = _motorInput;
        if (_frontLeftWheel.canDrive)
            _frontLeftWheel._wheelCollider.motorTorque = _motorInput;
        if (_backRightWheel.canDrive)
            _backRightWheel._wheelCollider.motorTorque = _motorInput;
        if (_backLeftWheel.canDrive)
            _backLeftWheel._wheelCollider.motorTorque = _motorInput;
    }

    void ApplySteer()
    {
        if (_frontRightWheel.canSteer)
            _frontRightWheel._wheelCollider.steerAngle = _steerInput;
        if (_frontLeftWheel.canSteer)
            _frontLeftWheel._wheelCollider.steerAngle = _steerInput;
        if (_backRightWheel.canSteer)
            _backRightWheel._wheelCollider.steerAngle = _steerInput;
        if (_backLeftWheel.canSteer)
            _backLeftWheel._wheelCollider.steerAngle = _steerInput;
    }

    void ApplyBrake()
    {
        _frontRightWheel._wheelCollider.brakeTorque = _brakeInput;
        _frontLeftWheel._wheelCollider.brakeTorque = _brakeInput;
        _backRightWheel._wheelCollider.brakeTorque = _brakeInput;
        _backLeftWheel._wheelCollider.brakeTorque = _brakeInput;
    }

    void UpdateWheelPositions()
    {
        UpdateWheelPosition(_frontLeftWheel);
        UpdateWheelPosition(_frontRightWheel);
        UpdateWheelPosition(_backLeftWheel);
        UpdateWheelPosition(_backRightWheel);
    }

    void UpdateWheelPosition(WheelInformation _wheelInformation)
    {
        Vector3 _position = _wheelInformation._wheelModel.position;
        Quaternion _rotation = _wheelInformation._wheelModel.rotation;
        _wheelInformation._wheelCollider.GetWorldPose(out _position, out _rotation);
        _wheelInformation._wheelModel.position = _position;
        _wheelInformation._wheelModel.rotation = _rotation;
    }
}
