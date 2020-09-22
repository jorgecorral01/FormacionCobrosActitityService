using Charge.Activity.Service.Action;
using System;

namespace Charge.Activity.Service.Factories {
    public  class ActionFactory {
         RepositoriesFactory repositoriesFactory;

        public ActionFactory() {
        }

        public ActionFactory(RepositoriesFactory repositoriesFactory) {
            this.repositoriesFactory = repositoriesFactory;
        }
        public virtual UpdateActivityAction GetUpdateActivityAction() {
            return new UpdateActivityAction(repositoriesFactory.GetRespository());
        }
    }
}
