using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Kameranın Takip Edeceği Hedef")]
    [SerializeField] private Transform target;

    [Header("Camera Position Settings")]
    [Tooltip("Kameranın Karaktere Olan Uzaklığı Ve Yüksekliği")]
    [SerializeField] private Vector3 offset = new Vector3(0f , 1.5f , -3f);
    [Tooltip("Kameranın Takip Etme Yumuşaklığı (Düşük Değer = Daha Yumuşak)")]
    [SerializeField] private float smoothSpeed = 0.125f;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if(target == null)
        {
            Debug.LogWarning("CameraController: Target is not assigned.");
            return;
        }
        // Kameranın Gitmesi Gereken Hedef Pozisyonunu Hesapla
        Vector3 desiredPosition = target.position + offset;
        // SmootDamp Kullanarak Kameranın Mevcut Pozisyonundan Hedef Pozisyona Yumuşak Bir Şekilde Hareket Etmesini Sağla
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position , desiredPosition , ref velocity , smoothSpeed);
        // Kameranın Yeni Pozisyonunu Ayarla
        transform.position = smoothedPosition;
        // Kameranın Hedefe Doğru Bakmasını Sağla
        transform.LookAt(target.position + Vector3.up * 1f); // Hedefin Başına Bakması İçin Yükseklik Ekleyebiliriz
    }
}
