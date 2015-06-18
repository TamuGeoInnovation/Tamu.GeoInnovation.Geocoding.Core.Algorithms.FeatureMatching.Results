using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using USC.GISResearchLab.Common.Addresses;
using USC.GISResearchLab.Common.Core.Geocoders.FeatureMatching;
using USC.GISResearchLab.Geocoding.Core.Algorithms.TieHandlingMethods;
using USC.GISResearchLab.Geocoding.Core.Metadata.FeatureMatchingResults;
using USC.GISResearchLab.Geocoding.Core.Metadata.Qualities;
using USC.GISResearchLab.Geocoding.Core.Metadata.Statistics.ReferenceDatasets;
//using USC.GISResearchLab.Geocoding.Core.Metadata.Statistics.ReferenceDatasets;

namespace USC.GISResearchLab.Geocoding.Core.Algorithms.FeatureMatchingMethods
{
    public class FeatureMatchingResult
    {
        #region Properties

        public ReferenceDatasetStatistics ReferenceDatasetStatistics { get; set; }
        public FeatureMatchingResultType FeatureMatchingResultType { get; set; }
        public List<FeatureMatchingAmbiguity> AmbiguityTypes { get; set; }
        public List<AddressComponents> AmbiguousAddressComponents { get; set; }
        public string FeatureMatchingNotes { get; set; }
        public string FeatureMatchingTieNotes { get; set; }
        public TieHandlingStrategyType TieHandlingStrategyType { get; set; }
        public int FeatureMatchingResultCount { get; set; }
        public MatchedLocationTypes MatchedLocationTypes { get; set; }
        public FeatureMatchingGeographyType FeatureMatchingGeographyType { get; set; }
        public string Error { get; set; }
        public TimeSpan TimeTaken { get; set; }

        [XmlIgnore]
        public Exception Exception { get; set; }
        public bool ExceptionOccurred { get; set; }
        //public MatchedFeature MatchedFeature { get; set; }
        public List<MatchedFeature> MatchedFeatures { get; set; }

        #endregion

        public FeatureMatchingResult()
        {
            FeatureMatchingNotes = "";
            MatchedFeatures = new List<MatchedFeature>();
        }
    }
}
