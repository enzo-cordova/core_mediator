using System.Collections;

namespace Genzai.WebCore.Test
{
    public abstract class BaseGeneratorTest : IEnumerable<object[]>
    {
        protected abstract List<object[]> GetData();

        public IEnumerator<object[]> GetEnumerator()
        {
            return this.GetData().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
