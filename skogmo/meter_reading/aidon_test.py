#!/usr/bin/python

import serial, time, sys
from aidon_obis import *
from datetime import date
import datetime

if len(sys.argv) != 2:
    print("Usage: ... <serial_port>")
    sys.exit(0)


def aidon_callback(fields):
    print(fields)


ser = serial.Serial(sys.argv[1], 2400, timeout=0.05, parity=serial.PARITY_NONE)
a = aidon(aidon_callback)
startTime = datetime.datetime.now()

while (1):
    while ser.inWaiting():
        byte = ser.read(1)
        a.decode(byte)
    time.sleep(0.01)

    # currentTime = datetime.datetime.now()
    # timeSpan = currentTime - startTime
    # if timeSpan.total_seconds() > 10:
    #     ser.close()
    #     exit(1)
