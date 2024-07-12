import cv2
import core.face_swap_core as face_swap
import utility.utility as utility

cv_bodies = cv2.imread("test_images/body/input1.png")

detected_faces = face_swap.detect_faces(cv_bodies)
print('Detected faces:')
for index, face_box in enumerate(detected_faces):
    print(str(index) + ')', face_box)

print(type(detected_faces[0]))

img = cv_bodies
label = 'face1'

rectangle_color = (0, 255, 0)
label_color = (0, 0, 0)

# detected_faces_image = utility.draw_rectangle_with_text(cv_bodies, detected_faces[0], rectangle_color, 'face 1',
#                                                         label_color)

detected_faces_image = utility.draw_rectangles_with_text(cv_bodies, detected_faces, 'face')

cv2.imshow('Detected faces', detected_faces_image)
cv2.waitKey()
