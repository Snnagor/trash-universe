using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speedCrusher = 740;
    public float SpeedCrusher { get => speedCrusher; set => speedCrusher = value; }

    [SerializeField] private float speedRotation;
    [SerializeField] private Rigidbody rb;

    public float SpeedMove { get; set; }
    public float CurrentSpeed { get; set; }    

    public float DeltaChangeSpeed { get; set; }

    #region Injects

    private FloatingJoystick _floatingJoystick;
    private SettingsShop _settingsShop;

    [Inject]
    private void Construct(FloatingJoystick floatingJoystick,
                           SettingsShop settingsShop)
    {
        _floatingJoystick = floatingJoystick;
        _settingsShop = settingsShop;
    }

    #endregion

    public void Init()
    {
        SpeedMove = SpeedCrusher;
        CurrentSpeed = SpeedMove;        
    }

    private void Move()
    {
        Vector3 direction = Vector3.zero;

        rb.angularVelocity = Vector3.zero;

        if (_floatingJoystick.Controll) 
        {
            direction = new Vector3(_floatingJoystick.Direction.x, 0, _floatingJoystick.Direction.y);

            Rotation(direction);
        }

        var speedNow = CurrentSpeed + DeltaChangeSpeed;
       
        rb.velocity = direction * speedNow * Time.fixedDeltaTime;
    }

    private void Rotation(Vector3 direction)
    {        
        if (_floatingJoystick.Horizontal != 0)
        {           
            rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.fixedDeltaTime * speedRotation);
        }
    }

    public void SetResist(float resistValue)
    {
        float tmpSpeed = SpeedCrusher / resistValue;

        if (CurrentSpeed > tmpSpeed)
            CurrentSpeed = tmpSpeed;
    }

    public void SetNormalSpeed() 
    {
        if (CurrentSpeed < SpeedMove)
        {
            CurrentSpeed = SpeedMove;
        }            
    }

    public void SetDeltaSpeed()
    {
        DeltaChangeSpeed = CurrentSpeed * _settingsShop.JunkBotPercentDeltaSpeed / 100;
    }

    public void UpgradeSpeed(float deltaChangeSpeed)
    {
        SpeedMove += deltaChangeSpeed;
        CurrentSpeed = SpeedMove;

        if(DeltaChangeSpeed > 0)
        {
            SetDeltaSpeed();
        }
    }

    public void FixedUpdate() 
    {        
        Move();
    }
}
