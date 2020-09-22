using Charge.Activity.Service.Bussines.Interfaces;
using Charge.Activity.Service.Factories;
using Charge.Activity.Service.Repository;
using Charge.Activity.Service.Repository.Entity;
using Charge.Activity.Service.Repository.Entity.Models;
using NSubstitute;
using System;

namespace Charge.Activity.Service.Controller.Test.mocks {
    public class RepositoriesFactoryMock {
        public static RepositoriesFactory Instance { get; }

        static RepositoriesFactoryMock() {
            Instance = Substitute.For<RepositoriesFactory>();
        }

        public static void CreateAddRepository(IChargeActivityRepository repository) {
            Instance.GetRespository().Returns(repository);
        }


        ChargesContext chargesContext;
        public RepositoriesFactoryMock(ChargesContext chargesContext) {
            this.chargesContext = chargesContext;
        }
        public IChargeActivityRepository GetRespository() {
            return new ChargeActivityServiceRepository(chargesContext);
        }
    }
}
