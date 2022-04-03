using System;
using System.IO;

namespace DirectoryCleanup.Logger
{
    public class SimpleLogger : ILogger, IDisposable
    {
        private readonly StreamWriter _streamWriter;

        public event EventHandler<EventArgs> LoggerReconfigured;

        public string Name { get; }
        public bool IsTraceEnabled { get; }
        public bool IsDebugEnabled { get; }
        public bool IsInfoEnabled { get; }
        public bool IsWarnEnabled { get; }
        public bool IsErrorEnabled { get; }
        public bool IsFatalEnabled { get; }

        public SimpleLogger(string logFileName)
        {
            _streamWriter = new StreamWriter(logFileName);
        }

        public bool IsEnabled(LoggingLevel level)
        {
            throw new NotImplementedException();
        }

        public void Log<T>(LoggingLevel level, T value)
        {
            throw new NotImplementedException();
        }

        public void Log<T>(LoggingLevel level, IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void LogException(LoggingLevel level, string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingLevel level, IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingLevel level, string message)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingLevel level, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument>(LoggingLevel level, IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument>(LoggingLevel level, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument1, TArgument2>(LoggingLevel level, IFormatProvider formatProvider, string message,
           TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument1, TArgument2>(LoggingLevel level, string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LoggingLevel level, IFormatProvider formatProvider, string message,
           TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LoggingLevel level, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Trace<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void TraceException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Trace(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message)
        {
            throw new NotImplementedException();
        }

        public void Trace(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Debug<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void DebugException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Debug(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Info<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void InfoException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Info(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Info<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Warn<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Warn<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void WarnException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Warn(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Error<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void ErrorException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Error<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Fatal<T>(T value)
        {
            throw new NotImplementedException();
        }

        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            throw new NotImplementedException();
        }

        public void FatalException(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(IFormatProvider formatProvider, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument>(string message, TArgument argument)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument1, TArgument2>(string message, TArgument1 argument1, TArgument2 argument2)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, string message, TArgument1 argument1,
           TArgument2 argument2, TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(string message, TArgument1 argument1, TArgument2 argument2,
           TArgument3 argument3)
        {
            throw new NotImplementedException();
        }

        public void SetLogLevel(string levelName, string ruleName = null)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _streamWriter?.Dispose();
        }
    }
}