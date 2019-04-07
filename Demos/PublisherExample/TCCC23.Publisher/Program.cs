﻿using System;
using System.Threading;
using EasyNetQ;
using TCCC23.Publisher.Models;

namespace TCCC23.Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			var busFactory = new BusFactory();
			using (var bus = busFactory.GetBus())
			{
				while (true)
				{
					try
					{
						var exchange = Configuration.HelloWorldExchangeName;
						var routingKey = Configuration.HelloWorldRoutingKey;

						// Declare exchange and publish hello world message.
						var source = bus.Advanced.ExchangeDeclare(exchange, "topic");
						bus.Advanced.Publish(source, routingKey, true, new Message<HelloWorld>(new HelloWorld
						{
							Text = "Hello World!"
						}));
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Publish failure. {ex.Message}");
					}

					Thread.Sleep(1000);
				}
			}
		}
	}
}
