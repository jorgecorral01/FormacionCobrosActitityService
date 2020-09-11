using Charge.Activity.Service.Bussines.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Charge.Activity.Service.Bussines.Interfaces {
    public interface IChargeActivityRepository {
        bool Add(string identifier);

        bool Update(IdentifierDto identifier);
    }
}
