using System.Linq;
using Ninject;
using NUnit.Framework;

namespace ATZ.DependencyInjection.Tests
{
    [TestFixture]
    public class DependencyResolverShould
    {
        [SetUp]
        public void SetUp()
        {
            DependencyResolver.Initialize();
        }

        [Test]
        public void HaveANonNullInstanceByDefault()
        {
            Assert.IsNotNull(DependencyResolver.Instance);
        }

        [Test]
        public void ProperlyInitialize()
        {
            var kernel = DependencyResolver.Instance;
            DependencyResolver.Initialize();
            Assert.AreNotSame(kernel, DependencyResolver.Instance);
        }

        [Test]
        public void NinjectKernelIsUncapableOfContravariantInterfaceInjection()
        {
            var classInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(classInterface);
            Assert.Throws<ActivationException>(() => DependencyResolver.Instance.Get<IInterface<DerivedClass>>());
        }

        [Test]
        public void ReturnCorrectTypeForGetInterfaceIfRegisteredClass()
        {
            var classInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(classInterface);

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(classInterface, DependencyResolver.Instance.GetInterface<IInterface<BaseClass>>(interfaceType, typeof(BaseClass)));
        }

        [Test]
        public void ReturnCorrectTypeForGetInterfaceIfRegisteredBaseClass()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(baseClassInterface, DependencyResolver.Instance.GetInterface<IInterface<DerivedClass>>(interfaceType, typeof(DerivedClass)));
        }

        [Test]
        public void RegisterNewlyDiscoveredInterfaceTypeIfPreviouslyNotRegistered()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);
            Assert.AreEqual(0, DependencyResolver.Instance.GetBindings(typeof(IInterface<DerivedClass>)).Count());

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(baseClassInterface, DependencyResolver.Instance.GetInterface<IInterface<DerivedClass>>(interfaceType, typeof(DerivedClass)));
            Assert.AreEqual(1, DependencyResolver.Instance.GetBindings(typeof(IInterface<DerivedClass>)).Count());
        }

    }
}
