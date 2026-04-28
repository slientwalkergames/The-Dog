using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [Header("Bağlantılar")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerMovement playerMovement; // Artık joystick'e değil, direkt hareket koduna bakıyoruz
    [SerializeField] private Transform cameraTransform;[Header("Yön Çizimleri (Sprites)")]
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;[SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;

    private void Start()
    {
        if (spriteRenderer != null) spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        if (playerMovement == null || cameraTransform == null || spriteRenderer == null) return;

        Vector3 moveDir = playerMovement.CurrentMoveDirection;

        // EĞER HAREKET YOKSA (Joystick ortadaysa)
        if (moveDir.magnitude < 0.1f)
        {
            if (backSprite != null) spriteRenderer.sprite = backSprite;
            return; 
        }

        Vector3 camDir = cameraTransform.forward;
        moveDir.y = 0; 
        camDir.y = 0;

        float angle = Vector3.SignedAngle(camDir, moveDir, Vector3.up);

        if (Mathf.Abs(angle) < 45f) 
        {
            if (backSprite != null) spriteRenderer.sprite = backSprite;
        }
        else if (Mathf.Abs(angle) > 135f) 
        {
            if (frontSprite != null) spriteRenderer.sprite = frontSprite;
        }
        else if (angle >= 45f && angle <= 135f) 
        {
            if (rightSprite != null) spriteRenderer.sprite = rightSprite;
        }
        else if (angle <= -45f && angle >= -135f) 
        {
            if (leftSprite != null) spriteRenderer.sprite = leftSprite;
        }
    }
}