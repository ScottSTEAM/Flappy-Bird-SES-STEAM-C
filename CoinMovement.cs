using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoinMovement : MonoBehaviour
{
    public float PillerMovementSpeed;
    public BirdMovement Bird;
    [SerializeField] float XDistanceInPillers;
    [SerializeField] float YDistanceInPillers;
    public static event EventHandler OnPillerReachInMid;
    public static event EventHandler OnPillerPassed;
    bool isPillerOnMyBack;
    private Vector3 _initialPosition;
    private bool isPillerStop;

    private void OnEnable()
    {
        isPillerStop = false;
        isPillerOnMyBack = false;
       
    }
    void Start()
    {
        Bird = GameObject.FindAnyObjectByType<BirdMovement>();
        _initialPosition = transform.position;
        BirdMovement.OnGameOver += StopPillerOnGameOver;
        transform.position = new Vector2(transform.position.x + UnityEngine.Random.Range(0f, XDistanceInPillers), transform.position.y - UnityEngine.Random.Range(YDistanceInPillers, YDistanceInPillers + 2));
  


}

    private void OnDisable()
    {
        transform.position = _initialPosition;
        BirdMovement.OnGameOver -= StopPillerOnGameOver;
    }

    private void StopPillerOnGameOver(object sender, EventArgs e)
    {
        isPillerStop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPillerStop & !Bird.isGameOver & Bird.canPlay)
        {
            transform.position += Vector3.left * PillerMovementSpeed * Time.deltaTime;
            if (transform.position.x <= 0 & !isPillerOnMyBack)
            {
                OnPillerReachInMid?.Invoke(this, EventArgs.Empty);
                isPillerOnMyBack = true;
            }

            if (transform.position.x <= -8.5)
            {
                this.gameObject.SetActive(false);
                transform.position = _initialPosition;
                isPillerOnMyBack = false;

            }
        }
    }

    public void SetPillerMovementSpeed(float pillerSpeed)
    {
        PillerMovementSpeed = pillerSpeed;
    }
    public float GetPillerMovementSpeed()
    {
        return PillerMovementSpeed;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnPillerPassed?.Invoke(this, EventArgs.Empty);
    }

}
