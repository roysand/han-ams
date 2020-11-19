import serial
import codecs
import sys
import datetime

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=2400,
    parity=serial.PARITY_NONE,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=4)
print("Connected to: " + ser.portstr)

with open (f'han-data-raw-{datetime.datetime.now().strftime("%Y%m%d-%H%M")}.bin', 'wb') as file:
    while True:
        bytes = ser.read(1024)
        if bytes:
            print('Got %d bytes:' % len(bytes))
            bytes = ('%02x' % int(codecs.encode(bytes, 'hex'), 16)).upper()
            bytes = ' '.join(bytes[i:i+2] for i in range(0, len(bytes), 2))
            print(bytes)
            file.write(bytes)
        else:
            print('Got nothing')
