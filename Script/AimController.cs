using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField]
    [Range(0, 5)]
    private float aimSensitivity;

    [SerializeField]
    private GameObject body, gun;
    
    private PlayerControl playerIA;

    void Awake()
    {
        playerIA = new PlayerControl();
        playerIA.Movement.Enable();
    }

    void Update()
    {
        Vector2 mouseInput = playerIA.Movement.Aim.ReadValue<Vector2>();

        body.transform.Rotate(new Vector3(0, mouseInput.x, 0) * aimSensitivity, Space.Self);
        gun.transform.Rotate(new Vector3(mouseInput.y, 0, 0) * aimSensitivity * -1, Space.Self);

        // Debug.Log(mouseInput);
        // var x = gun.transform.eulerAngles.x;
        // var xclamp = Mathf.Clamp(gun.transform.eulerAngles.x, -60f, 60f);

        // Debug.Log(x.ToString() + " = " + xclamp.ToString());
        if (gun.transform.eulerAngles.x <= 180)
            gun.transform.eulerAngles = new Vector3(Mathf.Clamp(gun.transform.eulerAngles.x, 0, 60f), body.transform.eulerAngles.y, body.transform.eulerAngles.z);
        else 
            gun.transform.eulerAngles = new Vector3(Mathf.Clamp(gun.transform.eulerAngles.x, 300, 360f), body.transform.eulerAngles.y, body.transform.eulerAngles.z);
    }
}
