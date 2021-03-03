## Cassandra - часть 1

Отчет по работе:
1. Был установлен локально Kubernetes
2. Для удобства сразу был настроен Web UI (Dashboard) по инструкции на официальном сайте
3. Был поднят трехузловой кластер из БД Cassandra. Перед созданием были созданы три Persistent Volumes.

		kubectl apply -f .\pv-kube-test-0.yaml
		kubectl apply -f .\pv-kube-test-1.yaml
		kubectl apply -f .\pv-kube-test-2.yaml
		kubectl apply -f .\cassandra-service.yaml
		kubectl apply -f .\cassandra-statefulset.yaml

Сами yaml файлы: <br>
[pv-kube-test-0](cassandra/pv-kube-test-0.yaml)<br>
[cassandra-service.yaml](cassandra/cassandra-service.yaml)<br>
[cassandra-statefulset.yaml](cassandra/cassandra-statefulset.yaml)

![cassandra_kube](attachements/cassandra_kube.png)

4. Были протестированы запросы из демонстрации на лекции. Вставка данных была на одну из нод, чтение данных было проверено со всех нод.

![cassandra_populate_test](attachements/cassandra_populate_test.png)

5. На одной из нод Cassandra был установлен wget, скачан официальный архив Cassandra с инструментами.
6. Была запущена утилита cassandra-stress на запись и чтение

		./cassandra-stress write n=1000000
		./cassandra-stress read n=100000

Ниже результаты тестирования:
![cassandra_stress_write](attachements/cassandra_stress_write.png)
![cassandra_stress_read](attachements/cassandra_stress_read.png)


Материалы:<br>
https://medium.com/ksquare-inc/how-to-use-apache-cassandras-stress-tool-a-step-by-step-guide-649ea26daa5d <br>
https://kubernetes.io/docs/tutorials/stateful-application/cassandra/ <br>
https://kubernetes.io/docs/tasks/access-application-cluster/web-ui-dashboard/ <br>
https://kubernetes.io/ru/docs/reference/kubectl/cheatsheet/ <br>
https://kubernetes.io/docs/concepts/configuration/overview/