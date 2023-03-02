using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PenSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    public RectTransform m_RectTransform;
    public Vector2 initPos;
    public Vector3 displacement = new Vector3(0, -60, 0);
    public Vector2 targetPos;
    // Start is called before the first frame update   
    private bool isSelected = false;
    void Start()
    {
        //Fetch the RectTransform from the GameObject
        m_RectTransform = GetComponent<RectTransform>();
        initPos = m_RectTransform.position;
        //Set target position
        targetPos = m_RectTransform.position + displacement;
    }
    private void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Entered the" + this.transform.name);
        m_RectTransform.position = targetPos;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("left the" + this.transform.name);
        if (!isSelected)
        {
            m_RectTransform.position = initPos;
        }

    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        m_RectTransform.position = targetPos;
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was Deselected");
        m_RectTransform.position = initPos;
        isSelected = false;

    }
}
