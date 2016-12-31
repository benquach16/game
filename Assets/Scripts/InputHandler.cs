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
        //iterate over every unit??
        var objs = GameObject.FindGameObjectsWithTag("Unit");
        int[] ids;
        foreach (GameObject obj in objs)
        {
            if(obj.GetComponent<Renderer>().isVisible)
            {
                Debug.Log(obj.GetComponent<Unit>().controllingPlayer);
                var screenpos = m_playerCam.WorldToScreenPoint(obj.transform.position);
                Rect selectionBox = GetScreenRect(startMousePos, endMousePos);
                if (selectionBox.Contains(screenpos))
                {
                    Debug.Log("SELECTED");
                    obj.GetComponent<Unit>().selected = true;
                }
            }
        }
    }


    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
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

    }

}
