#PhosphorLUT v2.0

#Copyright 2013 hunterk
#This shader and all associated files are licensed under the General Public License (GPL) v2 or higher.

#USER-EDITABLE VARIABLES (located at the top of the respective files):
#Each of the gaussian blur shaders have a 'blurfactor' variable that can increase or decrease the intensity of the blurring effect.
#They also have a 'wid' variable that effectively controls the brightness of the image. At lower resolutions, the shader can get quite dark, so you 
#can raise this value to compensate.

#The 'phosphorlut-pass2' shader has a variable, 'brightness,' which can also increase the brightness some if the image is too dark.

#If you are already getting gamma correction from somewhere else (such as another shader or filter), you can comment out (i.e., put two slashes in
#front of it, like this: //) the bsnes-gamma-ramp.cg shader below and change the 'shaders' line to 4 instead of 5 to remove it.

shaders = 5

shader0 = phosphorlut-pass0.cg // Applies the LUT to the game image
shader1 = gaussian-horiz.cg // Blurs the combined LUT/game image horizontally
shader2 = gaussian-vert.cg // Blurs the combined LUT/game image vertically
shader3 = phosphorlut-pass1.cg // Combines the LUTed image with the blurred image
shader4 = bsnes-gamma-ramp.cg // Adds some gamma correction

scale_type0 = viewport
scale0 = 2.0
filter_linear0 = true

scale_type1 = source
scale1 = 1.0
filter_linear1 = true

scale_type2 = source
scale2 = 1.0
filter_linear2 = true

textures = PHOSPHOR_LUT
PHOSPHOR_LUT = luts/480pvert.png
PHOSPHOR_LUT_linear = true
