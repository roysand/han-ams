import os
import struct
import sys
import re

# State
import crcmod as crcmod

WAITING = 0
DATA = 1
ESCAPED = 2

VALID_BYTE = b'[A-F0-9]'
FLAG = '\x7e'
ESCAPE = '7D'


class HdlcParse:
    def __init__(self, file_name):
        self.file_name = file_name
        self.pkt = ""
        self.file = open(file_name, "rb")
        self.state = WAITING
        self.crc_func = crcmod.mkCrcFun(0x11021, rev=True, initCrc=0xffff, xorOut=0x0000)

    # General HDLC decoder
    # File format is 7E A0 21 FF B1
    # Read two bytes from file and concatenate them to one byte
    def run(self):
        self.pkt = ""
        # byte = self.file.read(1)
        print(type(self.pkt))

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
                        self.pkt = self.pkt.encode('utf-8')

                        crc = self.crc_func(self.pkt[:-2])
                        crc ^= 0xffff

                        test = struct.unpack("<H", self.pkt[-2:])[0]
                        if crc == test: #struct.unpack("<H", self.pkt[-2:])[0]:
                            self.parse(self.pkt)
                        else:
                            print('CRC-check failed')
                            self.parse(self.pkt)

                        self.pkt = ""
                        self.state = WAITING
                elif byte == ESCAPE:
                    self.state = ESCAPED
                else:
                    self.pkt += byte
            elif self.state == ESCAPED:
                self.pkt += chr(ord(byte) ^ 0x20)
                self.state = DATA

    def parse(self, pkt):
        print(f'Parse ({len(pkt)}): {pkt}')

    def read_byte_from_file(self, file):
        byte = ''
        y = ""
        hi = b'x'
        lo = b'x'
        i = 0

        while bool(not (re.match(VALID_BYTE, hi))) and hi != b'':
            # y = format(file.read(1), '02X')
            hi = file.read(1)

        if not hi:
            return b''

        while bool(not (re.match(VALID_BYTE, lo))) and lo != b'':
            lo = file.read(1)

        if not lo:
            return b''

        x = hi + lo
        i = int(x, 16)
        val = r"\x{0}".format(format(int(hi + lo, 16), '02X'))
        val = chr(i);
        return val
