﻿using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{

    public PlayerController currentController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h, v;
        float h2, v2;

        

        if (currentController)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            h2 = Input.GetAxis("RHorizontal");
            v2 = Input.GetAxis("RVertical");
            currentController.SetAxis(h, v);
            currentController.SetAxis2(h2, v2);

            currentController.a += Time.deltaTime;
            currentController.b += Time.deltaTime;
            currentController.c += Time.deltaTime;

            currentController.aDown = Input.GetButton("Fire1");
            currentController.bDown = Input.GetButton("Fire2");
            currentController.cDown = Input.GetButton("Fire3");

            if (Input.GetButtonDown("Fire1"))
            {
                currentController.A();
                currentController.a = 0;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                //currentController.AUp();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                currentController.B();
                currentController.b = 0;
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                currentController.BUp();
            }

            if (Input.GetButtonDown("Fire3"))
            {
                currentController.C();
                currentController.c = 0;
            }
            else if (Input.GetButtonUp("Fire3"))
            {
                currentController.CUp();
            }
        }
    }
}
