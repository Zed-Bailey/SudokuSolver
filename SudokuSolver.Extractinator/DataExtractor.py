# This file is called by c#, an image will be passed in as an argument
# the script should load image in, fetch digits from sudoku grid and then
# print the grids data in lines
# https://stackoverflow.com/questions/59182827/how-to-get-the-cells-of-a-sudoku-grid-with-opencv
import cv2
import sys
import numpy as np
from imutils import contours

testing_img_path = "../SudokuSolver.Test/resources/testImage1.jpg"

print("Hello world")

image = cv2.imread(testing_img_path)
if image is None:
    sys.exit("Couldn't find the image :( @ " + testing_img_path)

# Load image, grayscale, and adaptive threshold
gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
thresh = cv2.adaptiveThreshold(gray,255,cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV,57,5)

# Filter out all numbers and noise to isolate only boxes
cnts = cv2.findContours(thresh, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
cnts = cnts[0] if len(cnts) == 2 else cnts[1]
for c in cnts:
    area = cv2.contourArea(c)
    if area < 1000:
        cv2.drawContours(thresh, [c], -1, (0,0,0), -1)

# Fix horizontal and vertical lines
vertical_kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (1,5))
thresh = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, vertical_kernel, iterations=9)
horizontal_kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (5,1))
thresh = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, horizontal_kernel, iterations=4)

# Sort by top to bottom and each row by left to right
invert = 255 - thresh
cnts = cv2.findContours(invert, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
cnts = cnts[0] if len(cnts) == 2 else cnts[1]
(cnts, _) = contours.sort_contours(cnts, method="top-to-bottom")

sudoku_rows = []
row = []
for (i, c) in enumerate(cnts, 1):
    area = cv2.contourArea(c)
    if area < 50000:
        row.append(c)
        if i % 9 == 0:
            (cnts, _) = contours.sort_contours(row, method="left-to-right")
            sudoku_rows.append(cnts)
            row = []

# Iterate through each box
for row in sudoku_rows:
    for c in row:
        mask = np.zeros(image.shape, dtype=np.uint8)
        cv2.drawContours(mask, [c], -1, (255,255,255), -1)
        result = cv2.bitwise_and(image, mask)
        result[mask==0] = 255
        cv2.imshow('result', result)
        cv2.waitKey(175)

cv2.imshow('thresh', thresh)
cv2.imshow('invert', invert)
cv2.waitKey()

# # img = cv2.GaussianBlur(img, (5,5), cv2.BORDER_DEFAULT)
# img = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
# # normalize the illumination levels across the image
# outer_box = cv2.adaptiveThreshold(img, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY_INV, 57, 5)
# 
# # invert the image so the once black borders are now white
# cv2.bitwise_not(outer_box, outer_box)
# 
# # TODO:  dilate image??
# 
# # Filter out all numbers and noise to isolate only boxes
# cnts = cv2.findContours(outer_box, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
# cnts = cnts[0] if len(cnts) == 2 else cnts[1]
# for c in cnts:
#     area = cv2.contourArea(c)
#     if area < 1000:
#         cv2.drawContours(outer_box, [c], -1, (0,0,0), -1)
# 
# # Fix horizontal and vertical lines
# vertical_kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (1,5))
# thresh = cv2.morphologyEx(outer_box, cv2.MORPH_CLOSE, vertical_kernel, iterations=9)
# horizontal_kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (5,1))
# thresh = cv2.morphologyEx(thresh, cv2.MORPH_CLOSE, horizontal_kernel, iterations=4)
# 
# cv2.imshow("", thresh)
# # cv.imshow("image", img)
# # cv.imshow("image", outer_box)
# 
# 
# cv2.waitKey()

