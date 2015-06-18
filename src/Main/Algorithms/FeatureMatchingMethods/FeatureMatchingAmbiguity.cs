using USC.GISResearchLab.Common.Addresses;

namespace USC.GISResearchLab.Geocoding.Core.Algorithms.FeatureMatchingMethods
{
    public class FeatureMatchingAmbiguity
    {

        #region Properties

        public FeatureMatchingAmbiguityType FeatureMatchingAmbiguityType { get; set; }
        public AddressComponents AddressComponent { get; set; }

        #endregion



        public FeatureMatchingAmbiguity()
        {

        }


        public override string ToString()
        {
            return AddressComponent.ToString() + "-" + FeatureMatchingAmbiguityType.ToString();
        }

        public override bool Equals(object obj)
        {
            bool ret = false;
            if (obj != null)
            {
                if (obj.GetType() == typeof(FeatureMatchingAmbiguity))
                {
                    FeatureMatchingAmbiguity b = (FeatureMatchingAmbiguity)obj;

                    if (b.AddressComponent == this.AddressComponent && b.FeatureMatchingAmbiguityType == this.FeatureMatchingAmbiguityType)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public override int GetHashCode()
        {
            return (int)FeatureMatchingAmbiguityType + (int)AddressComponent;
        }
    }
}
