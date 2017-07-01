using NUnit.Framework;
using System;
using System.Linq;


namespace ATZ.DependencyInjection.Tests
{
    [TestFixture]
    public class DependencyResolverShould
    {
        [SetUp]
        public void SetUp()
        {
            DependencyResolver.Initialize(new NinjectStandardKernel());
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
            DependencyResolver.Initialize(new NinjectStandardKernel());
            Assert.AreNotSame(kernel, DependencyResolver.Instance);
        }

        [Test]
        public void NinjectKernelIsUncapableOfContravariantInterfaceInjection()
        {
            var classInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(classInterface);
            Assert.Throws<Ninject.ActivationException>(() => DependencyResolver.Instance.Get<IInterface<DerivedClass>>());
        }

        [Test]
        public void ContainNinjectStyleErrorMessageWhenThrowingActivationException()
        {
            var ex =
                Assert.Throws<Ninject.ActivationException>(() => DependencyResolver.Instance.Get<IInterface<DerivedClass>>());

            Assert.IsNotNull(ex);
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
            var ex = Assert.Throws<ActivationException>(() => DependencyResolver.Instance.GetInterface<IInterface<BaseClass>>(typeof(IInterface<>), typeof(DerivedClass)));

            Assert.IsNotNull(ex);
            Assert.AreEqual(@"Error activating IInterface{in DerivedClass}
No matching contra-variant bindings are available, and the type is not self-bindable.
Activation path:
  1) Looking for bindings of IInterface{in Object}, found none.
  2) Looking for bindings of IInterface{in BaseClass}, found none.
  3) Request for IInterface{in DerivedClass}, no bindings found.

Suggestions:
  1) Ensure that you have defined a contra-variant binding for IInterface with type parameter of DerivedClass or one of its base class.
", ex.Message);
        }

        [Test]
        public void ThrowAnExceptionIfInterfaceWithArgumentDoesNotExistAndTheInterfaceIsNonContravariantEvenIfInterfaceIsRegisteredForTheArgumentBaseClass()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<INonContravariantInterface<BaseClass>>().ToConstant(baseClassInterface);

            var interfaceType = typeof(INonContravariantInterface<>);
            Assert.Throws<ActivationException>(
                () =>
                    DependencyResolver.Instance.GetInterface<INonContravariantInterface<DerivedClass>>(
                        interfaceType, typeof(DerivedClass)));
        }

        [Test]
        public void ThrowActivationExceptionForNonContravariantInterfaceWithProperMessage()
        {
            var ex = Assert.Throws<ActivationException>(() => DependencyResolver.Instance.GetInterface<INonContravariantInterface<BaseClass>>(typeof(INonContravariantInterface<>), typeof(BaseClass)));

            Assert.IsNotNull(ex);
            Assert.AreEqual(@"Error activating INonContravariantInterface{BaseClass}
No matching contra-variant bindings are available, and the type is not self-bindable.
Activation path:
  1) Request for INonContravariantInterface{BaseClass}, no bindings found.

Suggestions:
  1) Ensure that you have defined a contra-variant binding for INonContravariantInterface with type parameter of BaseClass or one of its base class.
", ex.Message);
        }

        [Test]
        public void NotSupportMultipleTemplateArgumentsAtTheMoment()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => DependencyResolver.Instance.GetInterface<IMultiParameterInterface<BaseClass, BaseClass>>(typeof(IMultiParameterInterface<,>), typeof(BaseClass)));

            Assert.IsNotNull(ex);
            Assert.AreEqual(@"Error activating IMultiParameterInterface.
At the moment, multiple generic parameters are not supported by this method.
Parameter name: interfaceType", ex.Message);
        }

        [Test]
        public void AllowContravariantInterfaceResolutionWithoutTypeCastingToAllowDebuggingOfCastingProblems()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var interfaceType = typeof(IInterface<>);
            Assert.AreSame(baseClassInterface,
                DependencyResolver.Instance.GetInterface(interfaceType, typeof(DerivedClass)));
        }

        [Test]
        public void NonGenericInterfaceThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => DependencyResolver.Instance.GetInterface(typeof(INonGenericInterface), typeof(BaseClass)));
            Assert.IsNotNull(ex);
            Assert.AreEqual("interfaceType", ex.ParamName);
        }

        [Test]
        public void ThrowProperActivationExceptionWhenActivatedObjectCannotBeCastedToRequestedInterface()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var ex = Assert.Throws<ActivationException>(() => DependencyResolver.Instance.GetInterface<IInterface<object>>(typeof(IInterface<>), typeof(BaseClass)));
            Assert.IsNotNull(ex);
            Assert.AreEqual("Activated object of type IInterface{in BaseClass} cannot be casted to expected return type ATZ.DependencyInjection.Tests.IInterface`1[System.Object]!", ex.Message);
        }

        [Test]
        public void GetInterfaceThrowsArgumentNullExceptionIfKernelIsNull()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var ex =
                Assert.Throws<ArgumentNullException>(
                    () => DependencyResolver.GetInterface(null, typeof(IInterface<>), typeof(BaseClass)));
            Assert.IsNotNull(ex);
            Assert.AreEqual("kernel", ex.ParamName);
        }

        [Test]
        public void GetInterfaceThrowsArgumentNullExceptionIfInterfaceTypeIsNull()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var ex =
                Assert.Throws<ArgumentNullException>(
                    () => DependencyResolver.Instance.GetInterface(null, typeof(BaseClass)));
            Assert.IsNotNull(ex);
            Assert.AreEqual("interfaceType", ex.ParamName);
        }

        [Test]
        public void GetInterfaceThrowsArgumentNullExceptionIfInterfaceArgumentIsNull()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var ex =
                Assert.Throws<ArgumentNullException>(
                    () => DependencyResolver.Instance.GetInterface(typeof(IInterface<>), null));
            Assert.IsNotNull(ex);
            Assert.AreEqual("interfaceArgument", ex.ParamName);
        }

        [Test]
        public void GetInterfaceTemplateThrowsArgumentNullExceptionIfInterfaceTypeIsNull()
        {
            var baseClassInterface = new Template<BaseClass>();
            DependencyResolver.Instance.Bind<IInterface<BaseClass>>().ToConstant(baseClassInterface);

            var ex =
                Assert.Throws<ArgumentNullException>(
                    () => DependencyResolver.Instance.GetInterface<Template<BaseClass>>(null, typeof(BaseClass)));
            Assert.IsNotNull(ex);
            Assert.AreEqual("interfaceType", ex.ParamName);
        }

    }
}
