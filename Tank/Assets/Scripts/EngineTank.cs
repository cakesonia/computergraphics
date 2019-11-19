using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTank : MonoBehaviour {

    //the tank's speed
    float MoveSpeed = 1.5f;
    //the tank's rotate speed
    float RotateSpeed = 20;
    //the tank's current speed
    float CurrentSpeed = 0;
    //the tank's speed
    int SpeedNum = 0;
    //the tank movement's component
    Rigidbody TankEngine;

    //куля
    public GameObject Bullet;
    //ствол
    public GameObject StartBarrel;

    //a function for tank's moving
    void MoveTank()
    {
        //where a tank will move
        Vector3 Move = transform.forward * CurrentSpeed * MoveSpeed * Time.deltaTime;
        
        //get the current tank's position
        Vector3 Position = TankEngine.position + Move;
        
        //move a tank
        TankEngine.MovePosition(Position);

    }

    //the tank's rotation
    void RotateTank()
    {
        //calculate the rotation
        float R = Input.GetAxis("Horizontal") * RotateSpeed * Time.deltaTime;
        
        //create new angle of tank's rotation
        Quaternion RotateAngle = Quaternion.Euler(0, R, 0);
        
        //get the current tank's rotation angle
        Quaternion CurrentAngle = TankEngine.rotation * RotateAngle;
        
        //rotate a tank
        TankEngine.MoveRotation(CurrentAngle);
    }

    //increase the tank's speed
    void UpSpeed()
    {
        if ((SpeedNum + 1) < 2) SpeedNum++;
    }

    //reduce the tank's speed
    void DownSpeed()
    {
        if ((SpeedNum - 1) > -2) SpeedNum--;
    }

    void Transmission()
    {
        switch (SpeedNum)
        {
            case -1: CurrentSpeed = -1; RotateSpeed = 25; break; //reverse
            case 0: CurrentSpeed = 0; RotateSpeed = 20; break; //neutral
            case 1: CurrentSpeed = 1f; RotateSpeed = 25; break; //forward
        }
    }

    //to know that a tank "crashed" in something
    void OnCollisionEnter(Collision collision)
    {
        SpeedNum = 0;
    }

    void Fire()
    {
        if (Input.GetButtonUp("Fire4"))
        {
            //the current coordinates of barrel for the bullet creating
            Vector3 MazzlePoint = StartBarrel.transform.position;
            Quaternion MazzleRotation = StartBarrel.transform.rotation;
            GameObject Bullet_for_fire = Instantiate(Bullet, MazzlePoint, MazzleRotation) as GameObject;

            //get a component
            Rigidbody Run = Bullet_for_fire.GetComponent<Rigidbody>();
            //speed up the bullet
            Run.AddForce(Bullet_for_fire.transform.forward * 30, ForceMode.Impulse);

            Destroy(Bullet_for_fire, 1);
        }
    }

    void FixedUpdate()
    {
        Fire();
        Transmission();
        MoveTank();
        RotateTank();
    }

    // Use this for initialization
    void Start () {
        //get tank movement's component
        TankEngine = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // remember what user has pressed
        float Axis = Input.GetAxis("Vertical");

        if (Input.GetButtonUp("Vertical"))
        {
            if (Axis > 0) UpSpeed();
            if (Axis < 0) DownSpeed();
        }
    }
}
