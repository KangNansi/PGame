using UnityEngine;
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
        

        if (currentController)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");
            currentController.SetAxis(h, v);
            if (Input.GetButtonDown("Fire1"))
            {
                currentController.A();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                currentController.B();
            }
            else if (Input.GetButtonUp("Fire2"))
            {
                currentController.BUp();
            }
        }
    }
}
