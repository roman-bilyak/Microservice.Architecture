﻿using CommandLine;

namespace Microservice.Performance.Tests
{
    internal class CommandLineOptions
    {
        [Option('u', "url", Required = true, HelpText = "Urls to be processed.")]
        public IEnumerable<string> Urls { get; set; } = new List<string>();

        [Option('t', "timeout", Default = 5, Required = false, HelpText = "Step timeout (seconds).")]
        public int Timeout { get; set; }

        [Option('c', "config", Required = false, HelpText = "Configuration file.")]
        public string Config { get; set; } = string.Empty;
    }
}