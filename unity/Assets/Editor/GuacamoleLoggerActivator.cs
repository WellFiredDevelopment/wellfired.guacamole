using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class GuacamoleLoggerActivator {

	static GuacamoleLoggerActivator()
	{
		WellFired.Guacamole.Diagnostics.Logger.RegisterLogger(new WellFired.Guacamole.Unity.Editor.Diagnostics.Logger());
	    UnitySystemConsoleRedirector.Redirect();
	}

    private static class UnitySystemConsoleRedirector
        {
            private class UnityTextWriter : TextWriter
            {
                private readonly StringBuilder _buffer = new StringBuilder();
    
                public override void Flush()
                {
                    Debug.Log(_buffer.ToString());
                    _buffer.Length = 0;
                }
    
                public override void Write(string value)
                {
                    _buffer.Append(value);
                    if (string.IsNullOrEmpty(value) || value [value.Length - 1] != '\n') return;
                    Flush();
                }
    
                public override void Write(char value)
                {
                    _buffer.Append(value);
                    if (value == '\n')
                    {
                        Flush();
                    }
                }
    
                public override void Write(char[] value, int index, int count)
                {
                    Write(new string(value, index, count));
                }
    
                public override Encoding Encoding
                {
                    get { return Encoding.Default; }
                }
            }
    
            public static void Redirect()
            {
                Console.SetOut(new UnityTextWriter());
            }
        }
}