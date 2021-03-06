## InterSystems Cache

Отчет по работе:
1. Был запущен следующий контейнер в докере с БД InterSystems Cache

		docker run -d -p 52773:52773 --name intersystems_iris intersystemsdc/iris-community:2020.4.0.524.0-zpm

![cache_start_page](attachements/cache_start_page.png)

2. По инструкции из материалов была создана база данных, таблица в ней через VS Code, табица была заполнена данными.

![cache_query_1](attachements/cache_query_1.png)
![cache_query_2](attachements/cache_query_2.png)

3. Далее был создан уникальный индекс на поле Name.

![cache_query_index](attachements/cache_query_index.png)