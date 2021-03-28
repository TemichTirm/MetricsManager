MetricsManager



Решение задания к уроку №1. Cервер хранения целочисленных значений температуры в указанное время.

Проект: WeatherForecast
Реализует следующие запросы:
1) Read (startDate, endDate) - Чтение показаний температуры в указанный промежуток времени. В случае отсутствия параметров выдает все имеющиеся записи;
2) Save (temperature, date) - Добавляет значение температуры в указанное время;
3) Update (temperature, date) - Обновляет значение температуры в указанное время;
4) Delete (startDate, endDate) - Удаляет значение температуры в указанном промежутке времени.

_________________________________________________________________________________________________________________________________________________

Решение задания к уроку №2. Менеджер сбора метрик CPU, RAM, HDD, Metwork и .Net (Создание шаблонов контроллеров сбора метрик с агентов и 
всего кластера, создание проекта с шаблоном Агента и его контроллеров сбора метрик. Также формирование шаблонов тестов для всех контроллеров
проекта Менеджера сбора метрик и проекта Агента)


Проект: MetricsManager (Менеджер сбора метрик)
1) Контроллер сбора метрик CPU (CpuMetricsController)
	1.1 - Шаблон метода возврата загрузки CPU для определенного Агента (agentID) в указанном диапазоне времени (fromTime, toTime);
	1.2 - Шаблон метода возврата загрузки CPU для определенного Агента (agentID) в указанном диапазоне времени (fromTime, toTime) с заданным перцентилем (percentile);
	1.3 - Шаблон метода возврата загрузки CPU для всего кластера в указанном диапазоне времени (fromTime, toTime);
	1.4 - Шаблон метода возврата загрузки CPU для всего кластера в указанном диапазоне времени (fromTime, toTime) с заданным перцентилем (percentile).

2) Контроллер сбора метрик RAM (RamMetricsController)
	2.1 - Шаблон метода возврата доступной оперативной памяти для определенного Агента (agentID); 
	2.2 - Шаблон метода возврата доступной оперативной памяти для всего кластера.

3) Контроллер сбора метрик HDD (HddMetricsController)
	3.1 - Шаблон метода возврата оставшегося пространства на диске для определенного Агента (agentID); 
	3.2 - Шаблон метода возврата оставшегося пространства на диске для всего кластера.

4) Контроллер сбора метрик Network (NetworkMetricsController)
	4.1 - Шаблон метода возврата сетевых метрик определенного Агента (agentID) в указанном диапазоне времени (fromTime, toTime);
	4.2 - Шаблон метода возврата сетевых метрик всего кластера в указанном диапазоне времени (fromTime, toTime).

5) Контроллер сбора метрик .Net (DotNetMetricsController)
	5.1 - Шаблон метода возврата метрик работы .Net определенного Агента (agentID) в указанном диапазоне времени (fromTime, toTime);
	5.2 - Шаблон метода возврата метрик работы .Net всего кластера в указанном диапазоне времени (fromTime, toTime).

6) Контроллер Агентов (AgentsController)
	6.1 - Регистрирует нового Агента (объект класса AgentInfo);
	6.2 - Активирует Агента по ID (agentID);
	6.3 - Деактивирует Агента по ID (agentID);


Проект: MetricsManagerTests (Тестирование всех методов всех контроллеров в Менеджере сбора метрик)
1) CpuMetricsControllerUnitTest - тестирование всехметодов в CpuMetricsController
2) RamMetricsControllerUnitTest - тестирование всехметодов в RamMetricsController
3) HddMetricsControllerUnitTest - тестирование всехметодов в HddMetricsController
4) NetworkMetricsControllerUnitTest - тестирование всехметодов в NetworkMetricsController
5) DotNetMetricsControllerUnitTest - тестирование всехметодов в DotNetMetricsController
6) AgentsControllerUnitTest - тестирование всехметодов в AgentsController


Проект: MetricsAgent (Агент метрик)
1) Контроллер сбора метрик CPU (CpuMetricsController)
	1.1 - Шаблон метода возврата загрузки CPU в указанном диапазоне времени (fromTime, toTime);
	1.2 - Шаблон метода возврата загрузки CPU в указанном диапазоне времени (fromTime, toTime) с заданным перцентилем (percentile).
	
2) Контроллер сбора метрик RAM (RamMetricsController)
	2.1 - Шаблон метода возврата доступной оперативной памяти.

3) Контроллер сбора метрик HDD (HddMetricsController)
	3.1 - Шаблон метода возврата оставшегося пространства на диске.

4) Контроллер сбора метрик Network (NetworkMetricsController)
	4.1 - Шаблон метода возврата сетевых метрик в указанном диапазоне времени (fromTime, toTime).

5) Контроллер сбора метрик .Net (DotNetMetricsController)
	5.1 - Шаблон метода возврата метрик работы .Net в указанном диапазоне времени (fromTime, toTime).


Проект: MetricsAgentTests (Тестирование всех методов всех контроллеров в Агенте метрик)
1) CpuMetricsControllerUnitTest - тестирование всехметодов в CpuMetricsController
2) RamMetricsControllerUnitTest - тестирование всехметодов в RamMetricsController
3) HddMetricsControllerUnitTest - тестирование всехметодов в HddMetricsController
4) NetworkMetricsControllerUnitTest - тестирование всехметодов в NetworkMetricsController
5) DotNetMetricsControllerUnitTest - тестирование всехметодов в DotNetMetricsController
