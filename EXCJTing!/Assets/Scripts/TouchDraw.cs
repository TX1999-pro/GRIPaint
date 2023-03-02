using UnityEngine;
using System.Collections;

public class TouchDraw : MonoBehaviour
{
    public GameObject linePrefab;

    public Transform drawnPicture;


    #region private
    private Coroutine drawing;
    private Coroutine erasing;
    [SerializeField] private bool isDrawing;
    [SerializeField] private bool isErasing;
    private int sortOrder=0;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartDraw();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndDraw();
            }
        } else if (isErasing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartErase();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EndErase();
            }
        }

    }
    #region drawing
    void StartDraw()
    {
        if (drawing != null) //why?
        {
            StopCoroutine(drawing);
        }

        drawing = StartCoroutine(Drawing());

    }
    IEnumerator Drawing()
    {

        isDrawing = true;

        GameObject lineObject = Instantiate(linePrefab, new Vector3(0, 0, 0), Quaternion.identity, drawnPicture);
        //lineObject.transform.SetAsFirstSibling(); // new line object appear on top
        LineRenderer line = lineObject.GetComponent<LineRenderer>();
        line.sortingLayerName = "Top";
        line.sortingOrder = sortOrder;
        sortOrder += 1; // later lines will be rendered on top of older ones
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.positionCount = 0;

        while (isDrawing)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, position);
            yield return null;
        }
    }

    void EndDraw()
    {
        StopCoroutine(drawing);
        //isDrawing = false;
    }
    #endregion

    void StartErase()
    {
        if (erasing != null)
        {
            StopCoroutine(erasing);
        }
        erasing = StartCoroutine(Erasing());

    }
    void EndErase()
    {
        StopCoroutine(drawing);
        isErasing = false;
    }

 

    IEnumerator Erasing()
    {
        isErasing = true;

        while (isErasing)
        {
            yield return null;
        }
    }
}