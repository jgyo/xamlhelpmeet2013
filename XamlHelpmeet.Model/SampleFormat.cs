﻿using System;

namespace XamlHelpmeet.Model
{
using NLog;

[Serializable]
public class SampleFormat
{
    private static readonly Logger logger =
        LogManager.GetCurrentClassLogger();

    public string DataType { get; set; }

    public string Example { get; set; }

    public SampleFormat(string DataType, string Example, string StringFormat)
    {
        logger.Trace("Entered SampleFormat()");

        this.DataType = DataType;
        this.Example = Example;
        this.StringFormat = StringFormat;
    }

    public string StringFormat { get; set; }

    public string StringFormatAndExample
    {
        get
        {
            return ToString();
        }
    }

    public string StringFormatParsedValue
    {
        get
        {
            // What is this for? There are no back slashes in
            // the SampleFormats created within the program
            // as far as I can tell.
            return StringFormat.Replace(@"\", string.Empty);
        }
    }



    public override String ToString()
    {
        logger.Trace("Entered ToString()");

        return string.Format("{0} - {1}", StringFormatParsedValue, Example);
    }

}
}
