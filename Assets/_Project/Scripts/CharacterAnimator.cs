using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [Header("Bağlantılar")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [Tooltip("Joystick merkezde mi diye kontrol etmek için ekledik")]
    [SerializeField] private MobileJoystick joystick; 

    [Header("Yön Çizimleri (Sprites)")]
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private Sprite leftSprite;

    private void Start()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void Update()
    {
        if (playerTransform == null || cameraTransform == null || spriteRenderer == null || joystick == null) return;

        // EĞER JOYSTICK ORTADAYSA (Oyuncu dokunmuyorsa)
        if (joystick.InputVector.magnitude < 0.1f)
        {
            // Köpeğin varsayılan "Boşta" (Idle) duruşu olarak arkasını göster ve işlemi burada kes.
            if (backSprite != null) spriteRenderer.sprite = backSprite;
            return; 
        }

        // Eğer Joystick hareket ediyorsa normal yön hesaplamasına devam et
        Vector3 moveDir = playerTransform.forward;
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