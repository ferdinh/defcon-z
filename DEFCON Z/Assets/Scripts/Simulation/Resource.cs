using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A class that contains the available resources.
/// </summary>
public class Resource
{
    public float BaseResourcePoint { get; }
    public float MaxResourcePoint { get; set; }
    public float MaxSciencePoint { get; set; }
    public float SciencePoint { get; set; }
    public float ResourcePoint { get; set; }


    public IList<Modifier> Modifiers { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Resource"/> class.
    /// </summary>
    public Resource()
    {
        BaseResourcePoint = 10000.0f;
        Modifiers = new List<Modifier>();
    }

}