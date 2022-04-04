using System;
using System.IO;

namespace VsCleanup.Logger
{
    public class SimpleLogger : ILogger, IDisposable
    {
        private readonly StreamWriter _streamWriter;

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

        public void Log(LoggingLevel level, string message)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
        }

        public void Info(string message)
        {
            _streamWriter.WriteLine(message);
            _streamWriter.Flush();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message)
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