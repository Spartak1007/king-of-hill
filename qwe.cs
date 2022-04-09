using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class qwe : MonoBehaviour
{
    Transform playerBody;
    CharacterController contr;
    public float speed = 12f;
    float graValue = -9.81f;
    bool isGrounded = false;
    int time = 10;
    [SerializeField] TextMeshProUGUI seconds;
    public GameObject loose;
    public GameObject win;
    public GameObject cap;
    void timeMinus(){
        time = time -1;
        seconds.text = time + ""; 
        if(time == 0){
            CancelInvoke();
            loose.SetActive(true);
        }
    }

    void Start()
    {
        InvokeRepeating("timeMinus",1f,1f);
        playerBody = GetComponent<Transform>();
        contr = GetComponent<CharacterController>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Vertical");
        contr.Move(playerBody.up * graValue * Time.deltaTime);
        contr.Move(playerBody.forward * vertical * speed * Time.deltaTime);

        playerBody.Rotate(0, mouseX, 0);

        if(Input.GetKeyDown("space")&& isGrounded == true){
            contr.Move(playerBody.up * 7f);
        } else {
            isGrounded = true;
        }
    }
    void OnControllerColliderHit(ControllerColliderHit col){
        if(col.gameObject.tag == "ground"){
            isGrounded = true;
        }
        if(col.gameObject.tag == "flag"){
            CancelInvoke();
            win.SetActive(true);
            cap.SetActive(true);
        }
    }
}