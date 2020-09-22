using Charge.Activity.Service.Action;
using Charge.Activity.Service.Bussines.Dtos;
using Charge.Activity.Service.Bussines.Interfaces;
using Charge.Activity.Service.Controller.Test.mocks;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.Language;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Charge.Activity.Service.Controllers.ChargeActivityController;

namespace Charge.Activity.Service.Controller.Test {
    [TestFixture]
    public class ChargeActivityServiceControllerShould {
        private HttpClient client;
        private IdentifierDto identifier;
       [SetUp]
        public void Setup() {
            client = TestFixture.HttpClient;
            identifier = new IdentifierDto { identifier = "any identifier", AddResult = true };
        }

        [Test]
        public async Task given_an_identifier_add_new_activity_charge_we_obtein_an_ok_response() {
            var requestUri = "http://localhost:10002/api/ChargeActivity/add";
            var content = GivenAHttpContent(identifier, requestUri);
            IChargeActivityRepository repository = GivenArepositoryMock();
            RepositoriesFactoryMock.CreateAddRepository(repository);
            ReturnTrueForRepository(repository);

            var result = await client.PostAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            repository.Received(1).Add(identifier.identifier);
        }

        [Test]
        public async Task given_an_identifier_for_update_activity_charge_we_obtein_an_ok_response() {
            var requestUri = "http://localhost:10002/api/ChargeActivity/update";
            var content = GivenAHttpContent(identifier, requestUri);
            UpdateActivityAction action = GivenAnUpdateActivityActionMock();
            ActionsFactoryMock.CreateUpdateActivityAction(action);
            ReturnTrueForActivityActionMock(action);

            var result = await client.PutAsync(requestUri, content);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
            action.Received(1).Execute(Arg.Is<IdentifierDto>(Item => Item.identifier == identifier.identifier && Item.AddResult == identifier.AddResult));
        }

        private void ReturnTrueForActivityActionMock(UpdateActivityAction action) {
            action.Execute(Arg.Is<IdentifierDto>(Item => Item.identifier == identifier.identifier && Item.AddResult == identifier.AddResult)).Returns(true);
        }

        private static UpdateActivityAction GivenAnUpdateActivityActionMock() {
            return Substitute.For<UpdateActivityAction>(new object[] { null });
        }

        private static HttpContent GivenAHttpContent(IdentifierDto identifierDto, string requestUri) {            
            string json = JsonConvert.SerializeObject(identifierDto, Formatting.Indented);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage {
                Method = HttpMethod.Post,
                RequestUri = new Uri(requestUri),
                Content = content
            };
            return content;
        }
        private void ReturnTrueForRepository(IChargeActivityRepository repository) {
            repository.Add(identifier.identifier).Returns(true);
        }
        private static IChargeActivityRepository GivenArepositoryMock() {
            return Substitute.For<IChargeActivityRepository>();
        }
    }
}