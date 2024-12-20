﻿using Confluent.Kafka;

namespace OrderService.Kafka
{

    public interface IKafkaProducer
    {
        Task ProduceAsync(string Topic, Message<string,string> Message);
    }

    public class KafkaProducer : IKafkaProducer
    {


        public readonly IProducer<string,string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "192.168.1.15:9092",
            };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public Task ProduceAsync(string Topic, Message<string, string> Message)
        {
            return _producer.ProduceAsync(Topic, Message);
        }
    }
}
