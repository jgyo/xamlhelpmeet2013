// <copyright file="XamlTag.cs" company="YoderZone.com">
// Copyright (c) 2014 Gil Yoder. All rights reserved.
// </copyright>
// <author>Gil Yoder</author>
// <date>9/19/2014</date>
// <summary>Implements the XAML tag class</summary>
// <remarks>
// Licensed under the Microsoft Public License (Ms-PL); you may not
// use this file except in compliance with the License. You may obtain a copy
// of the License at
//
// https://remarker.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
// WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
// License for the specific language governing permissions and limitations
// under the License.
// </remarks>
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XamlHelpmeet.Utility.XamlParts
{
/// <summary>
/// An XAML tag.
/// </summary>
/// <seealso cref="T:XamlHelpmeet.Utility.XamlParts.XamlPart"/>
public class XamlTag : XamlPart
{
    /// <summary>
    /// Initializes a new instance of the
    /// XamlHelpmeet.Utility.XamlParts.XamlTag class.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when one or more arguments have unsupported or illegal
    /// values.
    /// </exception>
    /// <param name="match">
    /// Specifies the match.
    /// </param>
    public XamlTag(Match match) : base(match.Value, match.Index)
    {
        try
        {
            var matchResults = Regex.Match(match.Value,
                                           @"^((?'Start'<(?'TagName'[\w:.]+?)(?=[\s>])[^><]*?(?<!/)>)|
                                                (?'End'</(?'TagName'[\w:.]+?)(?=[\s>]).*?>)|
                                                (?'SelfClosed'<(?'TagName'[\w:.]+?)(?=[\s/])[^><]*?/>))$",
                                           RegexOptions.Singleline | RegexOptions.IgnoreCase |
                                           RegexOptions.IgnorePatternWhitespace);

            if (matchResults.Success)
            {
                Name = matchResults.Groups["TagName"].Captures[0].Value;
                TagType = matchResults.Groups["Start"].Success ? XamlTagType.Starting :
                          matchResults.Groups["End"].Success ? XamlTagType.Ending :
                          matchResults.Groups["SelfClosed"].Success ? XamlTagType.SelfClosing :
                          XamlTagType.Unknown;
            }
            else
            {
                throw new ArgumentException("The argument does not contain an XML tag match.",
                                            "match");
            }
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("The regular expression contains a syntax error.",
                                        ex);
        }

        TagText = match.Value;
        StartingIndex = match.Index;
        Length = TagText.Length;
        EndingIndex = StartingIndex + Length;
    }

    /// <summary>
    /// Gets the zero-based index of the starting.
    /// </summary>
    /// <value>
    /// The starting index.
    /// </value>
    public int StartingIndex { get; private set; }

    /// <summary>
    /// Gets the zero-based index of the ending.
    /// </summary>
    /// <value>
    /// The ending index.
    /// </value>
    public int EndingIndex { get; private set; }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <value>
    /// The length.
    /// </value>
    public int Length { get; private set; }

    /// <summary>
    /// Gets the tag text.
    /// </summary>
    /// <value>
    /// The tag text.
    /// </value>
    public string TagText { get; private set; }

    /// <summary>
    /// Gets the type of the tag.
    /// </summary>
    /// <value>
    /// The type of the tag.
    /// </value>
    public XamlTagType TagType { get; private set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name
    {
        get;
        private set;
    }

    /// <summary>
    /// The attributes.
    /// </summary>
    private List<XamlAttribute> _attributes;

    /// <summary>
    /// Gets the attributes.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when one or more arguments have unsupported or illegal
    /// values.
    /// </exception>
    /// <returns>
    /// The attributes.
    /// </returns>
    public IReadOnlyList<XamlAttribute> GetAttributes()
    {
        if (_attributes == null)
        {
            try
            {
                var regexObj = new Regex(
                    @"(?:\s+(?<Attribute>(?<AttribName>\w+?(?::\w+?)?)=""(?<AttribValue>.*?)""))",
                    RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var matches = regexObj.Matches(TagText);
                if (matches.Count > 0)
                {
                    _attributes = new List<XamlAttribute>();
                    foreach (Match match in matches)
                    {
                        var attrib = new XamlAttribute(match.Value, TopPoint + match.Index,
                                                       match.Groups["AttribName"].Captures[0].Value,
                                                       match.Groups["AttribValue"].Captures[0].Value);
                        _attributes.Add(attrib);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
                throw new ArgumentException("Syntax error in regular expression.", ex);
            }
        }
        return _attributes;
    }
}
}
