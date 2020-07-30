﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAndTwitch {
    public static class Logger {
        public delegate void BotMessage(string message);
        public static BotMessage Log;

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
