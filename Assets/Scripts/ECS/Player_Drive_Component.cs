using Unity.Entities;
using Unity.Mathematics;

public class Player_Drive_Component : IComponentData
{
    public float _maxMotorTorque;
    public float _maxBrakeTorque;
    public float _maxSteerAngle;
    public float _maxSpeed;
    public float _acceleration;

    public float3 _currentVelocity;
    public float _currentSpeed;

    public float _motorInput;
    public float _steerInput;
    public float _brakeInput;
}
