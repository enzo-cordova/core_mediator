using Genzai.WebCore.Attributes;
using Genzai.WebCore.Test.Mock.Attribute;
using Genzai.WebCore.Utils;
using System.Reflection;
using Xunit;

namespace Genzai.WebCore.Test.Utils;

public class AttributeUtilsTest
{

    [Fact]
    public void HaveAttributeTest()
    {
        MethodInfo test1 = typeof(AttributeTest).GetMethod("Test1");
        MethodInfo test2 = typeof(AttributeTest).GetMethod("Test2");
        Assert.True(AttributeUtils.HaveAttribute<Error400Attribute>(test1));
        Assert.False(AttributeUtils.HaveAttribute<Error400Attribute>(test2));
    }

}
