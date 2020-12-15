import sys
from aidon_parse import *

def aidon_callback(fields):
    print(fields)

def main():
    if len(sys.argv) < 2:
        print("usage: run.pc <filename>")
        exit(-1)

    if __name__ == '__main__':
        file_name = sys.argv[1]

    a = aidon(aidon_callback)
    with open(file_name, 'rb') as file:
        while 1:
            byte = file.read(1)
            # str = string.fromhex(byte).decode('utf-8')
            if not byte:
                break

            a.decode(byte)

            print(byte)

if __name__ == "__main__":
    main()