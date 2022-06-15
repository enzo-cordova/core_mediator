using Genzai.Core.Domain.Model;

namespace Genzai.WebCore.Test.Mock.Domain.Persistence.Model
{
    public class Sample : Entity<Sample, long>
    {
        public Sample() { }
        public Sample(long id)
        {
            Id = id;
        }

        public string Name { get; set; }

        public long SubSampleId { get; set; }
    }
}
