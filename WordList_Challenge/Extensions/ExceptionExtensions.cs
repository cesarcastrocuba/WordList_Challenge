namespace WordList_Challenge.Extensions
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    public static class ExceptionExtensions
    {
        public static void Log(this Exception e, string extraMessage)
        {
            StackTrace trace = new StackTrace(e, true);
            StackFrame frame = trace.GetFrame(0);
            MethodBase method = frame.GetMethod();

            string message = string.Format(
                @"
                    -----------------------------------------------------------------------\n
                    [ {0} ]\n
                    [{1} :: {2} ({3}, {4})]\n
                    {5}\n{6}
                ",
                 (string.IsNullOrEmpty(extraMessage) ? string.Empty : "[" + extraMessage + "]"),
                 (method.ReflectedType != null ? method.ReflectedType.Name : string.Empty),
                 method.Name,
                 frame.GetFileLineNumber(),
                 frame.GetFileColumnNumber(),
                 e.Message,
                 e.StackTrace
                );

            Console.WriteLine(message);
            Console.ReadLine();
        }
    }
}
