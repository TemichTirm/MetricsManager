using MetricsManager;
using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MetricsManagerTests
{
    public class AgentsControllerUnitTest
    {
        private readonly AgentsController controller;
        private readonly Mock<ILogger<AgentsController>> mockLogger;
        private readonly List<AgentInfo> agents = new();

        public AgentsControllerUnitTest()
        {
            mockLogger = new Mock<ILogger<AgentsController>>();
            agents.Add(new AgentInfo());
            controller = new AgentsController(agents, mockLogger.Object);
        }

        [Fact]
        public void RegisterAgent_ReturnsOk()
        {  
            var result = controller.RegisterAgent(agents[0]);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void EnableAgentById_ReturnsOk()
        {
            var result = controller.EnableAgentById(agents[0].AgentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void DisableAgentById_ReturnsOk()
        {
            var result = controller.DisableAgentById(agents[0].AgentId);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}

