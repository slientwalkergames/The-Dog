using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Bağlantılar")]
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private Transform cameraTransform;

    [Header("Hareket Ayarları")][SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;

    // Karakterin gittiği yönü Animatörün (CharacterAnimator) okuyabilmesi için hafızada tutuyoruz
    public Vector3 CurrentMoveDirection { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (joystick == null) return;

        Vector3 inputDirection = new Vector3(joystick.InputVector.x, 0f, joystick.InputVector.y);

        if (inputDirection.magnitude >= 0.1f)
        {
            // Kameraya göre gidilecek yönü hesapla
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            // Yönü animatör için kaydet
            CurrentMoveDirection = moveDirection;

            // Karakteri SADECE YÜRÜT (Asla döndürme!)
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Joystick bırakıldığında yönü sıfırla
            CurrentMoveDirection = Vector3.zero;
        }
    }
}