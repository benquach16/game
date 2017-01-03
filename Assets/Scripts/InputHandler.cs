using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class handles rts input events
public class InputHandler : MonoBehaviour {
    private Vector3 m_initialClickPosition;
    private bool m_isSelecting = false;
    private Texture2D m_selectionTexture;
    // Use this for initialization
    Player m_player;
    Camera m_playerCam;
    void Start () {
        m_player = GetComponent<Player>();
        m_playerCam = GetComponent<Camera>();
		m_selectionTexture = new Texture2D(1, 1);
        m_selectionTexture.SetPixel(1, 1, new Color(0,128,0,0.2f)); //Sets the 1 pixel to be white
        m_selectionTexture.Apply(); //Applies all the changes made
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void handleInput()
    {
        leftMouseClick();
    }

    //mostly selection code
    void leftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_isSelecting = true;
            m_initialClickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isSelecting = false;
            //handle selection
            //call a callback on the player
            handleSelection();
        }
        //right click
        if(Input.GetMouseButtonDown(1))
        {
            
        }
    }

    void handleSelection()
    {
        Vector3 startMousePos = m_initialClickPosition;
        Vector3 endMousePos = Input.mousePosition;

        List<int> ids = new List<int>();
        /*
        Ray ray1 = m_playerCam.ScreenPointToRay(startMousePos);
        Ray ray2 = m_playerCam.ScreenPointToRay(endMousePos);

        //generate vertices
        RaycastHit out1, out2;

        Physics.Raycast(ray1.origin, ray1.direction, out out1, 1000);
        Physics.Raycast(ray2.origin, ray2.direction, out out2, 1000);

        var vertex1 = m_playerCam.transform.position;
        var vertex2 = out1.point;
        var vertex3 = out2.point;
        var vertex4 = new Vector3(out2.point.x, out1.point.y, out1.point.z);
        var vertex5 = new Vector3(out1.point.x, out2.point.y, out2.point.z);
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";
        //translate these poor motherfuckers by the inverse of the camera 
        m.vertices = new Vector3[]
        {
            m_playerCam.transform.position,
            out1.point,
            out2.point,
            new Vector3(out2.point.x, out1.point.y, out1.point.z),
            new Vector3(out1.point.x, out2.point.y, out2.point.z)
        };
        m.triangles = new int[] { 0, 1, 3,
            0, 4, 1,
            0, 3, 2,
            0, 2, 4,
            3, 1, 2,
            1, 4, 2};
        m.RecalculateNormals();
        GameObject newObj = new GameObject();
        MeshCollider collider = (MeshCollider)newObj.AddComponent(typeof(MeshCollider));
        collider.sharedMesh = m;
        */

        //iterate over every unit??
        var objs = GameObject.FindGameObjectsWithTag("Unit");
        m_player.clearSelectedObjs();
        foreach (GameObject obj in objs)
        {
            //make sure that we can select all of these
            if(obj.GetComponent<Renderer>().isVisible)
            {
                var screenpos = m_playerCam.WorldToScreenPoint(obj.transform.position);

                Rect selectionBox = GetScreenRect(startMousePos, endMousePos);
                Vector2 screenpoint = new Vector2(screenpos.x, Screen.height - screenpos.y);
                Debug.Log(selectionBox);
                Debug.Log(screenpoint);
                if (selectionBox.Contains(screenpoint, true))
                {
                    obj.GetComponent<Unit>().selected = true;
                    //MUST CHANGE THIS WITH NETWORK IDS
                    ids.Add(obj.GetComponent<Unit>().currentId);
                }
            }

            
        }
        m_player.setSelectedObjs(ids);
    }


    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        var bottomLeft = new Vector3(topLeft.x, bottomRight.y);
        var size = new Vector2(bottomRight.x - topLeft.x, bottomRight.y - topLeft.y);
        // Create Rect
        return new Rect(topLeft, size);
    }

    public void OnGUI()
    {
        if (m_isSelecting)
        {

            // Create a rect from both mouse positions
            var rect = GetScreenRect(m_initialClickPosition, Input.mousePosition);
            GUI.DrawTexture(rect, m_selectionTexture);
            //GUIElement.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
        var objs = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject obj in objs)
        {
            var pos = m_playerCam.WorldToScreenPoint(obj.transform.position);
            GUI.DrawTexture(GetScreenRect(pos, new Vector3(pos.x + 2, pos.y+2, 0)), m_selectionTexture);
        }
    }

}
