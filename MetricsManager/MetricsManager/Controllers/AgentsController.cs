using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly List<AgentInfo> _registeredAgents;
        public AgentsController(List<AgentInfo> registeredAgents)
        {
            _registeredAgents = registeredAgents;
        }

        /// <summary>
        /// Регистрирует нового агента
        /// </summary>
        /// <param name="agentInfo">Принимает в качестве параметра объект AgentInfo из тела сообщения</param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
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
            return Ok();
        }
    }
}

