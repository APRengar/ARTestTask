using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager; // Для взаимодействия с AR-поверхностями

    private Camera arCamera;
    private GameObject selectedObject;

    private Vector2 previousTouchPosition;

    private float rotationSpeed = 100f;
    private float scaleSpeed = 0.01f;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        // Если пользователь взаимодействует с UI, игнорируем касания
        if (IsTouchingUI()) return;

        // Обработка одного касания (перемещение объекта)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SelectObject(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && selectedObject != null)
            {
                MoveObject(touch.position);
            }
        }

        // Обработка двух касаний (масштабирование и вращение объекта)
        if (Input.touchCount == 2 && selectedObject != null)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Масштабирование (изменение расстояния между пальцами)
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float previousDistance = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float distanceDelta = currentDistance - previousDistance;

                ScaleObject(distanceDelta);
            }

            // Вращение (изменение угла между пальцами)
            float previousAngle = Mathf.Atan2(touch1.position.y - touch2.position.y, touch1.position.x - touch2.position.x);
            float currentAngle = Mathf.Atan2((touch1.position.y - touch1.deltaPosition.y) - (touch2.position.y - touch2.deltaPosition.y),
                                             (touch1.position.x - touch1.deltaPosition.x) - (touch2.position.x - touch2.deltaPosition.x));
            float angleDelta = Mathf.Rad2Deg * (currentAngle - previousAngle);

            RotateObject(angleDelta);
        }

        // Сброс выделенного объекта, если касания закончились
        if (Input.touchCount == 0)
        {
            selectedObject = null;
        }
    }

    private void SelectObject(Vector2 touchPosition)
    {
        // Проверка на попадание касания в объект
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            selectedObject = hit.collider.gameObject;
        }
    }

    private void MoveObject(Vector2 touchPosition)
    {
        // Использование AR Raycast для получения позиции на плоскости
        if (raycastManager.Raycast(touchPosition, raycastHits, TrackableType.Planes))
        {
            Pose hitPose = raycastHits[0].pose; // Берём первую найденную плоскость
            selectedObject.transform.position = hitPose.position;
        }
    }

    private void ScaleObject(float scaleDelta)
    {
        // Масштабирование объекта
        float scaleFactor = 1 + scaleDelta * scaleSpeed;
        selectedObject.transform.localScale *= scaleFactor;
    }

    private void RotateObject(float angleDelta)
    {
        // Вращение объекта вокруг оси Y
        selectedObject.transform.Rotate(Vector3.up, angleDelta * rotationSpeed * Time.deltaTime, Space.World);
    }

    private bool IsTouchingUI()
    {
        // Проверка на взаимодействие с UI (чтобы жесты не срабатывали на кнопках)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }
        return false;
    }
}