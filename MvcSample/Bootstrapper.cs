using System;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcSample.Models;
using Nest;
using Unity.Mvc4;

namespace MvcSample
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        var settings = new ConnectionSettings(new Uri(ConfigurationManager.AppSettings["FacetflowUrl"]))
                                .SetDefaultIndex(ConfigurationManager.AppSettings["FacetflowIndexName"]);
        var client = new ElasticClient(settings);
        
        // create the index and set the Category field to not be analyzed to retain the casing
        if (!client.IndexExists(settings.DefaultIndex).ConnectionStatus.Success)
        {
            var result = client.CreateIndex(settings.DefaultIndex, 
                c => c.AddMapping<Post>(
                        m => m.Properties(p => 
                                          p.String(s => 
                                                   s.Name(o => o.Category)
                                                    .Index(FieldIndexOption.not_analyzed)))));

            if (!result.ConnectionStatus.Success)
            {
                throw new ApplicationException(string.Format("Could not create the default index because of '{0}'.", result.ConnectionStatus.Result));
            }
        }

        container.RegisterInstance<IElasticClient>(client);
    }
  }
}