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
        public void ContainNinjectStyleErrorMessageWhenThrowingActivationException()
        {
            try
            {
                DependencyResolver.Instance.Get<IInterface<DerivedClass>>();
            }
            catch (ActivationException ex)
            {
                Assert.AreEqual(@"Error activating IInterface{DerivedClass}
No matching bindings are available, and the type is not self-bindable.
Activation path:
  1) Request for IInterface{DerivedClass}

Suggestions:
  1) Ensure that you have defined a binding for IInterface{DerivedClass}.
  2) If the binding was defined in a module, ensure that the module has been loaded into the kernel.
  3) Ensure you have not accidentally created more than one kernel.
  4) If you are using constructor arguments, ensure that the parameter name matches the constructors parameter name.
  5) If you are using automatic module loading, ensure the search path and filters are correct.
", ex.Message);
            }
        }

        [Test]
        public void ReturnCorrectTypeForGetInterfaceIfRegisteredClass()
        {
            var classInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(classInterface);

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(classInterface,
                DependencyResolver.Instance.GetInterface<IInterface<BaseClass>>(interfaceType, typeof(BaseClass)));
        }

        [Test]
        public void ReturnCorrectTypeForGetInterfaceIfRegisteredBaseClass()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(baseClassInterface,
                DependencyResolver.Instance.GetInterface<IInterface<DerivedClass>>(interfaceType, typeof(DerivedClass)));
        }

        [Test]
        public void RegisterNewlyDiscoveredInterfaceTypeIfPreviouslyNotRegistered()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);
            Assert.AreEqual(0, DependencyResolver.Instance.GetBindings(typeof(IInterface<DerivedClass>)).Count());

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(baseClassInterface,
                DependencyResolver.Instance.GetInterface<IInterface<DerivedClass>>(interfaceType, typeof(DerivedClass)));
            Assert.AreEqual(1, DependencyResolver.Instance.GetBindings(typeof(IInterface<DerivedClass>)).Count());
        }

        [Test]
        public void ThrowActivationExceptionIfContravariantInterfaceCannotBeResolved()
        {
            Assert.Throws<ActivationException>(
                () =>
                    DependencyResolver.Instance.GetInterface<IInterface<BaseClass>>(typeof(IInterface<>),
                        typeof(BaseClass)));
        }

        [Test]
        public void ThrowActivationExceptionForContravariantInterfaceWithProperMessage()
        {
            try
            {
                DependencyResolver.Instance.GetInterface<IInterface<BaseClass>>(typeof(IInterface<>), typeof(DerivedClass));
                Assert.Fail("ActivationException not thrown.");
            }
            catch (ActivationException ex)
            {
                Assert.AreEqual(@"Error activating IInterface{in DerivedClass}
No matching contravariant bindings are available, and the type is not self-bindable.
Activation path:
  1) Looking for bindings of IInterface{in Object}, found none.
  2) Looking for bindings of IInterface{in BaseClass}, found none.
  3) Request for IInterface{in DerivedClass}, no bindings found.

Suggestions:
  1) Ensure that you have defined a contravariant binding for IInterface with type parameter of DerivedClass or one of its base class.
", ex.Message);
            }
        }

        // TODO: Check what if generic interface type parameter is not marked as contravariant.
    }
}
