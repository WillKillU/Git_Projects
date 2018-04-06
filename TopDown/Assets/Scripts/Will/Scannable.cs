using UnityEngine;
using System.Collections;

public class Scannable : MonoBehaviour
{
	public Animator UIAnim;

	public virtual void Ping()
	{
        UIAnim.SetTrigger("Ping");
    }
}
