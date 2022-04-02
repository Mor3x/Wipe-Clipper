using System;

namespace WipeClipperUtils {
    public static class Logger {
        public delegate void Message(string message);
        public static Message Log;

        public static void Debug(string message) {
            Log?.Invoke($"[Debug] {message}");
        }

        public static void Error(string message, Exception e) {
            Log?.Invoke($"[Error] {message} - {e}");
        }

        public static void Error(string message) {
            Log?.Invoke($"[Error] {message}");
        }
    }
}
