﻿using System;
using FubuMVC.Core.ServiceBus;
using FubuMVC.Core.ServiceBus.Monitoring;
using FubuMVC.Tests.TestSupport;
using Xunit;
using Rhino.Mocks;
using Shouldly;

namespace FubuMVC.Tests.ServiceBus.Monitoring
{
    
    public class PersistentTaskAgentTester : InteractionContext<PersistentTaskAgent>
    {
        [Fact]
        public void assert_available_happy()
        {
            ClassUnderTest.AssertAvailable().Wait();

            MockFor<IPersistentTask>().AssertWasCalled(x => x.AssertAvailable());
        }

        [Fact]
        public void assert_available_sad_path()
        {
            var ex = new DivideByZeroException();

            MockFor<IPersistentTask>().Stub(x => x.AssertAvailable())
                .Throw(ex);

            var task = ClassUnderTest.AssertAvailable();

                task.Wait();



            task.Result.ShouldBe(HealthStatus.Error);
        }

        [Fact]
        public void activate_happy_path()
        {
            ClassUnderTest.Activate().Wait();
            MockFor<IPersistentTask>().AssertWasCalled(x => x.Activate());
        }

        [Fact]
        public void deactivate_happy_path()
        {
            ClassUnderTest.Deactivate().Wait();
            MockFor<IPersistentTask>().AssertWasCalled(x => x.Deactivate());
        }

        [Fact]
        public void assign_peers()
        {
            var peers = Services.CreateMockArrayFor<ITransportPeer>(4);

            var peer = peers[2];

            MockFor<IPersistentTask>().Stub(x => x.SelectOwner(peers))
                .Return(peer.ToCompletionTask());

            var task = ClassUnderTest.AssignOwner(peers);
            task.Wait();

            task.Result.ShouldBeTheSameAs(peer);
        }
    }
}