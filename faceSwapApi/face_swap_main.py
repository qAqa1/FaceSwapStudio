import cv2
import core.face_swap_core as face_swap
import utility.utility as utility

cv_bodies = cv2.imread("test_images/body/body1.png")
# cv_bodies = cv2.imread("test_images/no_face_image.jpg")

detected_faces = face_swap.detect_faces(cv_bodies)
detected_faces_rectangles = face_swap.get_faces_rectangles(detected_faces)
print('Detected faces:')
for index, face_box in enumerate(detected_faces_rectangles):
    print(str(index) + ')', face_box)

# detected_faces_image = utility.draw_rectangle_with_text(cv_bodies, detected_faces[0], rectangle_color, 'face 1',
#                                                         label_color)

detected_faces_image = utility.draw_rectangles_with_text(cv_bodies, detected_faces_rectangles, 'source face')

# cv_face1 = cv2.imread("test_images/no_face_image.jpg")
cv_face1 = cv2.imread("test_images/face/face1.jpg")
cv_face2 = cv2.imread("test_images/face/face2.jpg")
cv_face3 = cv2.imread("test_images/face/face3.jpg")
cv_face4 = cv2.imread("test_images/face/face4.jpg")

# target_faces = face_swap.detect_faces(cv_face1)

# result = face_swap.swap_face(cv_bodies, detected_faces[0], target_faces[0])

result = face_swap.swap_face_by_images(cv_bodies, cv_face1, 0)
result = face_swap.swap_face_by_images(result, cv_face2, 1)
result = face_swap.swap_face_by_images(result, cv_face3, 2)
result = face_swap.swap_face_by_images(result, cv_face4, 3)

result = utility.draw_rectangles_with_text(result, detected_faces_rectangles, 'result face')

cv2.imshow('Detected faces', detected_faces_image)
cv2.imshow('Swapped faces', result)
cv2.waitKey()
