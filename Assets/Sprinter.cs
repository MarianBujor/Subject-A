using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprinter : MonoBehaviour
{
    // Start is called before the first frame update
    float stamina = 5, maxStamina = 5;
    public float walkSpeed, runSpeed;
    public PlayerMovement pm;
    public bool isRunning;
    Rect staminaRect;
    Texture2D staminaTexture;
    void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        walkSpeed = pm.speed;
        runSpeed = walkSpeed * 2f;

        staminaRect = new Rect(Screen.width / 10, Screen.height * 9 / 10, Screen.width / 3, Screen.height / 50);
        staminaTexture = new Texture2D(1, 1);
        staminaTexture.SetPixel(0, 0, Color.cyan);
        staminaTexture.Apply();

    }
    void OnGUI()
    {
        float ratio = stamina / maxStamina;
        float rectWidth = ratio * Screen.width / 3;
        staminaRect.width = rectWidth;
        GUI.DrawTexture(staminaRect, staminaTexture);
    }
    void SetRunning(bool isRunning)
    {
        this.isRunning = isRunning;
        pm.speed = isRunning ? runSpeed : walkSpeed;
 
    }
    // Update is called once per frame
    void Update()
    {
        bool onGround = Physics.CheckSphere(pm.groundCheck.position, pm.groundDistance, pm.groundMask);
        if (Input.GetKeyDown(KeyCode.LeftShift) && onGround == true)
            SetRunning(true);
      
        if (Input.GetKeyUp(KeyCode.LeftShift))
            SetRunning(false);
        if (isRunning)
        {
            stamina -= Time.deltaTime;
            if (stamina < 0)
            {
                stamina = 0;
                SetRunning(false);
            }
        }
        else if (stamina < maxStamina)
        {
            stamina += Time.deltaTime;
        }
    }
}
