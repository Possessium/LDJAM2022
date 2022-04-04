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
    private Vector2 mouseOffset = Vector2.zero;

    private void Start()
    {
        TryGetComponent(out polygonCollider);
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isGrabbed)
            return;

        Vector3 _nextPosition = Camera.main.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())) + new Vector3(mouseOffset.x, mouseOffset.y, 20);

        if (_nextPosition.x < (Player.Instance.PlayerLimits.min.x / 2) + (Player.Instance.PlayerLimits.center.x / 2) || _nextPosition.x > (Player.Instance.PlayerLimits.max.x / 2) + (Player.Instance.PlayerLimits.center.x / 2))
            _nextPosition.x = transform.position.x;

        if (_nextPosition.y < (Player.Instance.PlayerLimits.min.y / 2) + (Player.Instance.PlayerLimits.center.y / 2) || _nextPosition.y > (Player.Instance.PlayerLimits.max.y / 2) + (Player.Instance.PlayerLimits.center.y / 2))
            _nextPosition.y = transform.position.y;

        transform.position = _nextPosition;
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

        if (!isGrabbed)
        {
            GameManager.Instance.ChangeCursor(false);
            ResetPosition();
        }

        else
        {
            mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())) ;
            GameManager.Instance.ChangeCursor(true);
            switch (PuzzleObject.Instance.CurrentMaterial)
            {
                case PieceMaterial.ceramic:
                    AudioController.Instance.Play("ceramicPrise");
                    break;
                case PieceMaterial.metal:
                    AudioController.Instance.Play("metalPrise");
                    break;
                default:
                    break;
            }
        }
    }
}
