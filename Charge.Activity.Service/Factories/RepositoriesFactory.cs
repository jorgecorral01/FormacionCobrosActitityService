using Charge.Activity.Service.Bussines.Interfaces;
using Charge.Activity.Service.Repository;
using Charge.Activity.Service.Repository.Entity;
using Charge.Activity.Service.Repository.Entity.Models;
using System;

namespace Charge.Activity.Service.Factories {
    public class RepositoriesFactory {
        ChargesContext chargesContext;
        public RepositoriesFactory (ChargesContext chargesContext) {
            this.chargesContext = chargesContext;
        }
        public IChargeActivityRepository GetRespository() {            
            return new ChargeActivityServiceRepository(chargesContext);
        }        
    }
}
