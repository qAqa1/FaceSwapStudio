import cv2
import core.face_swap_core as fswap

cv_bodies = cv2.imread("test_images/body/input1.png")

detected_faces = fswap.detect_faces(cv_bodies)
print('Detected faces:')
for index, face_box in enumerate(detected_faces):
    print(str(index) + ')', face_box)

print(type(detected_faces[0]))

img = cv_bodies
label = 'face1'

x1 = int(detected_faces[0][0])
y1 = int(detected_faces[0][1])

x2 = int(detected_faces[0][2])
y2 = int(detected_faces[0][3])

color = (0, 255, 0)
text_color = (0, 0, 255)

# For bounding box
img = cv2.rectangle(img, (x1, y1), (x2, y2), color, 2)

# For the text background
# Finds space required by the text so that we can put a background with that amount of width.
(w, h), _ = cv2.getTextSize(
    label, cv2.FONT_HERSHEY_SIMPLEX, 0.6, 1)

# Prints the text.
img = cv2.rectangle(img, (x1, y1 - 20), (x1 + w, y1), color, -1)
img = cv2.putText(img, label, (x1, y1 - 5),
                  cv2.FONT_HERSHEY_SIMPLEX, 0.6, text_color, 1)

cv2.imshow('graycsale image', img)
cv2.waitKey()
