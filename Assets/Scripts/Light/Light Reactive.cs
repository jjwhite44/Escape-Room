using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//note: since this is an abstract class its not actually used on an object, just a template for subclasses
public abstract class LightReactive : MonoBehaviour
{
    public void BaseReact()
    {
        React();
    }

    protected virtual void React()
    {
        //won't have any code written here, this will be overridden by subclasses
    }
}
