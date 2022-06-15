using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;

namespace Genzai.WebCore.Test.Data
{
    /// <summary>
    /// Class <c>SampleTestingDataset</c> provides datasets to perform unit tests with.
    /// </summary>
    internal static class SampleTestingDataset
    {

        public static List<object> ListDefaultDataset()
        {



            return new List<object> {

               
                // Sample
                new Sample(1)
                {
                    Name = "sample1",
                    SubSampleId = 1,
                },
                new Sample(2)
                {
                    Name = "sample2",
                    SubSampleId = 2,
                }

            };

        }
    }
}