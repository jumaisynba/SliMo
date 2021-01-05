#1: (launching sensor)
roslaunch weiss_kms40 kms40.launch 

#2: (sending data to Teensy board)
rosrun rosserial_python serial_node.py /dev/ttyACM0

#3: (sending data to Unity PC)
cd Desktop 
python3 test.py
