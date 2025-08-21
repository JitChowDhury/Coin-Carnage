using Unity.Netcode;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform turrentTransform;

    private void LateUpdate()
    {
        if (!IsOwner) return;
        Vector2 aimScrenPosition = inputReader.AimPosition;
        Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScrenPosition);
        turrentTransform.up = new Vector2(aimWorldPosition.x - turrentTransform.position.x, aimWorldPosition.y - turrentTransform.position.y);
    }
}
