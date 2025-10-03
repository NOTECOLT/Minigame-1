using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CopState
{
    protected CopController cc;

    Rigidbody2D _rb;

    public CopState(CopController cc)
    {
        this.cc = cc;
        _rb = cc.gameObject.GetComponent<Rigidbody2D>();
    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

    public virtual CopState NextState()
    {
        return this;
    }
}
