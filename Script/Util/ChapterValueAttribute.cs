using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Enumに文字列を付加するためのAttributeクラス
/// </summary>
public class ChapterValueAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public ChapterValueAttribute(string value)
    {
        this.StringValue = value;
    }
}