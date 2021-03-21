import uuid
import time
from locust import HttpUser, task, between
from faker import Faker


class QuickstartUser(HttpUser):
    # wait_time = between(1, 2)

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.fake = Faker()

    @task
    def write_entity_cassandra(self):
        self.client.post("/CassandraStress/WriteEntity",
                         json=self.generate_entity())

    @task
    def read_entity_cassandra(self):
        self.client.get("/CassandraStress/ReadEntity?id=" + str(uuid.uuid4()), name="/CassandraStress/ReadEntity")

    @task
    def write_entity_mongo(self):
        self.client.post("/MongoStress/WriteEntity",
                         json=self.generate_entity())

    @task
    def read_entity_mongo(self):
        self.client.get("/MongoStress/ReadEntity?id=" + str(uuid.uuid4()), name="/MongoStress/ReadEntity")

    def generate_entity(self):
        return {"id": str(uuid.uuid4()),
                "name": self.fake.name(),
                "age": self.fake.pyint(),
                "description": self.fake.text(),
                "address": self.fake.address(),
                "phone": self.fake.phone_number(),
                "email": self.fake.email()}
