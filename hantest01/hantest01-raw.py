import serial
import codecs
import sys
import datetime
import time

ser = serial.Serial(
    port='/dev/ttyUSB0',
    baudrate=2400,
    parity=serial.PARITY_NONE,
    stopbits=serial.STOPBITS_ONE,
    bytesize=serial.EIGHTBITS,
    timeout=4)
print("Connected to: " + ser.portstr)

loop = 0
with open (f'han-data-raw-{datetime.datetime.now().strftime("%Y%m%d-%H%M")}.bin', 'wb') as file:
    while (1):
        while ser.inWaiting():
            byte = ser.read(1)
            file.write(byte)

        time.sleep(0.01)
