// <copyright file="XamlWhiteSpace.cs" company="Gil Yoder">
// Copyright (c) 2013 Gil Yoder. All rights reserved.
// </copyright>
// <author>Gil Yoder</author>
// <date>3/7/2013</date>
// <summary>Implements the xaml white space class</summary>

namespace XamlHelpmeet.Utility.XamlParts
{
using NLog;

/// <summary>
/// Xaml white space.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Utility.XamlParts.XamlPart"/>
public class XamlWhiteSpace : XamlPart
{
    /// <summary>
    /// The logger.
    /// </summary>
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Initializes a new instance of the XamlWhiteSpace class.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <param name="topPoint">
    /// The top point.
    /// </param>
    public XamlWhiteSpace(string text, int topPoint)
    : base(text, topPoint)
    {
        logger.Trace("Entered XamlWhiteSpace()");
    }
}
}
