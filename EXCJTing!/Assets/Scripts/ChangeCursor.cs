using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Texture2D cursorSelect;
	public Texture2D cursorDefault;
	public CursorMode cursorMode = CursorMode.ForceSoftware;
	public Vector2 hotSpot = Vector2.zero;

	public Button button;
	public Image panel;
	private void Start()
	{
		//for (int i = 0; i < buttons.length; i++)
		//{
		//	button button = buttons[i];
		//	var index = i;
		//	button.onclick.removealllisteners();
		//	button.onclick.addlistener(() => onclick(index));
		//	button.onpointerenter();
		//}
	}

	private void OnClick(int index)
	{
		Debug.Log("You click button at index: " + index);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Cursor.SetCursor(cursorSelect, hotSpot, cursorMode);
	}

	public void OnPointerExit(PointerEventData eventData)
    {
		Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
	}
}
