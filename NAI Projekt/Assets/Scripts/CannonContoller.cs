using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using UnityEngine;

public class CannonContoller : UnitController
{
    private GameObject target;
    public bool IsRunning;
    private IBlackBox box;

    public float fitness;
    public float distance;

    public float angle;
    public Vector2 force = new Vector2();
    public GameObject arrow;
    public bool b;
    public float targetx;
    public float targety;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (IsRunning)
        {
            target = GameObject.FindGameObjectWithTag("Target");
            targetx = target.transform.position.x;
            targety = target.transform.position.y;

            // initialize input and output signal arrays
            ISignalArray inputArr = box.InputSignalArray;
            ISignalArray outputArr = box.OutputSignalArray;

            inputArr[0] = target.transform.position.x;
            inputArr[1] = target.transform.position.y;
            inputArr[3] = fitness;

            

            
            if (b == false)
            {
                box.Activate();
                force.x = (float)outputArr[0] * 2000;
                force.y = (float)outputArr[1] * 2000;
                //Rotate(angle);
                Shoot(force);
                b = true;
            }
            distance = GetDistance();

            if (distance > 0)
            {//cannot divide by zero and cannot be closer to something than 0
                AddFitness(Mathf.Abs(1 / distance));
            }
            
        }
    }

    void AddFitness(float fit)
    {
        //increment our fitness score on every frame by the fit value
        fitness += fit;
    }

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        this.IsRunning = true;
    }

    public override float GetFitness()
    {
        var fit = fitness;//cache the fitness value
        fitness = 0;//reset fitness value each time we start a new training cycle

        if (fit < 0)
            fit = 0;

        return fit;
    }

    public override void Stop()
    {
        this.IsRunning = false;
    }

    void Rotate(float r)
    {
        float smooth = 2f;
        float tilt = r;
        Quaternion t = Quaternion.Euler(0, 0, tilt);
        //transform.rotation = t;
    }

    void Shoot(Vector3 f)
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject pArrow = Instantiate(arrow, gameObject.transform);
        pArrow.transform.localScale = new Vector3(0.16f, 0.6f, 1);
        pArrow.GetComponent<Rigidbody2D>().AddForce(f);
    }
    
    float GetDistance()
    {
        return gameObject.transform.GetChild(0).GetComponent<IgnoreCollision>().minDis;    
    }
}
