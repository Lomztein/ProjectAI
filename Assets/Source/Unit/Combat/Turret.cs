using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lomztein.ProjectAI.Sim;

public class Turret : SimComponent
{
    public Vector3 TargetPosition { get; private set; }

    public Transform baseTransform;
    public Transform yawTransform;
    public Transform pitchTransform;

    public Vector2 yawMinMax = new Vector2(-180, 180);
    public Vector2 pitchMinMax = new Vector2(-180, 180);

    public float rotationSpeed;

    public override void Init()
    {
        TargetPosition = transform.position + transform.forward * 10;
    }

    private void RotateTowardsTarget (float deltaTime)
    {
        Vector3 transformed = baseTransform.InverseTransformPoint(TargetPosition);
        Quaternion towards = Quaternion.LookRotation(transformed - Vector3.zero, baseTransform.up);

        RotateYaw(deltaTime, towards);
        RotatePitch(deltaTime, towards);
    }

    private void RotateYaw(float deltaTime, Quaternion towards)
    {
        yawTransform.localRotation = Quaternion.RotateTowards(yawTransform.localRotation, Quaternion.Euler (0, MinMax (towards.eulerAngles.y, yawMinMax), 0), rotationSpeed * deltaTime);
    }

    private void RotatePitch (float deltaTime, Quaternion towards)
    {
        pitchTransform.localRotation = Quaternion.RotateTowards(pitchTransform.localRotation, Quaternion.Euler(MinMax (towards.eulerAngles.x, pitchMinMax), 0, 0), rotationSpeed * deltaTime);
    }

    private float MinMax (float value, Vector2 minMax)
    {
        if (value > 180)
        {
            value -= 180;
            value = Mathf.Clamp(value, minMax.x + 180, minMax.y + 180);
            value += 180;
        }
        else
        {
            value += 180;
            value = Mathf.Clamp(value, minMax.x + 180, minMax.y + 180);
            value -= 180;
        }
        
        return value;
    }

    public void Target (Vector3 newPos)
    {
        TargetPosition = newPos;
    }

    public override void Kill()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick(float deltaTime)
    {
        Target(WorldCursor.Position);
        RotateTowardsTarget(deltaTime);
    }
}
