using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that represents game modifiers.
/// </summary>
public class Modifier
{
    public string Name { get; set; }
    public float Value { get; set; }
    public ModifierType Type { get; set; }
}
