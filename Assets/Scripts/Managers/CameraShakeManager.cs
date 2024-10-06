using FirstGearGames.SmoothCameraShaker;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShakeManager : NonPersistentSingleton<CameraShakeManager>
{
    [SerializeField] private ShakeData _wallHitCameraShake;
    [SerializeField] private ShakeData _fireballCameraShake;

    private void Awake()
    {
        Initialize();

        // Provera da li postoji Camera Shaker
        if (CameraShakerHandler.InstantiatedShakers.Count == 0)
        {
            Camera.main.AddComponent<CameraShaker>();
        }
    }

    public void DoWallHitCameraShake()
    {
        CameraShakerHandler.Shake(_wallHitCameraShake);
    }
    public void DoFireballCameraShake()
    {
        CameraShakerHandler.Shake(_fireballCameraShake);
    }
}
