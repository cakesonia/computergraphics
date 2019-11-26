using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTank : MonoBehaviour {

    //the tank's speed
    [SerializeField] float MoveSpeed = 1.5f;
    //the tank's rotate speed
    float RotateSpeed = 20;
    
    
    int SpeedNum = 0;
    //the tank movement's component
    Rigidbody TankEngine;

    //куля
    public GameObject Bullet;
    //ствол
    public GameObject StartBarrel;

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

    void Move(KeyCode Key, int direction)
    {
        if (Input.GetKey(Key))
        {
            TankEngine.velocity = transform.rotation * Vector3.forward * MoveSpeed * direction;
        }
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

    void Fire()
    {
        if (Input.GetButtonUp("Fire4"))
        {
            //the current coordinates of barrel for the bullet creating
            Vector3 MazzlePoint = StartBarrel.transform.position;
            Quaternion MazzleRotation = StartBarrel.transform.rotation;
            GameObject BulletForFire = Instantiate(Bullet, MazzlePoint, MazzleRotation) as GameObject;

            //get a component
            Rigidbody Run = BulletForFire.GetComponent<Rigidbody>();
            
            //speed up the bullet
            Run.AddForce(BulletForFire.transform.forward * 30, ForceMode.Impulse);

            Destroy(BulletForFire, 1);
        }
    }

    // Use this for initialization
    void Start () {
        //get tank movement's component
        TankEngine = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        Fire();
        Move(KeyCode.UpArrow, 1);
        Move(KeyCode.DownArrow, -1);
        RotateTank();
    }
}
