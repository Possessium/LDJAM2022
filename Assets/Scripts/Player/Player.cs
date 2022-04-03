using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public PuzzlePiece GrabbedPiece { get; private set; }
    public PuzzlePiece deb;
    public Bounds PlayerLimits;

    [SerializeField] private LayerMask pieceLayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        deb = GrabbedPiece;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(PlayerLimits.center, PlayerLimits.extents);
    }

    public void GrabPiece(InputAction.CallbackContext _ctx)
    {
        if (!_ctx.started)
            return;

        RaycastHit2D _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector2(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue())), Vector3.forward, Mathf.Infinity, pieceLayer);

        if (GrabbedPiece)
        {
            if (_hit && _hit.transform.GetComponent<PuzzlePiecePosition>())
            {
                _hit.transform.GetComponent<PuzzlePiecePosition>().PlacePiece(GrabbedPiece);
            }

            if(GrabbedPiece)
                GrabbedPiece.SetGrabbed(false);

            GrabbedPiece = null;

            return;
        }

        else if (_hit && _hit.transform.GetComponent<PuzzlePiece>())
        {
            GrabbedPiece = _hit.transform.GetComponent<PuzzlePiece>();
            GrabbedPiece.SetGrabbed(true);
        }
    }
}
