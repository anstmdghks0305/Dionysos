using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    public GameObject weapon;
    public GameObject weaponsp;
    Vector3 mouse;
    Vector3 target;
    float angle;
    float anglesp;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        target = weapon.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponsp.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.down);
    }
}
