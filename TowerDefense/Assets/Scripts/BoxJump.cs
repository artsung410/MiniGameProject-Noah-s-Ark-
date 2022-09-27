using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour
{
    protected float Animation;

    private void Update()
    {
        //Animation += Time.deltaTime;

        //Animation = Animation % 5f;

        //transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, Animation / 5f);

        if (Input.GetMouseButton(0))
        {
            GetComponent<ParabolaController>().FollowParabola();
        }
    }
}
