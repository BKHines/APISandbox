using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSandboxAPI.Core
{
    public enum LogType
    {
        error,
        info,
        warn,
        debug
    }

    public class ApiSandboxLog
    {
        public string title { get; set; }
        public List<string> messages { get; set; }
    }

    public class ApiSandboxLogger
    {
        private readonly ILogger _logger;

        public ApiSandboxLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogMessage(LogType logType, ApiSandboxLog apisLog, Exception ex = null)
        {
            var currentTime = DateTime.Now;

            switch (logType)
            {
                case LogType.debug:
                    _logger.LogDebug("----------------------------------------------------------------------------------------------------------------------");
                    _logger.LogDebug($"{apisLog.title}");
                    _logger.LogDebug("----------------------------------------------------------------------------------------------------------------------");
                    apisLog.messages?.ForEach((m) => { _logger.LogDebug(m); });
                    if (apisLog.messages != null && apisLog.messages.Any())
                    {
                        _logger.LogDebug("----------------------------------------------------------------------------------------------------------------------");
                    }
                    if (ex != null)
                    {
                        _logger.LogDebug(ex, "Exception");
                        _logger.LogDebug("----------------------------------------------------------------------------------------------------------------------");
                    }
                    break;
                case LogType.error:
                    _logger.LogError("----------------------------------------------------------------------------------------------------------------------");
                    _logger.LogError($"{apisLog.title}");
                    _logger.LogError("----------------------------------------------------------------------------------------------------------------------");
                    apisLog.messages?.ForEach((m) => { _logger.LogError(m); });
                    if (apisLog.messages != null && apisLog.messages.Any())
                    {
                        _logger.LogError("----------------------------------------------------------------------------------------------------------------------");
                    }
                    if (ex != null)
                    {
                        _logger.LogError(ex, "Exception");
                        _logger.LogError("----------------------------------------------------------------------------------------------------------------------");
                    }
                    break;
                case LogType.info:
                    _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
                    _logger.LogInformation($"{apisLog.title}");
                    _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
                    apisLog.messages?.ForEach((m) => { _logger.LogInformation(m); });
                    if (apisLog.messages != null && apisLog.messages.Any())
                    {
                        _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
                    }
                    if (ex != null)
                    {
                        _logger.LogInformation(ex, "Exception");
                        _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
                    }
                    break;
                case LogType.warn:
                    _logger.LogWarning("----------------------------------------------------------------------------------------------------------------------");
                    _logger.LogWarning($"{apisLog.title}");
                    _logger.LogWarning("----------------------------------------------------------------------------------------------------------------------");
                    apisLog.messages?.ForEach((m) => { _logger.LogWarning(m); });
                    if (apisLog.messages != null && apisLog.messages.Any())
                    {
                        _logger.LogWarning("----------------------------------------------------------------------------------------------------------------------");
                    }
                    if (ex != null)
                    {
                        _logger.LogWarning(ex, "Exception");
                        _logger.LogWarning("----------------------------------------------------------------------------------------------------------------------");
                    }
                    break;
            }
        }

        public void LogInfoObject(string title, List<object> objectsToLog)
        {
            _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
            _logger.LogInformation($"{title}");
            _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
            objectsToLog?.ForEach((o) => { _logger.LogInformation($"{JsonConvert.SerializeObject(o)}"); });
            if (objectsToLog != null && objectsToLog.Any())
            {
                _logger.LogInformation("----------------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}
