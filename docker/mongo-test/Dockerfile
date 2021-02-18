FROM ubuntu
LABEL Description="Test image MongoDb from ubuntu linux" Vendor="korchakan"

RUN apt-get update \
 && apt-get -y install sudo \
 && apt-get install -y wget \
 && rm -rf /var/lib/apt/lists/*

RUN sudo apt-get update
RUN yes | sudo apt-get install gnupg
RUN yes | sudo apt install vim 

RUN wget -qO - https://www.mongodb.org/static/pgp/server-4.4.asc | sudo apt-key add - && echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/4.4 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-4.4.list && sudo apt-get update && sudo apt-get install -y mongodb-org

RUN sudo mkdir /home/mongo &&  sudo mkdir /home/mongo/db1 && sudo chmod 777 /home/mongo/db1

RUN mongod --dbpath /home/mongo/db1 --port 27001 --fork --logpath /home/mongo/db1/db1.log --pidfilepath /home/mongo/db1/db1.pid --bind_ip_all

EXPOSE 27001

# НЕ заработало пока что ENTRYPOINT mongod --dbpath /home/mongo/db1 --port 27001 --fork --logpath /home/mongo/db1/db1.log --pidfilepath /home/mongo/db1/db1.pid --bind_ip_all