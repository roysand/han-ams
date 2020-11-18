import os
import sys
import re

# State
WAITING = 0
DATA = 1

VALID_BYTE = b'[A-F0-9]'
FLAG = '7E'


class HdlcParse:
    def __init__(self, file_name):
        self.file_name = file_name
        self.pkt = ""
        self.file = open(file_name, "rb")
        self.state = WAITING

    # General HDLC decoder
    # File format is 7E A0 21 FF B1
    # Read two bytes from file and concatenate them to one byte
    def run(self):
        self.pkt = ""
        # byte = self.file.read(1)

        while (1):
            byte = self.read_byte_from_file(self.file)
            if byte == b'':
                self.file.close()
                exit(1)

            if (self.state == WAITING):
                if (byte == FLAG):
                    self.state = DATA
                    self.pkt = ""
            elif (self.state == DATA):
                if (byte == FLAG):
                    if (len(self.pkt) >= 19):
                        # Check CRC
                        # TODO CRC Check
                        self.parse(self.pkt)

                        self.pkt = ""
                        self.state = WAITING
                else:
                    self.pkt += byte

    def parse(self, pkt):
        print('Parse: ', pkt)

    def read_byte_from_file(self, file):
        byte = ''
        hi = b'x'
        lo = b'x'
        i = 0

        while bool(not (re.match(VALID_BYTE, hi))) and hi != b'':
            hi = file.read(1)

        if not hi:
            return b''

        while bool(not (re.match(VALID_BYTE, lo))) and lo != b'':
            lo = file.read(1)

        if not lo:
            return b''

        x = hi + lo
        val = format(int(hi + lo, 16), '02X')
        return val
