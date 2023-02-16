﻿
using many_buttons.Services;
using many_buttons.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace many_buttons
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        private static IHost __Host;

        public static IHost Host => __Host ??= Microsoft.Extensions.Hosting.Host
           .CreateDefaultBuilder(Environment.GetCommandLineArgs())
           .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("appsettings.json", true, true))
           .ConfigureServices((host, services) => services
               .AddViews()
               .AddServices()
                )
           .Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using var host = Host;
            await host.StopAsync();
        }
    }
}
