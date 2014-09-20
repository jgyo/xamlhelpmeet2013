// <copyright file="XamlCollection.cs" company="Gil Yoder">
// Copyright (c) 2013 Gil Yoder. All rights reserved.
// </copyright>
// <author>Gil Yoder</author>
// <date>3/8/2013</date>
// <summary>Implements the xaml collection class</summary>

using System.Collections.Generic;

namespace XamlHelpmeet.Utility
{
using NLog;

/// <summary>
///     Collection of xaml nodes.
/// </summary>
/// <seealso cref="T:System.Collections.Generic.LinkedList{XamlHelpmeet.Utility.XamlNode}"/>
public sealed class XamlNodeCollection : LinkedList<XamlNode>
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    private readonly XamlNode _owner;

    /// <summary>
    ///     Initializes a new instance of the XamlNodeCollection class.
    /// </summary>
    /// <param name="owner">
    ///     The owner.
    /// </param>
    public XamlNodeCollection(XamlNode owner)
    {

        logger.Trace("Entered XamlNodeCollection()");
        _owner = owner;
    }

    /// <summary>
    ///     Gets the owner.
    /// </summary>
    /// <value>
    ///     The owner.
    /// </value>
    public XamlNode Owner
    {
        get
        {
            return _owner;
        }
    }

}

/// <summary>
///     Xaml collection helper.
/// </summary>
public static class XamlCollectionHelper
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    /// <summary>
    ///     An XamlCollection extension method that queries if 'target' is
    ///     empty.
    /// </summary>
    /// <param name="target">
    ///     The target to act on.
    /// </param>
    /// <returns>
    ///     true if empty, otherwise false.
    /// </returns>
    public static bool IsEmpty(this XamlNodeCollection target)
    {

        logger.Trace("Entered IsEmpty()");
        return target == null || target.Count == 0;
    }
}
}
