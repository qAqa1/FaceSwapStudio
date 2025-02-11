from console.face_swap_console_core import face_swap
import sys


output_dir = ""
face_filename = ""
body_filename = ""

if len(sys.argv) == 1:
    output_dir = input("output dir path: ")
    face_filename = input("input face path: ")
    body_filename = input("input body path: ")
    print("output dir: " + output_dir)
    print("face path: " + face_filename)
    print("body path: " + body_filename)
    print('possible run with args: [pyhton] [script_name] [output_dir] [path_to_face] [path_to_body]')
elif len(sys.argv) == 4:
    print('Parsing user data:')
    output_dir = sys.argv[1]
    face_filename = sys.argv[2]
    body_filename = sys.argv[3]
    print("output dir: " + output_dir)
    print("face path: " + face_filename)
    print("body path: " + body_filename)
    print()
else:
    print('error args count, run with: [pyhton] [script_name] [output_dir] [path_to_face] [path_to_body]')
    exit(1)

face_swap(output_dir, face_filename, body_filename)
