using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Localization;

namespace Sitefinity.Sample.LeadScoreCriteria
{
    [ObjectInfo(typeof(LeadScorePersonalizationResources), Title = "LeadScorePersonalizationResources", Description = "LeadScorePersonalizationResourcesDescription")]
    public class LeadScorePersonalizationResources : Resource
    {
        public LeadScorePersonalizationResources()
        {
        }

        [ResourceEntry("LeadScorePersonalizationResources",
             Value = "LeadScorePersonalizationResources",
             Description = "LeadScorePersonalizationResources",
             LastModified = "2014/05/23")]
        public string LeadScorePersonalizationResourcesTitle
        {
            get { return this["LeadScorePersonalizationResourcesTitle"]; }
        }

        [ResourceEntry("LeadScorePersonalizationResourcesDescription",
             Value = "LeadScorePersonalizationResourcesDescription",
             Description = "LeadScorePersonalizationResourcesDescription",
             LastModified = "2014/05/23")]
        public string LeadScorePersonalizationResourcesDescription
        {
            get { return this["LeadScorePersonalizationResourcesDescription"]; }
        }

        /// <summary>
        /// Gets the lead score criterion.
        /// </summary>
        /// <value>The lead score criterion.</value>
        [ResourceEntry("LeadScoreCriterion", Value = "Lead score", Description = "word: Lead score", LastModified = "2017/02/02")]
        public string LeadScoreCriterion
        {
            get
            {
                return base["LeadScoreCriterion"];
            }
        }



        /// <summary>
        /// Gets the PersonaCriterionTitle label.
        /// </summary>
        /// <value>The PersonaCriterionTitle label.</value>
        [ResourceEntry("LeadScoreCriterionTitle", Value = "Lead score (from Digital Experience Cloud)", Description = "Label: Lead score (from Digital Experience Cloud)", LastModified = "2017/02/02")]
        public string LeadScoreCriterionTitle
        {
            get
            {
                return base["LeadScoreCriterionTitle"];
            }
        }

        /// <summary>
        /// Gets the persona criterion description.
        /// </summary>
        /// <value>The persona criterion description.</value>
        [ResourceEntry("LeadScoreCriterionDescription", Value = "Lead scores created in Digital Experience Cloud are added as a criteria in page personalization", Description = "phrase: Lead scores created in Digital Experience Cloud are added as a criteria in page personalization", LastModified = "2017/02/02")]
        public string LeadScoreCriterionDescription
        {
            get
            {
                return base["LeadScoreCriterionDescription"];
            }
        }

        /// <summary>
        /// Gets there are no lead scores created yet label.
        /// </summary>
        /// <value>There are no lead scores created yet.</value>
        [ResourceEntry("NoLeadScoresFound", Value = "There are no lead scores created yet.", Description = "Label: There are no lead scores created yet.", LastModified = "2014/09/12")]
        public string NoLeadScoresFound
        {
            get
            {
                return base["NoLeadScoresFound"];
            }
        }

        /// <summary>
        /// Gets the Connection to Digital Experience Cloud failed label
        /// </summary>
        /// <value>Connection to Digital Experience Cloud failed</value>
        [ResourceEntry("UnableToConnect", Value = "Connection to Digital Experience Cloud failed", Description = "Label: Connection to Digital Experience Cloud failеd", LastModified = "2014/09/12")]
        public string UnableToConnect
        {
            get
            {
                return base["UnableToConnect"];
            }
        }
    }
}
