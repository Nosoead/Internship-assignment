using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour
{
    public UnityAction<float> OnDirectionEvent;
    private MonsterStatHandler statHandler;
    private PlayerDetector playerDetector;
    private float moveSpeed;

    private void Awake()
    {
        playerDetector = GetComponent<PlayerDetector>();
        statHandler = GetComponent<MonsterStatHandler>();
    }

    private void OnEnable()
    {
        statHandler.OnSubscribeToStatUpdateEvent += OnStatUpdateEvent;
        statHandler.OnDieEvent += OnDie;
    }

    private void OnDisable()
    {
        statHandler.OnSubscribeToStatUpdateEvent += OnStatUpdateEvent;
        statHandler.OnDieEvent -= OnDie;
    }

    private void FixedUpdate()
    {
        playerDetector.ApplyDetector(moveSpeed);
        OnDirectionEvent?.Invoke(playerDetector.GetDirection());
    }

    private void OnDie()
    {
        moveSpeed = 0;
    }

    private void OnStatUpdateEvent(string key, float value)
    {
        switch (key)
        {
            case "MoveSpeed":
                moveSpeed = value;
                break;
        }
    }
}
