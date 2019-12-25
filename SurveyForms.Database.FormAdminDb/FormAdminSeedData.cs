using Microsoft.Extensions.DependencyInjection;
using SurveyForms.Application.Common.Interfaces.DataAccess;
using SurveyForms.Core.Domain.Common;
using SurveyForms.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyForms.Database.FormAdminDb
{
    public static class FormAdminSeedData
    {
        public async static Task SeedDevFormAreas(IServiceProvider provider)
        {
            var ctx = provider.GetRequiredService<IFormAdminDbContext>();

            if(!ctx.FormAreas.Any())
            {
                var areas = new List<FormArea>
                {
                    new FormArea
                    {
                        Name = "Test area 1",
                        Forms = new List<Form>
                        {
                            new Form { Name = "Test form 1", Description = "Description for first test form." },
                            new Form { 
                                Name = "Test form 2",
                                FormItems = new List<FormItem>
                                {
                                    new FormItem { Name = "Item 1" },
                                    new FormItem { Name = "Item 2" }
                                }
                            },
                            new Form {
                                Name = "Test form 3",
                                Description = "Description for third test form.",
                                FormItems = new List<FormItem>
                                {
                                    new FormItem { Name = "Item 1" },
                                    new FormItem { Name = "Item 2" }
                                }
                            },
                            new Form { Name = "Test form 4", Description = "Description for fourth test form." }

                        }
                    },
                    new FormArea
                    {
                        Name = "Test area 2",
                        Forms = new List<Form>
                        {
                            new Form { Name = "Test form 3", Description = "Description for third test form." }
                        }
                    }
                };

                await ctx.FormAreas.AddRangeAsync(areas);

                await ctx.SaveAuditableChangesAsync(DevAdmin.Username);
            }           
        }
    }
}
