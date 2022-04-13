ADDI    x1,  x0,    1 # Set x1 to 1.
ADDI x2,x0,    10#Set x2 to 10.

label:
	SUB		x2, x2, x1# Decrement x2

another_label:
	ADD		x1, x0, x2 #Set x1 to x2