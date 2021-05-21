using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Enumに文字列を付加するためのAttributeクラス
/// </summary>
public class StringValueAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public string StringValue { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public StringValueAttribute(string value)
    {
        this.StringValue = value;
    }

}

//210218 技能レベルに応じた必要経験値を持たせる為に作ったが、汎用的に使ってよい
public class IntValueAttribute : Attribute
{
    public int IntValue { get; protected set; }

    public IntValueAttribute(int value)
    {
        this.IntValue = value;
    }
}

public class ReimuRouteAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public bool ReimuRoute { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public ReimuRouteAttribute(bool value)
    {
        this.ReimuRoute = value;
    }
}

//210210 状態によって十字キーによるカーソル移動を制御したい
public class ControllAbleAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public bool ControllAble { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public ControllAbleAttribute(bool value)
    {
        this.ControllAble = value;
    }
}

public class MenuVisibleAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public bool MenuVisible { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public MenuVisibleAttribute(bool value)
    {
        this.MenuVisible = value;
    }
}

/// <summary>
/// 武器熟練度がDよりEが高いとか比べられないので数値で比較する為設定
/// </summary>
public class PriorityAttribute : Attribute
{
    /// <summary>
    /// Holds the stringvalue for a value in an enum.
    /// </summary>
    public int Priority { get; protected set; }

    /// <summary>
    /// Constructor used to init a StringValue Attribute
    /// </summary>
    /// <param name="value"></param>
    public PriorityAttribute(int value)
    {
        this.Priority = value;
    }
}


public static class CommonAttribute
{

    /// <summary>
    /// Will get the string value for a given enums value, this will
    /// only work if you assign the StringValue attribute to
    /// the items in your enum.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetStringValue(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return null;

        StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

        // Return the first if there was a match.
        return attribs.Length > 0 ? attribs[0].StringValue : null;

    }

    public static int GetIntValue(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return 0;

        IntValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(IntValueAttribute), false) as IntValueAttribute[];

        // Return the first if there was a match.
        return attribs[0].IntValue;

    }

    /// <summary>
    /// Will get the string value for a given enums value, this will
    /// only work if you assign the StringValue attribute to
    /// the items in your enum.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetChapterValue(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return null;

        ChapterValueAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(ChapterValueAttribute), false) as ChapterValueAttribute[];

        // Return the first if there was a match.
        return attribs.Length > 0 ? attribs[0].StringValue : null;

    }

    public static int GetPriorityValue(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return 0;

        PriorityAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(PriorityAttribute), false) as PriorityAttribute[];

        // Return the first if there was a match.
        return attribs[0].Priority;

    }

    public static bool IsReimuRoute(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return false;

        ReimuRouteAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(ReimuRouteAttribute), false) as ReimuRouteAttribute[];

        // Return the first if there was a match.
        return attribs[0].ReimuRoute;

    }

    public static bool isControllAble(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return false;

        ControllAbleAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(ControllAbleAttribute), false) as ControllAbleAttribute[];

        // Return the first if there was a match.
        return attribs[0].ControllAble;

    }

    public static bool isMenuVisible(this Enum value)
    {
        // 引数のEnumを取得
        Type type = value.GetType();

        // Get fieldinfo for this type
        System.Reflection.FieldInfo fieldInfo = type.GetField(value.ToString());

        //範囲外の値チェック
        if (fieldInfo == null) return false;

        MenuVisibleAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(MenuVisibleAttribute), false) as MenuVisibleAttribute[];

        // Return the first if there was a match.
        return attribs[0].MenuVisible;

    }
}