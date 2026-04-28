using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Sahnedeki ana kamerayı otomatik olarak bul ve hafızaya al.
        // Bunu Start'ta yapıyoruz ki oyun boyunca tekrar tekrar arayıp telefonu yormasın (Optimizasyon).
        mainCamera = Camera.main; 
    }

    // Hareket ve kamera takibinden SONRA dönme işlemi olmalı, bu yüzden LateUpdate kullanıyoruz.
    private void LateUpdate()
    {
        if (mainCamera == null) return;

        // Sprite'ın her zaman kameranın baktığı yöne doğru (kameraya tam paralel) bakmasını sağlar.
        // Bu sayede kamera yukarıdan veya aşağıdan baksa bile 2D çizim asla bükülmez, illüzyon bozulmaz.
        transform.rotation = mainCamera.transform.rotation;
    }
}