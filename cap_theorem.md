## CAP-теорема

CAP - Consistency, Availability, Partition tolerance. <br>
Необходимо соотнести по CAP-теореме следующие базы данных и аргументировать свой выбор.
Базы данных:
1. MSSQL
1. MongoDB
1. Cassandra

**MSQSL:**<br>
Считаю, что **MSSQL** в виде Single-Node решения либо MSSQL Server Cluster относится к **CA (Consistency + Availability) решению**. Так как MSSQL поддерживает ACID, то любой запрос к системе будет завершаться корректным откликом от нее и будет всегда доступен, пока единственная мастер нода будет работать (*вообще говоря рассматривать Single-Node RDBMS с точки зрения CAP-теоремы не совсем корректно*). В случае MSSQL Server Cluster это будет также как с Single-Node решением, так как Storage данных всегда на отдельном выделенном сервере (плюсом является повышение свойства Availability).<br>
[Подробнее про MSSQL Server Cluster.](https://www.brentozar.com/archive/2012/02/introduction-sql-server-clusters/)

При этом если рассматривать решение MSSQL Server AlwaysOn, то в случае включения Asynchronous-commit mode данное решение уже становится **AP (Availability + Partition tolerance) решением**, так как она не гарантирует согласованности данных из-за асинхронной передачи транзакций с мастер ноды на реплики, с учетом что приложение читает данные только с реплик. <br>
[Подробнее на MSDN про MSSQL Server AlwaysOn #1](https://docs.microsoft.com/ru-ru/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server?view=sql-server-ver15) <br>
[Подробнее на MSDN про MSSQL Server AlwaysOn #2](https://docs.microsoft.com/ru-ru/sql/database-engine/availability-groups/windows/availability-modes-always-on-availability-groups?view=sql-server-ver15)


**MongoDB:**<br>
Считаю, что **MongoDB** относится к **CP (Consistency + Partition tolerance) решению**, так как при записи и чтении с мастер ноды мы всегда будем получать согласованные данные, при чтении с реплик у нас будут eventually consistent данные, при записи с выбором корректного Write Concern соблюдается согласованность данных, при отправке запроса на запись, мы дожидаемся acknowledgment от 2 или более серверов (в зависимости от выбора). При падении нод, у нас кластер продолжает работать при наличии хотя бы двух нод. Доступность записи будет нарушаться или сильно задерживаться, пока система не убедится в своей целостности и согласованности.

**Cassandra:**<br>
Считаю, что **Cassandra** относится к **AP (Availability + Partition tolerance) решением**. При consistency level по умолчанию Cassandra устойчива к падению нод в кластере, при этом остается доступной на запись/чтение. Каждая нода Cassandra не обязуется в тот же момент распространять данные при записи на другие ноды, поэтому возможна выдача пользователям старых данных. <br>
Но если например в Cassandra настроить consistency level в значение ALL, то мы получим максимальную согласованность, но при этом потеряем большую долю Availability.

Используемые материалы: <br>
[Статья #1](https://medium.com/@bikas.katwal10/mongodb-vs-cassandra-vs-rdbms-where-do-they-stand-in-the-cap-theorem-1bae779a7a15#:~:text=CAP%20stands%20for%20Consistency%2C%20Availability,is%20in%20an%20inconsistent%20state.)
[Статья #2](https://www.bigdataschool.ru/wiki/cap)