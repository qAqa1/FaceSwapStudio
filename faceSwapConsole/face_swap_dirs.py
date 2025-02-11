from console.face_swap_console_core import face_swap
import sys
from sys import platform
import os.path
from os.path import join
from glob import glob
from datetime import datetime


output_dir = ""
faces_dir = ""
bodies_dir = ""

if len(sys.argv) == 1:
    output_dir = input("output dir path: ")
    faces_dir = input("input faces directory: ")
    bodies_dir = input("input bodies directory: ")
    print()
    print("output dir: " + output_dir)
    print("faces directory: " + faces_dir)
    print("bodies directory " + bodies_dir)
    print('possible run with args: [pyhton] [script_name] [output_dir] [faces_dir] [bodies_dir]')
elif len(sys.argv) == 4:
    print('Parsing user data:')
    output_dir = sys.argv[1]
    faces_dir = sys.argv[2]
    bodies_dir = sys.argv[3]
    print("output dir: " + output_dir)
    print("faces directory: " + faces_dir)
    print("bodies directory: " + bodies_dir)
    print()
else:
    print('error args count, run with: [pyhton] [script_name] [output_dir] [faces_dir] [bodies_dir]')
    exit(1)

if not os.path.isdir(output_dir):
    print("output directory doesn't exist")
    exit(1)

if not os.path.isdir(faces_dir):
    print("faces directory doesn't exist")
    exit(1)

if not os.path.isdir(bodies_dir):
    print("bodies directory doesn't exist")
    exit(1)

extensions = ['**\\*.gif', '**\\*.png', '**\\*.jpg', '**\\*.jpeg', '**\\*.tiff']

faces = []
for extension in extensions:
    faces.extend(glob(join(faces_dir, extension), recursive=True))

print('faces len = ' + str(len(faces)))
print('faces:')
print(faces)

print()

bodies = []
for extension in extensions:
    bodies.extend(glob(join(bodies_dir, extension), recursive=True))

print('bodies len = ' + str(len(bodies)))
print('bodies:')
print(bodies)

print()

print('************************')
start_time = datetime.now()
print('Start swapping directories, time {}:'.format(start_time))
for face in faces:
    print()
    print()
    print('#############################')
    print('swap face: ' + face)
    for body in bodies:
        print()
        print('________________________________')
        print('body: ' + body)
        face_swap(output_dir, face, body)
end_time = datetime.now()
print('Total calculation time: {}'.format(end_time - start_time))
print()
print()
print('Finished, end time: {0}, total calculation time: {1}, output directory: {2}'.format(end_time, (end_time - start_time), output_dir))
print()

if platform == "darwin":
    print('You can open previews for output by this command:')
    if output_dir.endswith('/'):
        print('qlmanage -p ' + output_dir.replace(' ', '\ ') + '*')
    else:
        print('qlmanage -p ' + output_dir.replace(' ', '\ ') + '/*')

if platform == "darwin":
    print('Open output directory by this command:')
    print('open "' + output_dir + '"')
