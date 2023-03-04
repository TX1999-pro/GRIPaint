using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform m_RectTransform;
    public Vector2 initPos;
    public Vector3 displacement = new(-150, 160, 0);
    public Vector2 targetPos;

    public Vector3 rotationAngle = new(0, 0, 8);

    public bool onUIelement = true;


    void Start()
    {
        //Fetch the RectTransform from the GameObject
        m_RectTransform = GetComponent<RectTransform>();
        initPos = m_RectTransform.position;
        //Set target position and rotation
        targetPos = m_RectTransform.position + displacement;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered the Paint Palettee");
        m_RectTransform.position = targetPos;
        m_RectTransform.Rotate(rotationAngle);
        onUIelement = true;
        FindObjectOfType<LineManager>().EndDraw();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left Paint Palettee");
        m_RectTransform.position = initPos;
        m_RectTransform.Rotate(-rotationAngle);
        onUIelement = false;

    }
}