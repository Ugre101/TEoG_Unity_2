using System;
using UnityEngine;

public abstract class BaseState
{
    public BaseState(GameObject parGameObject)
    {
        gameObject = parGameObject;
        transform = parGameObject.transform;
    }

    protected GameObject gameObject;
    protected Transform transform;

    public abstract Type Tick();
}

public class IdleState : BaseState
{
    public IdleState(GameObject parGameObject) : base(parGameObject)
    {
    }

    public override Type Tick()
    {
        throw new NotImplementedException();
    }
}

public class ChaseState : BaseState
{
    public ChaseState(GameObject parGameObject) : base(parGameObject)
    {
    }

    public override Type Tick()
    {
        throw new NotImplementedException();
    }
}