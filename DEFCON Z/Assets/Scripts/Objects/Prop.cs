using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : ObjectBase
{

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		DamageObject(0f);
    }

	public override void DamageObject(float damage)
	{
		if (destructable)
		{
			health -= damage;

			if (health <= 0.0f)
			{
				DestroySelf();
			}
		}
	}

	public override void DestroySelf()
	{
		Debug.Log(this.name + " has reached 0 or less health and has been destroyed");
		Destroy(gameObject); // Remove the game object this script is attached to
	}
}
