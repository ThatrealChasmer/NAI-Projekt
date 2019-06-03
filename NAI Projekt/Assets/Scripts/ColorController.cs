using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using UnityEngine;

public class ColorController : UnitController
{
    public Color color;
    private bool isRunning;
    private IBlackBox box;

    public float fitness;

    public int red;
    public int green;
    public int blue;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate(IBlackBox box)
    {
        throw new System.NotImplementedException();
    }

    public override float GetFitness()
    {
        throw new System.NotImplementedException();
    }

    public override void Stop()
    {
        throw new System.NotImplementedException();
    }
}
