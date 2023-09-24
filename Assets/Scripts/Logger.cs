using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Logger
{

    private static readonly bool enable = true;

    static Logger ()
    {

    }

    public static void Log (object msg)
    {
        if (enable)
        {
            Debug.Log(msg);
        }
    }
}
