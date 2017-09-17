#define DEBUG
//#undef DEBUG
#define RELEASE
#undef RELEASE

using System;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public static readonly Vector3 INITIAL_POSITION = Vector3.zero;

    private static readonly Vector3 G = new Vector3(0, -1, 0) * 9.8f;
    private static readonly Vector3 AXIS_X = new Vector3(1, 0, 0);
    private static readonly Vector3 AXIS_Y = new Vector3(0, 1, 0);
    private static readonly Vector3 AXIS_Z = new Vector3(0, 0, 1);
    
    public float thrustZ;
    public float thrustY;
    public GameObject target;

    private IntegratedController integratedController;
    private bool flagLaunched = false;
    private Camera mainCam;
    private new Rigidbody rigidbody;

#if DEBUG
    void Start()
    {
        integratedController = GetComponent<IntegratedController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 48)
        {
            StopArrow();
            integratedController.CallbackArrow(-1);
        }

        if (!flagLaunched)
        {
            var mPos = Input.mousePosition.y;
            transform.rotation = Quaternion.Euler(-(mPos % 90), 0f, 0f);
        }

        //左クリック
        if (!flagLaunched && Input.GetMouseButton(0))
            Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Target"))
        {
            StopArrow(true);

            var distance = Math.Abs(transform.position.y);

            if (distance <= 0.26)
                integratedController.CallbackArrow(4);
            else if (distance <= 1)
                integratedController.CallbackArrow(3);
            else if (distance <= 2)
                integratedController.CallbackArrow(2);
            else
                integratedController.CallbackArrow(1);
        }
        else if (other.name.Equals("Terrain"))
        {
            StopArrow();
            integratedController.CallbackArrow(-1);
        }
    }

    private void Launch()
    {
        var angDir = transform.eulerAngles.x * ((float)Math.PI / 180f);
        rigidbody.useGravity = true;
        rigidbody.AddForce(Vector3.forward * Mathf.Cos(-angDir) * thrustZ);
        rigidbody.AddForce(new Vector3(0f, Mathf.Sin(-angDir), 0f) * thrustY);
        flagLaunched = true;
    }

    private void StopArrow(bool adjust = false)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
        if (adjust)
        {
            transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            31f);
        }
    }

    public void ReInit()
    {
        transform.position = INITIAL_POSITION;
        flagLaunched = false;
    }
#endif

#if RELEASE
    void Start()
    {
        integratedController = GetComponent<IntegratedController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.z > 48)
        {
            StopArrow();
            integratedController.CallbackArrow(-1);
        }

        if (!flagLaunched)
        {
            var mPos = Input.mousePosition.y;
            transform.rotation = Quaternion.Euler(-(mPos % 90), 0f, 0f);
        }

        //左クリック
        if (!flagLaunched && Input.GetMouseButton(0))
            Launch();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Target"))
        {
            Invoke("StopArrow", delay);

            var distance = Math.Abs(transform.position.y);

            if (distance <= 0.26)
                integratedController.CallbackArrow(4);
            else if (distance <= 1)
                integratedController.CallbackArrow(3);
            else if (distance <= 2)
                integratedController.CallbackArrow(2);
            else
                integratedController.CallbackArrow(1);
        }
        else if (other.name.Equals("Terrain"))
        {
            StopArrow();
            integratedController.CallbackArrow(-1);
        }
    }

    private void Launch()
    {
        var angDir = transform.eulerAngles.x * ((float)Math.PI / 180f);
        rigidbody.useGravity = true;
        rigidbody.AddForce(Vector3.forward * Mathf.Cos(-angDir) * thrustZ);
        rigidbody.AddForce(new Vector3(0f, Mathf.Sin(-angDir), 0f) * thrustY);
        flagLaunched = true;
    }

    private void StopArrow()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;
    }

    public void ReInit()
    {
        transform.position = INITIAL_POSITION;
        flagLaunched = false;
    }
#endif
}
