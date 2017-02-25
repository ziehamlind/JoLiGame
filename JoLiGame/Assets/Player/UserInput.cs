using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS;
using UnityEngine.Audio;

public class UserInput : MonoBehaviour {

    private Player player;
    public AudioSource SoundOnClick;

    void Start () {

        player = transform.root.GetComponent<Player>();
    }
	
	void Update () {

        if (player.human){

            MoveCamera();
            MouseActivity();
        }
    }

    private void MoveCamera(){

        RotateCamera();
        Vector3 movement = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.A)) {

                movement.x -= ResourceManager.ScrollSpeed;
            }

        if (Input.GetKey(KeyCode.D)) {

                movement.x += ResourceManager.ScrollSpeed;
            }

        if (Input.GetKey(KeyCode.W)) {

                movement.z += ResourceManager.ScrollSpeed;
            }

        if (Input.GetKey(KeyCode.S)) {

                movement.z -= ResourceManager.ScrollSpeed;
            }



        //make sure movement is in the direction the camera is pointing
        //but ignore the vertical tilt of the camera to get sensible scrolling
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0;

        //away from ground movement
        movement.y -= 10 * ResourceManager.ScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 origin = Camera.main.transform.position;
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.y > ResourceManager.MaxCameraHeight){

                destination.y = ResourceManager.MaxCameraHeight;

            } else if (destination.y < ResourceManager.MinCameraHeight){

                destination.y = ResourceManager.MinCameraHeight;
            }

        //if a change in position is detected perform the necessary update
        if (destination != origin) {

                Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
            }

    }

    private void RotateCamera() {

        float xpos = Input.mousePosition.x;
        float ypos = Input.mousePosition.y;
        Vector3 origin = Camera.main.transform.eulerAngles;
        Vector3 destination = origin;


        if (xpos >= 0 && xpos < ResourceManager.ScrollWidth) {

                destination.y -= ResourceManager.RotateAmount;

             } else if (xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) {

                destination.y += ResourceManager.RotateAmount;

            }
        // vertical camera movement
        
        if (ypos >= 0 && ypos < ResourceManager.ScrollWidth) {

                destination.x += ResourceManager.RotateAmount;

            } else if (ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) {

                destination.x -= ResourceManager.RotateAmount;

            }


        //if a change in position is detected perform the necessary update
        if (destination.x >= ResourceManager.MaxCameraRotationDown) {

            destination.x = ResourceManager.MaxCameraRotationDown;
        }
        if (destination.x <= 0) {

            destination.x = 0;
        }

        if (destination != origin)
        {
            Camera.main.transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.RotateSpeed);
        }
    }

    private void MouseActivity(){
        if (Input.GetMouseButtonDown(0)) LeftMouseClick();
        else if (Input.GetMouseButtonDown(1)) RightMouseClick();
    }

    //just handle mouse clicks inside gaming area, mouse inside HUD? -> let HUD handle it, determine if player clicked on worldobject or not
    private void LeftMouseClick(){
        if (player.hud.MouseInBounds())
        {
            GameObject hitObject = FindHitObject();
            Vector3 hitPoint = FindHitPoint();
            if (hitObject && hitPoint != ResourceManager.InvalidPosition)
            {
                if (player.SelectedObject) {
                    player.SelectedObject.MouseClick(hitObject, hitPoint, player);
            }
            else if (hitObject.name != "Ground")
            {
                WorldObject worldObject = hitObject.transform.root.GetComponent<WorldObject>();
                    if (worldObject)
                {
                        //we already know the player has no selected object
                        
                        player.SelectedObject = worldObject;
                    worldObject.SetSelection(true, player.hud.GetPlayingArea());
                }
            }
            }
        }
    }
    //finding which object is clicked on 
    private GameObject FindHitObject(){

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {

            if (hit.collider.gameObject.name != "Ground") {
                SoundOnClick.Play();
            }
            return hit.collider.gameObject;

        } else {
            return null;
        }
    }

    private Vector3 FindHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) return hit.point;
        return ResourceManager.InvalidPosition;
    }

    private void RightMouseClick()
    {
        if (player.hud.MouseInBounds() && !Input.GetKey(KeyCode.LeftAlt) && player.SelectedObject)
        {
            player.SelectedObject.SetSelection(false, player.hud.GetPlayingArea());
            player.SelectedObject = null;
        }
    }
}
