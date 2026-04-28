using UnityEngine;[RequireComponent(typeof(Rigidbody))] // Bu kodun çalışması için Rigidbody'yi zorunlu kılar
public class PlayerMovement : MonoBehaviour
{
    [Header("Bağlantılar")]
    [SerializeField] private MobileJoystick joystick;
    [SerializeField] private Transform cameraTransform; // Kameraya göre hareket edeceğiz

    [Header("Hareket Ayarları")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Fizik işlemleri her zaman FixedUpdate içinde yapılır (Clean Code kuralı)
    private void FixedUpdate()
    {
        if (joystick == null) return;

        // Joystick'ten gelen X ve Y verisini, 3D dünyanın X ve Z (ileri/geri/sağ/sol) eksenine çeviriyoruz
        Vector3 inputDirection = new Vector3(joystick.InputVector.x, 0f, joystick.InputVector.y);

        // Eğer joystick'e dokunuluyorsa
        if (inputDirection.magnitude >= 0.1f)
        {
            // Kameranın baktığı yöne göre karakterin gitmesi gereken açıyı hesapla
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            
            // Karakterin o yöne doğru yumuşakça dönmesini sağla
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.fixedDeltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Karakteri ileri doğru hareket ettir
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}