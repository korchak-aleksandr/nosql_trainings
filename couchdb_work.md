## Couchdb

CouchDb был запущен в контейнере Docker следующей командой (последняя версия образа CouchDb не запускается без указания логин/пароля админа)

		docker run -e COUCHDB_USER=admin -e COUCHDB_PASSWORD=password -p 5984:5984 -d couchdb --name CoucndbTest

Зашел на страницу управления СУБД в браузере по ссылке: http://localhost:5984/_utils/#/_all_dbs
<br> 
Создал базу данных test и добавил в нее документ с заполненным полем name.

Далее поменял ссылку подключения к БД в файле index.html на:

		http://admin:password@localhost:5984/test

Открыл index.html в браузере, синхронизировал записи, попробовал добавить запись, потестировал кнопку sync с остановленным контейнером.

Ниже скриншот index.html после тестирования (на нем также видно что был тест с отключенным контейнером).
![CouchDb Index.html](attachements/Couchdb_Index_html.png)

Сам сохраненный файл: [index.html](attachements/PouchDB.html)

Ниже скриншоты работающего контейнера и страницы управления СУБД:
![CouchDb Docker](attachements/Couchdb_Docker.png)
![CouchDb Fauxton](attachements/Couchdb_fauxton.png)