using UnityEngine;

public class DrawerOpen : InteractableBase
{
    [SerializeField] Transform _posToTeleportPlayer;

    public override void Interact()
    {
        Player.Instance.transform.position = _posToTeleportPlayer.position;
    }
}
