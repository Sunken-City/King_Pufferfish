using UnityEngine;
using System.Collections;

public class GlowScript : MonoBehaviour {

	public int glowLevel = 0;
    private Animator anim;
	// Use this for initialization
	void Start()
	{
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update()
	{
        anim.SetInteger("glowLevel", glowLevel);
		float charge = SprayBulletScript.GetChargeMeter();
        //transform.localRotation = new Quaternion(-transform.parent.localEulerAngles);
		if(charge <= .33f)
		{
			glowLevel = 0;
		}
		if(charge > 0.33f && charge <= 0.66f)
		{
			glowLevel = 1;
		}
		if(charge > 0.66f && charge < 1.0f)
		{
			glowLevel = 2;
		}
		if(charge >= 1.0f)
		{
			glowLevel = 3;
		}
	}
}
