﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nimbus.IntegrationTests.InfrastructureContracts;
using Nimbus.IntegrationTests.Tests.MulticastRequestResponseTests.MessageContracts;
using NUnit.Framework;
using Shouldly;

namespace Nimbus.IntegrationTests.Tests.MulticastRequestResponseTests
{
    [TestFixture]
    public class WhenSendingATwoSecondMulticastRequest : TestForAllBuses
    {
        private IEnumerable<BlackBallResponse> _response;

        public override async Task When(ITestHarnessBusFactory busFactory)
        {
            var bus = busFactory.Create();

            var request = new BlackBallRequest
                          {
                              ProspectiveMemberName = "Fred Flintstone",
                          };

            _response = await bus.MulticastRequest(request, TimeSpan.FromSeconds(2));
        }

        [Test]
        [TestCaseSource("AllBusesTestCases")]
        public async void WeShouldReceiveTwoResponses(ITestHarnessBusFactory busFactory)
        {
            await When(busFactory);

            _response.Count().ShouldBe(2);
        }
    }
}