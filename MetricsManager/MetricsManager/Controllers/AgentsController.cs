using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly List<AgentInfo> _registeredAgents;
        private readonly ILogger<AgentsController> _logger;
        public AgentsController(List<AgentInfo> registeredAgents, ILogger<AgentsController> logger)
        {
            _registeredAgents = registeredAgents;
            _logger = logger;
            _logger.LogDebug(1, "AgentController created");
        }

        /// <summary>
        /// Регистрирует нового агента
        /// </summary>
        /// <param name="agentInfo">Принимает в качестве параметра объект AgentInfo из тела сообщения</param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _logger.LogTrace($"Agent registered with params: AgentID={agentInfo.AgentId}, AgentAdress={agentInfo.AgentAddress}");
            return Ok();
        }

        /// <summary>
        /// Активирует агента по указанному ID
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns></returns>
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"Agent enabled: AgentID={agentId}");
            return Ok();
        }

        /// <summary>
        /// Деактивирует агента по указанному ID
        /// </summary>
        /// <param name="agentId">ID агента</param>
        /// <returns></returns>
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogTrace($"Agent disabled: AgentID={agentId}");
            return Ok();
        }
    }
}

