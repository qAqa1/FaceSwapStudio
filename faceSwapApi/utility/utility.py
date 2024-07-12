import cv2


def draw_rectangle_with_text(cv_image, np_array_rectangle, cv_rectangle_color, label, cv_label_color):
    x1 = int(np_array_rectangle[0])
    y1 = int(np_array_rectangle[1])

    x2 = int(np_array_rectangle[2])
    y2 = int(np_array_rectangle[3])

    # For bounding box
    img = cv2.rectangle(cv_image, (x1, y1), (x2, y2), cv_rectangle_color, 2)

    # For the text background
    # Finds space required by the text so that we can put a background with that amount of width.
    (w, h), _ = cv2.getTextSize(
        label, cv2.FONT_HERSHEY_SIMPLEX, 0.6, 1)

    # Prints the text.
    img = cv2.rectangle(img, (x1, y1 - 20), (x1 + w, y1), cv_rectangle_color, -1)
    img = cv2.putText(img, label, (x1, y1 - 5),
                      cv2.FONT_HERSHEY_SIMPLEX, 0.6, cv_label_color, 1)
    return img


def draw_rectangles_with_text(cv_image, np_array_rectangles, label):
    colors = [(255, 0, 0), (0, 255, 0), (0, 0, 255), (255, 0, 255), (255, 255, 0), (0, 255, 255)]
    for index, rectangle in enumerate(np_array_rectangles):
        rectangle_color = colors[index % len(colors)]
        label_color = (0, 0, 0)
        cv_image = draw_rectangle_with_text(cv_image, rectangle, rectangle_color, 'face ' + str(index),
                                            label_color)
    return cv_image

