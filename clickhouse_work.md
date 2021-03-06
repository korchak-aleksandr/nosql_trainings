## ClickHouse

����� �� ������:
1. ���� ���������� � ������ ��� ���������� ClickHouse Server � ClickHouse Client

		docker run -d --name some-clickhouse-server --ulimit nofile=262144:262144 yandex/clickhouse-server
		docker run -it --rm --link some-clickhouse-server:clickhouse-server yandex/clickhouse-client --host clickhouse-server

![clickhouse_docker](attachements/clickhouse_docker.png)

��� ����������� ���������� ClickHouse Client ����������� ������� ��� ����������� clickhouse-client ����������� IP ����� �������, ����� ������ ���������� IP ������� ����������� ��������:

		docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' some-clickhouse-server

2. � ���������� ClickHouse Clien ��� ���������� cUrl � xz-utils ��� ����������� ���������� � ���������� ������� � ������� ��� �������� � ClickHouse
3. ������  ������ � ���������� ��������� �� tutorial
4. �������� �������� � �� ��������� INSERT INTO
5. ������� �������� ������� �� �� �� tutorial <br>

![clickhouse_test_query](attachements/clickhouse_test_query.png)
![clickhouse_test_query](attachements/clickhouse_test_query_2.png)

6. ������� ������������ �������� ���������� ��������, � ����� ����� ������ ����������� �������� ��� � ����������. ����������� ��� ��� ����� �������� ������� ������ �� ��������� 250��.

���������:<br>
https://clickhouse.tech/docs/ru/getting-started/tutorial/ <br>
https://clickhouse.tech/docs/en/interfaces/cli/ <br>
https://hub.docker.com/r/yandex/clickhouse-server