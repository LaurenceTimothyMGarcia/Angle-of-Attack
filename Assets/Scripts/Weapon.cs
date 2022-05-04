using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20f)) - transform.position;

//	if(difference.z == 0)
//		difference.Set(difference.x, difference.y, 0.1f);
	
	float rotX = Mathf.Atan2(difference.y,difference.z) * Mathf.Rad2Deg;

	transform.rotation = Quaternion.Euler((rotX*-1) + offset, 0f, 0f);

	if(timeBtwShots <= 0)
{
	if(Input.GetMouseButtonDown(0))
	{
		Instantiate(projectile, shotPoint.position, transform.rotation);
timeBtwShots = startTimeBtwShots;
	}
}
else
{
	timeBtwShots -= Time.deltaTime;
}



    }
}
