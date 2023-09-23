using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTest : MonoBehaviour
{
    public GameObject weapon;
    public GameObject weaponsp;
    public GameObject Player;
    public GameObject aim;
    Vector3 mouse;
    Vector3 target;
    Vector3 screen;
    float angle;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //target = Vector3.zero;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //target = cam.WorldToScreenPoint(weapon.transform.position);
        //screen = new Vector3(target.x, target.z, 0f);
        //mouse = Input.mousePosition;
        target = aim.transform.position;
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 distance = new Vector3(mouse.x - target.x, mouse.y - target.y, 0f).normalized;
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x ) * Mathf.Rad2Deg;
        weapon.transform.position = Player.transform.position + new Vector3(0, 0.6f, 0);
        weaponsp.transform.position = Player.transform.position + new Vector3(0, 0.6f, 0);
        weapon.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        weaponsp.transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.down);
        Debug.Log($"mouse : {mouse} distance : {screen}");
    }
}
