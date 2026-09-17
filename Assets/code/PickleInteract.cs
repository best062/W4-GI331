using UnityEngine;
using UnityEngine.InputSystem;

public class PickleInteract : MonoBehaviour
{
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Tap.performed += OnTapped;
    }

    private void OnDisable()
    {
        controls.Gameplay.Tap.performed -= OnTapped;
        controls.Gameplay.Disable();
    }

    private void OnTapped(InputAction.CallbackContext ctx)
    {
        // อ่านพิกัดที่นิ้วแตะหรือเมาส์คลิก
        Vector2 screenPos = controls.Gameplay.TouchPosition.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                Debug.Log("แตะโดนเป้าหมายแล้ว!");
                // ใส่ Feedback สั้นๆ เช่น เด้งขยายขนาด
                transform.localScale = Vector3.one * 1.25f;
            }
        }
    }

    private void Update()
    {
        // ย่อกลับขนาดปกติสมูทๆ
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 10f);
    }
}