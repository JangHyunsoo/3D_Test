using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfView : MonoBehaviour
{
    [SerializeField]
    private float ray_angle = 45;
    private float start_angle;
    private float angle_step = 5;
    private float radius = 5;

    [SerializeField]
    private int piece = 10;
    
    private Vector3 direction;

    private void FixedUpdate()
    {
        direction = transform.TransformDirection(Vector3.forward);
        float angle = Mathf.Atan2(direction.x, direction.z);

        if (angle < 0) angle += 2 * Mathf.PI;

        start_angle = angle - getRadian(ray_angle / 2);

        angle_step = ray_angle / piece;

        for (int i = 0; i < piece; i++)
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1, 0), new Vector3(Mathf.Sin(start_angle + (i * getRadian(angle_step))), 0f, Mathf.Cos(start_angle + (i * getRadian(angle_step)))) * radius, Color.red);
        }
    }

    private float getRadian(float _angle)
    {
        return _angle * Mathf.PI / 180;
    }

    private float getAngle(float _radian)
    {
        return _radian * 180 / Mathf.PI;
    }

}
