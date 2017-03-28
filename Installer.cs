using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Personalization.Impl;
using Telerik.Sitefinity.Personalization.Impl.Configuration;
using Telerik.Sitefinity.Personalization.Impl.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Logging;
using Telerik.Sitefinity.Restriction;

namespace Sitefinity.Sample.LeadScoreCriteria
{
    public class Installer
    {
        public static void PreApplicationStart()
        {

            Bootstrapper.Initialized += (new EventHandler<ExecutedEventArgs>(Installer.Bootstrapper_Initialized));
        }

        private static void Bootstrapper_Initialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "Bootstrapped")
            {
                SystemManager.RunWithElevatedPrivilegeDelegate worker = new SystemManager.RunWithElevatedPrivilegeDelegate(CreateSampleWorker);
                SystemManager.RunWithElevatedPrivilege(worker);
            }
        }

        private static void CreateSampleWorker(object[] parameters)
        {
            if (!IsPersonalizationModuleInstalled())
            {
                return;
            }

            AddVirtualPathToEmbeddedRes();
            CreateCriteria();
            RegisterCriteria();
            RegisterService();
        }

        private static void RegisterService()
        {
            SystemManager.RegisterWebService(
                    typeof(LeadScoreCriteria.Services.LeadScoring),
                    "CustomServices/LeadScoring");
        }

        private static bool IsPersonalizationModuleInstalled()
        {
            return 
                SystemManager.ApplicationModules != null &&
                SystemManager.ApplicationModules.ContainsKey("Personalization") && //PersonalizationModule.ModuleName
                !(SystemManager.ApplicationModules["Personalization"] is InactiveModule);

        }

        private static void AddVirtualPathToEmbeddedRes()
        {
            //Register the resource file
            Res.RegisterResource<LeadScorePersonalizationResources>();
            var configManager = ConfigManager.GetManager();
            var virtualPathConfig = configManager.GetSection<VirtualPathSettingsConfig>();
            using (new UnrestrictedModeRegion())
            {
                if (!virtualPathConfig.VirtualPaths.Contains(virtualPath + "*"))
                {
                    var pathConfig = new VirtualPathElement(virtualPathConfig.VirtualPaths)
                    {
                        VirtualPath = Installer.virtualPath + "*",
                        ResolverName = "EmbeddedResourceResolver",
                        ResourceLocation = typeof(LeadScoreEvaluator).Assembly.GetName().Name
                    };
                    virtualPathConfig.VirtualPaths.Add(pathConfig);
                    configManager.SaveSection(virtualPathConfig);
                }
            }
        }

        private static void CreateCriteria()
        {
            var personalizationConfig = Config.Get<PersonalizationConfig>();
            if (!personalizationConfig.Criteria.Contains("LeadScoreCriterion"))
            {
                CriterionElement criterionElement = new CriterionElement(personalizationConfig.Criteria)
                {
                    Name = "LeadScoreCriterion",
                    Title = "LeadScoreCriterionTitle",
                    ResourceClassId = typeof(LeadScorePersonalizationResources).Name,
                    CriterionEditorUrl = "Sitefinity.Sample.LeadScoreCriteria.LeadScoreEditor.ascx",
                    ConsoleCriterionEditorUrl = "Sitefinity.Sample.LeadScoreCriteria.LeadScoreEditor.ascx",
                    CriterionVirtualPathPrefix = Installer.virtualPath
                };
                personalizationConfig.Criteria.Add(criterionElement);
            }
        }

        private static void RegisterCriteria()
        {
            ObjectFactory.Container.RegisterType<ICriterionEvaluator, LeadScoreEvaluator>("LeadScoreCriterion", new ContainerControlledLifetimeManager(), new InjectionMember[0]);
        }

        private static readonly string virtualPath = "~/SFCustomPersonalization/";
    }
}
