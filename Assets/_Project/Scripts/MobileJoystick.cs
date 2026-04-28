using UnityEngine;
using UnityEngine.EventSystems; // UI etkileşimleri için gerekli kütüphane

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBG;
    private RectTransform joystickHandle;
    
    // Karakter kodumuzun okuyacağı yön verisi
    public Vector2 InputVector { get; private set; } 

    private void Start()
    {
        joystickBG = GetComponent<RectTransform>();
        // İlk çocuk objeyi (Handle) bul ve al
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Ekrana dokunulduğunda sürüklemeyi başlat
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        // Parmağın dokunduğu yeri Joystick arka planının içine göre hesapla
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG, eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / joystickBG.sizeDelta.x) * 2;
            position.y = (position.y / joystickBG.sizeDelta.y) * 2;

            InputVector = new Vector2(position.x, position.y);
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

            // İçerideki yuvarlağı (Handle) parmağımızın olduğu yere taşı
            joystickHandle.anchoredPosition = new Vector2(InputVector.x * (joystickBG.sizeDelta.x / 2), InputVector.y * (joystickBG.sizeDelta.y / 2));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Parmağımızı çektiğimizde Joystick'i sıfırla ve merkeze al
        InputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}