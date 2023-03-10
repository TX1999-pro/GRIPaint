using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mouseWorldPosition;
    public float mouse_x;
    public float mouse_y;
    [SerializeField] private Transform player;
    private Vector3 offset = new ();
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Transform>();
        //offset = new Vector3(-6.65f,5f,0f);
    }

    // Update is called once per frame
    void Update()
    {


        //Vector2 cursorPos = Input.mousePosition;
        //mouse_x = cursorPos[0];
        //mouse_y = cursorPos[1];
        //mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector2(mouse_x, mouse_y));
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition; 
    }
}
