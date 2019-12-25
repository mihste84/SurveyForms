using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Common.Behaviours;
using System.Collections.Generic;
using System.Reflection;
using SurveyForms.Application.FormAreas.Commands;

namespace SurveyForms.Application
{
    public class ApplicationStartup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(CreateFormAreaCommand).Assembly;
            services.AddMediatR(assembly);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuditTrailBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));

            var assembliesToRegister = new List<Assembly>() { assembly };
            AssemblyScanner.FindValidatorsInAssemblies(assembliesToRegister).ForEach(pair => {
                services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType));
            });
        }
    }
}
