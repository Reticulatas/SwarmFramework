using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCharacter : Entity
{
    public static List<PlayerCharacter> characters = new List<PlayerCharacter>();

    public Camera camera;
    public float TurnSpeed = 0.1f;
    public float StrafeSpeed = 1.0f;
    public float ForwardSpeed = 2.0f;
    public int PlayerNumber = 0;

    public TMPro.TextMeshProUGUI AmmoText, GunText, HealthText;

    public GC_Gun gun;

    public string inputTag;

	void Start ()
	{
	    gun = gameObject.GetComponentInChildren<GC_Gun>();
	    characters.Add(this);
	    inputTag = PlayerNumber == 0 ? "P1" : "P2";
	}
	
	public override void Update ()
	{
	    if (!Dead)
	    {
	        var camPos = new Vector3(camera.transform.position.x, gameObject.transform.position.y,
	            camera.transform.position.z);
	        var dir = gameObject.transform.position - camPos;
	        dir.Normalize();

	        var forward = transform.forward;
	        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(forward, dir, TurnSpeed, 0.0f));

	        if (Input.GetButtonDown(inputTag + "Fire1"))
	        {
	            gun.Fire(this);
            }
            if (Input.GetButtonDown(inputTag + "Reload"))
            {
                gun.Reload();
	        }
	    }

	    base.Update();
	}

    void LateUpdate()
    {
        if (!Dead)
        {
            transform.Translate(transform.right*Time.deltaTime*StrafeSpeed*Input.GetAxis(inputTag + "Horizontal"), Space.World);
            transform.Translate(transform.forward*Time.deltaTime*ForwardSpeed*Input.GetAxis(inputTag + "Vertical"), Space.World);
        }
    }

    void FixedUpdate()
    {
        if (gun != null)
        {
            AmmoText.text = gun.Ammo + " / " + gun.MaxAmmo;
        }
        if (gun is GC_StandardGun)
        {
            if ((gun as GC_StandardGun).Reloading)
            {
                AmmoText.text = "Reloading";
            }
        }

        HealthText.text = this.Health + " / " + this.MaxHealth + " Health";
    }
}
