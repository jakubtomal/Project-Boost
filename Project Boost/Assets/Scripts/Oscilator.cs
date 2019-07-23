using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscilator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(-1,1)][SerializeField] float movementFactor;
    [SerializeField] float period = 2f;
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if(period > Mathf.Epsilon)
        {
            float cycle = Time.time / period;
            const float tau = Mathf.PI * 2f;
            float rowSinWave = Mathf.Sin(tau * cycle);
            movementFactor = rowSinWave;
            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
