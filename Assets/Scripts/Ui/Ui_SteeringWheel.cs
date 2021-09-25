using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Ui_SteeringWheel : MonoBehaviour
{
    public Graphic UI_Element;
    public Player player;

    RectTransform rectT;
    Vector2 centerPoint;
    public float maximumSteeringAngle = 200f;
    public float wheelReleasedSpeed = 200f;
    private float wheelAngle = 0f;
    private float wheelPrevAngle = 0f;
    private bool wheelBeingHeld = false;

    public float GetClampedValue() => wheelAngle / maximumSteeringAngle;
    public float GetAngle() => wheelAngle;
    void Start()
    {
        rectT = UI_Element.rectTransform;
        InitEventsSystem();
    }
    void Update()
    {
        if (!wheelBeingHeld && !Mathf.Approximately(0f, wheelAngle))
        {
            float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= deltaAngle;
            else
                wheelAngle += deltaAngle;
        }
        rectT.localEulerAngles = Vector3.back * wheelAngle;
        
        if (Input.GetKey(KeyCode.A))
        {
            rectT.localEulerAngles = Vector3.back * -90;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rectT.localEulerAngles = Vector3.back * 90;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            rectT.localEulerAngles = Vector3.back * 0;
        }
        Debug.Log("Angle: " + rectT.localEulerAngles.z);

        if (rectT.localEulerAngles.z > 20 && rectT.localEulerAngles.z < 180)
            player.direction = Player.Direction.Left;
        else if (rectT.localEulerAngles.z > 270) 
            player.direction = Player.Direction.Right;
        else
            player.direction = Player.Direction.None;
    }
    void InitEventsSystem()
    {
        EventTrigger events = UI_Element.gameObject.GetComponent<EventTrigger>();

        if (events == null)
            events = UI_Element.gameObject.AddComponent<EventTrigger>();

        if (events.triggers == null)
            events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData>(PressEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(DragEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.Drag;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(ReleaseEvent);//
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = callback;

        events.triggers.Add(entry);
    }
    public void PressEvent(BaseEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        wheelBeingHeld = true;
        centerPoint = RectTransformUtility.WorldToScreenPoint(((PointerEventData)eventData).pressEventCamera, rectT.position);
        wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
    }
    public void DragEvent(BaseEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        if (Vector2.Distance(pointerPos, centerPoint) > 20f)
        {
            if (pointerPos.x > centerPoint.x)
                wheelAngle += wheelNewAngle - wheelPrevAngle;
            else
                wheelAngle -= wheelNewAngle - wheelPrevAngle;
        }
        wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
        wheelPrevAngle = wheelNewAngle;
    }
    public void ReleaseEvent(BaseEventData eventData)
    {
        DragEvent(eventData);
        wheelBeingHeld = false;
    }
}