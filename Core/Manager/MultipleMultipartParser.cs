using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <summary>
/// MultipartParser http://multipartparser.codeplex.com
/// Reads a multipart form data stream and returns the filename, content type and contents as a stream.
/// 2009 Anthony Super http://antscode.blogspot.com
/// </summary>
namespace Core.Manager
{
    public class MultipleMultipartParser
{
    public MultipleMultipartParser(Stream stream)
    {
        this.Parse(stream, Encoding.UTF8);
    }


    public static byte[][] Separate(byte[] source, byte[] separator)
    {
        var Parts = new List<byte[]>();
        var Index = 0;
        byte[] Part;
        for (var I = 0; I < source.Length; ++I)
        {
            if (Equals(source, separator, I))
            {
                Part = new byte[I - Index];
                Array.Copy(source, Index, Part, 0, Part.Length);
                Parts.Add(Part);
                Index = I + separator.Length;
                I += separator.Length - 1;
            }
        }
        Part = new byte[source.Length - Index];
        Array.Copy(source, Index, Part, 0, Part.Length);
        Parts.Add(Part);
        return Parts.ToArray();
    }

    public static bool Equals(byte[] source, byte[] separator, int index)
    {
        for (int i = 0; i < separator.Length; ++i)
            if (index + i >= source.Length || source[index + i] != separator[i])
                return false;
        return true;
    }
    private void Parse(Stream stream, Encoding encoding)
    {
        Regex regQuery;
        Match regMatch;
        string propertyType;

        // The first line should contain the delimiter
        byte[] data = ToByteArray(stream);
        // Copy to a string for header parsing
        string content = encoding.GetString(data);

        int delimiterEndIndex = content.IndexOf("\r\n");
        if (delimiterEndIndex > -1)
        {
            string delimiterString = content.Substring(0, content.IndexOf("\r\n"));
            byte[] delimiterBytes = encoding.GetBytes(delimiterString);
            byte[] delimiterWithNewLineBytes = encoding.GetBytes(delimiterString + "\r\n");
            // the request ends DELIMITER--\r\n
            byte[] delimiterEndBytes = encoding.GetBytes("\r\n" + delimiterString + "--\r\n");
            int lengthDifferenceWithEndBytes = (delimiterString + "--\r\n").Length;

            byte[][] separatedStream = Separate(data, delimiterWithNewLineBytes);
            data = null;
            for (int i = 0; i < separatedStream.Length; i++)
            {
                // parse out whether this is a parameter or a file
                // get the first line of the byte[] as a string
                string thisPieceAsString = encoding.GetString(separatedStream[i]);

                if (string.IsNullOrWhiteSpace(thisPieceAsString)) { continue; }

                string firstLine = thisPieceAsString.Substring(0, thisPieceAsString.IndexOf("\r\n"));

                // Check the item to see what it is
                regQuery = new Regex(@"(?<=name\=\"")(.*?)(?=\"")");
                regMatch = regQuery.Match(firstLine);
                propertyType = regMatch.Value.Trim();

                // get the index of the start of the content and the end of the content
                int indexOfStartOfContent = thisPieceAsString.IndexOf("\r\n\r\n") + "\r\n\r\n".Length;

                // this line compares the name to the name of the html input control, 
                // this can be smarter by instead looking for the filename property
                StreamContent myContent = new StreamContent();
                if (propertyType.ToUpper().Trim() != "FILES")
                {
                    // this is a parameter!
                    // if this is the last piece, chop off the final delimiter
                    int lengthToRemove = (i == separatedStream.Length - 1) ? lengthDifferenceWithEndBytes : 0;
                    string value = thisPieceAsString.Substring(indexOfStartOfContent, thisPieceAsString.Length - "\r\n".Length - indexOfStartOfContent - lengthToRemove);
                    myContent.StringData = value;
                    myContent.PropertyName = propertyType;
                    if (StreamContents == null)
                        StreamContents = new List<StreamContent>();

                    StreamContents.Add(myContent);
                    this.Success = true;
                }
                else
                {
                    // this is a file!
                    regQuery = new Regex(@"(?<=filename\=\"")(.*?)(?=\"")");
                    regMatch = regQuery.Match(firstLine);
                    string fileName = regMatch.Value.Trim();

                    // get the content byte[]
                    // if this is the last piece, chop off the final delimiter
                    int lengthToRemove = (i == separatedStream.Length - 1) ? delimiterEndBytes.Length : 0;
                    int contentByteArrayStartIndex = encoding.GetBytes(thisPieceAsString.Substring(0, indexOfStartOfContent)).Length;
                    byte[] fileData = new byte[separatedStream[i].Length - contentByteArrayStartIndex - lengthToRemove];
                    Array.Copy(separatedStream[i], contentByteArrayStartIndex, fileData, 0, separatedStream[i].Length - contentByteArrayStartIndex - lengthToRemove);
                    // save the fileData byte[] as the file
                    myContent.PropertyName = propertyType;
                    myContent.FileName = fileName;
                    myContent.IsFile = true;
                    myContent.Data = fileData;
                    if (StreamContents == null)
                        StreamContents = new List<StreamContent>();
                    StreamContents.Add(myContent);
                    this.Success = true;
                }

            }
        }
    }

    private byte[] ToByteArray(Stream stream)
    {
        byte[] buffer = new byte[32768];
        using (MemoryStream ms = new MemoryStream())
        {
            while (true)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return ms.ToArray();
                ms.Write(buffer, 0, read);
            }
        }
    }

    public List<StreamContent> StreamContents { get; set; }

    public bool Success
    {
        get;
        private set;
    }

    public string ContentType
    {
        get;
        private set;
    }

    public string Filename
    {
        get;
        private set;
    }

    public List<byte[]> FileContents
    {
        get;
        private set;
    }
}

public class StreamContent
{
    public byte[] Data { get; set; }
    public bool IsFile { get; set; }
    public string FileType { get; set; }
    public string FileName { get; set; }
    public string PropertyName { get; set; }
    public string StringData { get; set; }
}
}
