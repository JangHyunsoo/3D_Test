using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBall : MonoBehaviour
{
    private float start_y = 0.65f;
    private float end_y = 5f;
    private float start_angle;
    private float speed = 1f;
    private float end_time = 2f;
    private float during_time = 0f;
    [SerializeField]
    private Vector3 end_point = Vector3.zero;
    private float origin_y = 0f;

    [SerializeField]
    private Text text_;

    private Queue<XZFuncData> xy_func = new Queue<XZFuncData>();

    private void Start()
    {
        origin_y = transform.position.y;
        start_angle = Mathf.Asin(start_y / end_y);
        xy_func.Enqueue(new XZFuncData(transform.position, transform.position + end_point, 0, end_time));
    }

    private void Update()
    {
        during_time += Time.deltaTime;
        text_.text = during_time.ToString();

        if (during_time >= xy_func.Peek().end_time)
        {
            xy_func.Dequeue();
        }
        else
        {
            transform.position = new Vector3(0f, getYMovement(during_time), 0f) + getXYMovement(xy_func.Peek(), during_time);
            Debug.Log(transform.position.y);
        }
        
        if(xy_func.Count == 0)
        {
            gameObject.active = false;
        }
    }

    private float getYTimeScale()
    {
        return (Mathf.PI - start_angle) / end_time;
    }

    private float getYMovement(float t)
    {
        return end_y * Mathf.Sin(start_angle + t * getYTimeScale() * speed);
    }

    private Vector3 getXYMovement(XZFuncData xy_funct, float t)
    {
        return xy_funct.getValue(t);
    }
}

public class XZFuncData
{
    public float a_xz;
    public float y_xz;
    public float a_xt;
    public float y_xt;
    public float end_time;

    public XZFuncData(Vector3 one, Vector3 other, float s_t, float e_t)
    {
        var temp = (other - one);
        a_xz = temp.z / temp.x;
        y_xz = a_xz * one.x - one.z;
        a_xt = (other.x - one.x) / (e_t - s_t);
        y_xt = a_xt * s_t + one.x;
        end_time = e_t;
    }

    public Vector3 getValue(float t)
    {
        float x = getX(t);
        float z = a_xz * x + y_xz;
        return new Vector3(x, 0, z);
    }

    private float getX(float t)
    {
        return a_xt * t + y_xt;
    }
}