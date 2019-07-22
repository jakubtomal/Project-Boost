﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    [SerializeField] float flyingSpeed;
    [SerializeField] float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Fly(flyingSpeed);
        }

        else if(Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateOnZAxis(rotationSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateOnZAxis(-rotationSpeed);
        }
    }

    private void RotateOnZAxis(float speed)
    {
        gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Fly(float speed)
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying) { gameObject.GetComponent<AudioSource>().Play(); }
        gameObject.GetComponent<Rigidbody>().velocity += gameObject.transform.up * speed;
    }



}

