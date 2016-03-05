using DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
