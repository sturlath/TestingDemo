using Serilog;
using System;
using System.Runtime.CompilerServices;

namespace TestingDemo.Common.Exceptions
{
    public static class ExceptionHelper
    {
        public static void LogError(Exception ex, [CallerMemberName] string callerName = "")
        {
            Log.Logger.Error("{callerName} caught an exception - {@Exception}", callerName, ex);
        }

        public static void LogError(string message, object obj, [CallerMemberName] string callerName = "")
        {
            Log.Logger.Error("Error in {callerName}: {message} Data: {@data}", callerName, message, obj);
        }

        public static void LogError(string message, [CallerMemberName] string callerName = "")
        {
            Log.Logger.Error("Error in {callerName}: {message}.", callerName, message);
        }
    }
}
