using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using UnityEngine;

public class ColorController : UnitController
{
    public Color color;
    private bool IsRunning;
    private IBlackBox box;

    public float fitness;
    public float distance;

    public float red = 0.1f;
    public float green =0.1f;
    public float blue = 0.1f;

    public float t_red = 0.5f;
    public float t_green = 0.5f;
    public float t_blue = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsRunning)
        {
            // initialize input and output signal arrays
            ISignalArray inputArr = box.InputSignalArray;
            ISignalArray outputArr = box.OutputSignalArray;

            inputArr[0] = red;
            inputArr[1] = green;
            inputArr[2] = blue;
            inputArr[3] = fitness;

            distance = Mathf.Sqrt((red - t_red) * (red - t_red) + (green - t_green) * (green - t_green) + (blue - t_blue) * (blue - t_blue));

            box.Activate();

            red = (float)outputArr[0];
            green = (float)outputArr[1];
            blue = (float)outputArr[2];

            if (distance > 0)
            {//cannot divide by zero and cannot be closer to something than 0
                AddFitness(Mathf.Abs(1 / distance));
            }
            ChangeColor(red, green, blue);
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

    void ChangeColor(float r, float g, float b)
    {
        color.r = r;
        color.g = g;
        color.b = b;

        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
