using NUnit.Framework;

namespace ATZ.DependencyInjection.Tests
{
    [TestFixture]
    public class DependencyResolverTests
    {
        [Test]
        public void KernelInstanceIsNotNull()
        {
            Assert.IsNotNull(DependencyResolver.Instance);
        }
    }
}
