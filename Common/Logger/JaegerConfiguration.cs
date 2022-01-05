using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using System;

namespace Common.Logger
{
    public static class JaegerConfiguration
    {
        public static void AddJaeger(this IServiceCollection services, string servicesName)
        {
            // Use "OpenTracing.Contrib.NetCore" to automatically generate spans for ASP.NET Core, Entity Framework Core, ...
            // See https://github.com/opentracing-contrib/csharp-netcore for details.
            services.AddOpenTracing();

            //// Adds the Jaeger Tracer.
            //services.AddSingleton<ITracer>(serviceProvider =>
            //{
            //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //    //// This is necessary to pick the correct sender, otherwise a NoopSender is used!
            //    //Jaeger.Configuration.SenderConfiguration.DefaultSenderResolver = new SenderResolver(loggerFactory)
            //    //    .RegisterSenderFactory<ThriftSenderFactory>();
            //    var config = Jaeger.Configuration.FromEnv(loggerFactory);

            //    //// This will log to a default localhost installation of Jaeger.
            //    var tracer = config.GetTracer();

            //    // Allows code that can't use DI to also access the tracer.
            //    if (!GlobalTracer.IsRegistered())
            //    {
            //        GlobalTracer.Register(tracer);
            //    }

            //    return tracer;
            //});

            services.AddSingleton<ITracer>(sp =>
            {
                //var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                //var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                //var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender())
                //    .Build();
                //var tracer = new Tracer.Builder(serviceName)
                //    // The constant sampler reports every span.
                //    //.WithSampler(new ConstSampler(true))
                //    // LoggingReporter prints every reported span to the logging framework.
                //    .WithReporter(reporter)
                //    .Build();

                Environment.SetEnvironmentVariable("JAEGER_SERVICE_NAME", sp.GetRequiredService<IWebHostEnvironment>().ApplicationName);
                Environment.SetEnvironmentVariable("JAEGER_AGENT_HOST", "35.213.167.46");
                Environment.SetEnvironmentVariable("JAEGER_AGENT_PORT", "5775");
                Environment.SetEnvironmentVariable("JAEGER_SAMPLER_TYPE", "const");

                var loggerFactory = new LoggerFactory();

                var config = Jaeger.Configuration.FromEnv(loggerFactory);
                var tracer = config.GetTracer();

                if (!GlobalTracer.IsRegistered())
                {
                    // Allows code that can't use DI to also access the tracer.
                    GlobalTracer.Register(tracer);
                }

                return tracer;
            });


            services.Configure<AspNetCoreDiagnosticOptions>(options =>
            {
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/status"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/metrics"));
                options.Hosting.IgnorePatterns.Add(context => context.Request.Path.Value.StartsWith("/swagger"));
            });
        }
    }
}
