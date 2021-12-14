import struct, crcmod, time, codecs,datetime

# HDLC constants
FLAG = b'\x7e'
ESCAPE = b'\x7d'

# HDLC states
WAITING = 0
DATA = 1
ESCAPED = 2

# Number of objects in known frames
OBJECTS_2P5SEC = 1
OBJECTS_10SEC = 12
OBJECTS_1HOUR = 17

# OBIS types
TYPE_STRING = 0x0a
TYPE_UINT32 = 0x06
TYPE_INT16 = 0x10
TYPE_OCTETS = 0x09
TYPE_UINT16 = 0x12

class aidon:
    def __init__(self, callback):
        self.state = WAITING
        self.pkt = b""
        self.b = bytearray()
        self.crc_func = crcmod.mkCrcFun(0x11021, rev=True, initCrc=0xffff, xorOut=0x0000)
        self.callback = callback

	# Does a lot of assumptions on Aidon/Hafslund COSEM format
	# Not a general parser! 
    def parse(self, pkt):

		# 0,1 frame format
		# 2 client address
		# 3,4 server address
		# 5 control
		# 6,7 HCS
		# 8,9,10 LLC

        raw_data = pkt
        print(f"Data: {raw_data}")
        frame_type = (pkt[0] & 0xf0) >> 4
        length = ((pkt[0] & 0x07) << 8) + pkt[1]
        object_count =pkt[18]
        pkt = pkt[19:] # Remove 18 first bytes to start with first object

        print(f"Data before parse: {pkt}")
        fields = {}
        # print(f"object count: {object_count}")
        
		# If number of objects doesn't match any known type, don't continue
        if not (object_count in [OBJECTS_2P5SEC, OBJECTS_10SEC, OBJECTS_1HOUR]):
            return

        # Fill array with objects
        data = []

        try:
            for j in range(0, object_count):
                dtype = pkt[10]
                
                if (dtype == TYPE_STRING):
                    size = pkt[11]
                    data.append(pkt[12:12+size])
                    pkt = pkt[12+size:]

                elif (dtype == TYPE_UINT32):
                    data.append(struct.unpack(">I", pkt[11:15])[0])
                    pkt = pkt[21:]

                elif (dtype == TYPE_INT16):
                    data.append(struct.unpack(">h", pkt[11:13])[0])
                    pkt = pkt[19:]

                elif (dtype == TYPE_OCTETS):
                    size = pkt[11]
                    data.append(pkt[12:12+size])
                    pkt = pkt[12+size:]

                elif (dtype == TYPE_UINT16):
                    data.append(struct.unpack(">H", pkt[11:13])[0])
                    pkt = pkt[19:]

                else:
                    return # Unknown type, cancel
        except Exception as e:
            fields['error_message'] = e
            fields['raw_data'] = raw_data
            self.callback(fields)
            return

        # Convert array with generic types to dictionary with sensible keys
        fields['timestamp'] = datetime.datetime.now().timestamp()
        if (len(data) == OBJECTS_2P5SEC):
            fields['p_act_in'] = data[0]

        elif (len(data) == OBJECTS_10SEC) or (len(data) == OBJECTS_1HOUR):
            fields['version_id'] = data[0]
            fields['meter_id'] = data[1]
            fields['meter_type'] = data[2]
            fields['p_act_in'] = data[3]
            fields['p_act_out'] = data[4]
            fields['p_react_in'] = data[5]
            fields['p_react_out'] = data[6]
            fields['il1'] = data[7] / 10.0
            fields['il2'] = data[8] / 10.0
            fields['ul1'] = data[9] / 10.0
            fields['ul2'] = data[10] / 10.0
            fields['ul3'] = data[11] / 10.0

            if (len(data) == OBJECTS_1HOUR):
                fields['energy_act_in'] = data[13] / 100.0
                fields['energy_act_out'] = data[14] / 100.0
                fields['energy_react_in'] = data[15] / 100.0
                fields['energy_react_out'] = data[16] / 100.0

        self.callback(fields)                

    def decode(self, c):
        # Waiting for packet start
        # print(f"{c}, {c == FLAG}")
        if (self.state == WAITING): 
            if (c == FLAG):
                self.state = DATA
                self.pkt = b""

        elif (self.state == DATA):
            if (c == FLAG):
                # Minimum length check
                if (len(self.pkt) >= 19):
                    # Check CRC
                    crc = self.crc_func(self.pkt[:-2])
                    crc ^= 0xffff
                    if (crc == struct.unpack("<H", self.pkt[-2:])[0]):
                        print('CRC ok!')
                        self.parse(self.pkt)

                self.pkt = b""
            elif (c == ESCAPE):
                self.state = ESCAPED
            else:
                self.pkt += c

        elif (self.state == ESCAPED):
            self.pkt += c
            self.state = DATA

