using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public GameObject body;
    float moveLimiter = 0.99f;

    float horSpeed = 0.0f;
    float vertSpeed = 0.0f;

    public float maxSpeed = 7.0f;
    public float acceleration = 7.0f;
    public float deceleration = 7.0f;
    public float fetchBodyCooldown = 5.0f;
    public float timeStamp;

    public Slider cdSlider;

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        body = GameObject.Find("Body");
    }

    void Update()
    {
        movement();
        fetchBody();
    }

    void fetchBody(){
        if(Input.GetKeyDown("space"))
        {
            if(timeStamp <= Time.time)
            {
                CooldownUpdater();
                timeStamp = Time.time + fetchBodyCooldown;
                body.transform.position = rigidBody.transform.position;
            }
        }
    }

    void CooldownUpdater(){
        StartCoroutine(Cooldown(fetchBodyCooldown, cdSlider));
    }

    IEnumerator Cooldown(float seconds, Slider slider){
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            slider.value = Mathf.Lerp(1f, 0f, lerpValue);
            yield return null;
        }
    }


    void movement(){
        float horizontalInput = Input.GetAxisRaw("Horizontal");// -1 is left
        float verticalInput = Input.GetAxisRaw("Vertical");// -1 is down
        
        //Horizontal Acc - decc
        if((horizontalInput > 0) && horSpeed < maxSpeed)//right acc
        {
            if (Math.Abs(vertSpeed) > Math.Abs(horSpeed))
            {
                horSpeed = Math.Abs(vertSpeed);
            }
            else
            {
                horSpeed = horSpeed + acceleration * Time.deltaTime;
            }
        }
        else if((horizontalInput < 0) && horSpeed > -maxSpeed)//left acc
        {
            if (Math.Abs(vertSpeed) > Math.Abs(horSpeed))
            {
                horSpeed = -Math.Abs(vertSpeed);
            }
            else
            {
                horSpeed = horSpeed - acceleration * Time.deltaTime;
            }
        }
        else if (horSpeed > 0)
        {
            if(horSpeed > deceleration * Time.deltaTime)
            {
                horSpeed = horSpeed - deceleration * Time.deltaTime;
            }
            else
            {
                horSpeed = 0;
            }
        }
        else if (horSpeed < 0)
        {
            if(horSpeed < deceleration * Time.deltaTime)
            {
                horSpeed = horSpeed + deceleration * Time.deltaTime;
            }
            else
            {
                horSpeed = 0;
            }
        }

        // Vertical Acc - decc
        if((verticalInput > 0) && vertSpeed < maxSpeed)// up acc
        {
            if (Math.Abs(horSpeed) > Math.Abs(vertSpeed))
            {
                vertSpeed = Math.Abs(horSpeed);
            }
            else
            {
                vertSpeed = vertSpeed + acceleration * Time.deltaTime;
            }
        }
        else if((verticalInput < 0) && vertSpeed > -maxSpeed)// down acc
        {
            if (Math.Abs(horSpeed) > Math.Abs(vertSpeed))
            {
                vertSpeed = -Math.Abs(horSpeed);
            }
            else
            {
                vertSpeed = vertSpeed - acceleration * Time.deltaTime;
            }
        }
        else if (vertSpeed > 0)
        {
            if(vertSpeed > deceleration * Time.deltaTime)
            {
                vertSpeed = vertSpeed - deceleration * Time.deltaTime;
            }
            else
            {
                vertSpeed = 0;
            }
        }
        else if (vertSpeed < 0)
        {
            if(vertSpeed < deceleration * Time.deltaTime)
            {
                vertSpeed =vertSpeed + deceleration * Time.deltaTime;
            }
            else
            {
                vertSpeed = 0;
            }
        }

        if(horizontalInput != 0 && verticalInput != 0)
        {
            horSpeed *= moveLimiter;
            vertSpeed *= moveLimiter;
        }

        rigidBody.velocity = new Vector2(horSpeed, vertSpeed);
    }

}
