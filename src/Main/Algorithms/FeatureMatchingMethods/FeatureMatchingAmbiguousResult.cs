using System;
using System.Collections.Generic;
using USC.GISResearchLab.Common.Addresses;
using USC.GISResearchLab.Common.Core.Geocoders.FeatureMatching;

namespace USC.GISResearchLab.Geocoding.Core.Algorithms.FeatureMatchingMethods
{

    public class FeatureMatchingAmbiguousResult
    {
        public List<FeatureMatchingAmbiguity> DetermineAmbiguityType(StreetAddress inputAddress, List<MatchedFeature> matchedFeatures)
        {

            List<FeatureMatchingAmbiguity> ret = new List<FeatureMatchingAmbiguity>();

            try
            {
                MatchedFeature prev = matchedFeatures[0];
                for (int i = 1; i < matchedFeatures.Count; i++ )
                {
                    MatchedFeature current = matchedFeatures[i];
                    List<FeatureMatchingAmbiguity> ambiguities = DetermineAmbiguityType(inputAddress, prev, current);
                    foreach (FeatureMatchingAmbiguity ambiguity in ambiguities)
                    {
                        if (!ret.Contains(ambiguity))
                        {
                            ret.Add(ambiguity);
                        }
                    }
                    prev = current;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in DetermineAmbiguity: " + e.Message, e);
            }

            return ret;
        }

        public List<FeatureMatchingAmbiguity> DetermineAmbiguityType(StreetAddress inputAddress, MatchedFeature a, MatchedFeature b)
        {
            List<FeatureMatchingAmbiguity> ret = new List<FeatureMatchingAmbiguity>();
            try
            {
                if (a.MatchedFeatureAddress.Equals(b.MatchedFeatureAddress))
                {
                    FeatureMatchingAmbiguity ambiguity = new FeatureMatchingAmbiguity();
                    ambiguity.FeatureMatchingAmbiguityType = FeatureMatchingAmbiguityType.ReferenceFeatureMultipleInstances;
                    ret.Add(ambiguity);
                }
                else
                {
                    List<AddressComponents> differences = a.MatchedFeatureAddress.Difference(b.MatchedFeatureAddress);

                    if (differences.Count > 0)
                    {

                        foreach (AddressComponents difference in differences)
                        {
                            FeatureMatchingAmbiguity ambiguity = new FeatureMatchingAmbiguity();
                            ambiguity.AddressComponent = difference;


                            if (difference == AddressComponents.Number)
                            {
                                //don't include address number differences for now
                                // ambiguity.FeatureMatchingAmbiguityType = FeatureMatchingAmbiguityType.ReferenceFeatureOverlappingAddressRanges;
                            }
                            else
                            {
                                if (inputAddress.HasStreetAddressComponent(difference)) // the input address has the component
                                {
                                    ambiguity.FeatureMatchingAmbiguityType = FeatureMatchingAmbiguityType.InputAddressIncorrectMultiplePossibilities;
                                }
                                else // the input address does not have the component
                                {
                                    ambiguity.FeatureMatchingAmbiguityType = FeatureMatchingAmbiguityType.InputAddressIncompleteMultiplePossibilities;
                                }
                            }

                            ret.Add(ambiguity);
                        }
                    }
                    else
                    {
                        FeatureMatchingAmbiguity ambiguity = new FeatureMatchingAmbiguity();
                        ambiguity.AddressComponent = AddressComponents.Unknown;
                        ret.Add(ambiguity);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in DetermineAmbiguity: " + e.Message, e);
            }

            return ret;
        }

        public List<AddressComponents> DetermineAmbiguousAddressComponents(StreetAddress inputAddress, List<MatchedFeature> matchedFeatures)
        {

            List<AddressComponents> ret = new List<AddressComponents>();

            try
            {
                MatchedFeature prev = matchedFeatures[0];
                for (int i = 1; i < matchedFeatures.Count; i++)
                {
                    MatchedFeature current = matchedFeatures[i];
                    List<AddressComponents> ambiguities = DetermineAmbiguousAddressComponents(inputAddress, prev, current);
                    foreach (AddressComponents ambiguity in ambiguities)
                    {
                        if (!ret.Contains(ambiguity))
                        {
                            ret.Add(ambiguity);
                        }
                    }
                    prev = current;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in DetermineAmbiguousAddressComponents: " + e.Message, e);
            }

            return ret;
        }

        public List<AddressComponents> DetermineAmbiguousAddressComponents(StreetAddress inputAddress, MatchedFeature a, MatchedFeature b)
        {
            List<AddressComponents> ret = new List<AddressComponents>();
            try
            {
                if (a.MatchedFeatureAddress.Equals(b.MatchedFeatureAddress))
                {
                    ret.Add(AddressComponents.All);
                }
                else
                {
                    ret = a.MatchedFeatureAddress.Difference(b.MatchedFeatureAddress);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error in DetermineAmbiguousAddressComponent: " + e.Message, e);
            }

            return ret;
        }
    }
}
