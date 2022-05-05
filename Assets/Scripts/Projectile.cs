using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

public float speed;
public float lifeTime;
public float distance;
public LayerMask whatIsSolid;

    // Start is called before the first frame update
    void Start()
    {
     	Invoke("DestroyProjectile", lifeTime);   
    }

    // Update is called once per frame
    void Update()
    {
		RaycastHit hitInfo;

		if(Physics.Raycast(transform.position, transform.up, out hitInfo, distance, whatIsSolid))
		{
			if(hitInfo.collider.CompareTag("Enemy")) Debug.Log("Enemy has been hit!");
			DestroyProjectile();
		}

		transform.Translate(Vector2.up * speed * Time.deltaTime);        
    }

	void DestroyProjectile()
	{
		Destroy(gameObject);
	}
}
