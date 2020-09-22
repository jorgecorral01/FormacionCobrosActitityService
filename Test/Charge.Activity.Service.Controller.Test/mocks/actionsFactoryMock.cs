using Charge.Activity.Service.Action;
using Charge.Activity.Service.Factories;
using NSubstitute;
using System;

namespace Charge.Activity.Service.Controller.Test.mocks {
    public class ActionsFactoryMock {
        public static ActionFactory Instance { get; }

        static ActionsFactoryMock() {
            Instance = Substitute.For<ActionFactory>();
        }

        public static void CreateUpdateActivityAction(UpdateActivityAction action) {
            Instance.GetUpdateActivityAction().Returns(action);
        }        
    }
}
