using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomInputModule : StandaloneInputModule
{
    public bool simulateMouseClick = false;
    public bool simulateMouseDown = false;

    public PlayerController playerController;

    protected override void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public override void Process()
    {
        if (simulateMouseClick)
        {
            ProcessSimulatedMouseClick();
        }
        else
        {
            base.Process();
        }
    }

    private void ProcessSimulatedMouseClick()
    {
        PointerEventData eventData = new PointerEventData(eventSystem)
        {
            //position = Input.mousePosition,
            position = playerController.player.position,
            button = PointerEventData.InputButton.Left
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResults);

        GameObject target = null;
        foreach (RaycastResult result in raycastResults)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                target = result.gameObject;
                //Debug.Log(result.gameObject.name);
                break;
            }
        }

        if (target != null)
        {
            ExecuteEvents.Execute(target, eventData, ExecuteEvents.pointerClickHandler);
        }

        simulateMouseClick = false;
    }
}