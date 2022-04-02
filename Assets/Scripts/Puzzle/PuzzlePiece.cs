using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePiece : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isGrabbed = false;
    private bool isSet = false;
    private PolygonCollider2D polygonCollider;

    private void Start()
    {
        TryGetComponent(out polygonCollider);
        initialPosition = transform.position; 
    }

    private void Update()
    {
        if (!isGrabbed)
            return;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())) + new Vector3(0, 0, 20);
    }

    public void ResetPosition()
    {
        if (isSet)
            return;

        transform.position = initialPosition;
    }

    public void SetSet()
    {
        isSet = true;
    }

    public void SetGrabbed(bool _state)
    {
        isGrabbed = _state;

        polygonCollider.enabled = !_state;

        if(!isGrabbed)
            ResetPosition();
    }
}
