## Redis

Отчет по работе:
1. Был запущен шардированный отказоустойчивый кластер Redis из 6 нод. Запуск производился в docker из готового docker-compose файла: [docker-compose.yml](redis/docker-compose.yml)

![redis_cluster_docker](attachements/redis_cluster_docker.png)
![redis_cluster_docker_2](attachements/redis_cluster_docker_2.png)

2. Были протестированы Set/Get ключей с их редиректом на другие мастер ноды с параметром подключения __-с__

![redis_cluster_docker_3](attachements/redis_cluster_docker_3.png)

3. Также были исправлены дефолтные таймауты определения down состояния ноды

![redis_cluster_docker_config](attachements/redis_cluster_docker_config.png)

4. Попробовал остановить контейнер с master нодой. Ожидаемо slave нода становится master c нулем подключенных slave нод. А при поднятии ноды она поднимается уже как slave нода.

![redis_cluster_docker_down](attachements/redis_cluster_docker_down.png)

Материалы: <br>
https://github.com/bitnami/bitnami-docker-redis-cluster <br>
https://hub.docker.com/_/redis <br>
https://kb.objectrocket.com/redis/how-to-use-the-redis-to-store-json-data-1545