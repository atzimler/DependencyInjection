using Ninject;
using NUnit.Framework;

namespace ATZ.DependencyInjection.Tests
{
    [TestFixture]
    public class Expectations
    {
        [Test]
        public void StandardKernelConstructorShouldNotCrash()
        {
            // ReSharper disable once ObjectCreationAsStatement => We are testing the constructor.
            Assert.DoesNotThrow(() => new StandardKernel());
        }
    }
}
