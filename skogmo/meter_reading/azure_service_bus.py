from azure.servicebus import ServiceBusClient, ServiceBusMessage

class AzureServiceBus:
    def __init__(self):
        self.CONNECTION_STR = 'Endpoint=sb://sandaas-iot.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=r3s1U4/lzrCM2kF8BaLOIinQVWbwnPUlYhd91PjRYdQ='
        self.QUEUE_NAME = 'ams'
        

    def send_message_to_service_bus(message_to_send, sender):
        # create a Service Bus message
        message = ServiceBusMessage(message_to_send)
        
        # send the message to the queue
        sender.send_messages(message)

    def main(message):
        servicebus_client = ServiceBusClient.from_connection_string(conn_str=CONNECTION_STR, logging_enable=True)
        with servicebus_client:
            sender = servicebus_client.get_topic_sender(topic_name=TOPIC_NAME)
            with sender:
                send_message_to_service_bus(message, sender) 